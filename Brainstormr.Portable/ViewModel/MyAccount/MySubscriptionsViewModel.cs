using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.Model.MyAccount;
using Brainstormr.Portable.ViewModel.MyAccount.msg;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.MyAccount
{
    public class MySubscriptionsViewModel : ViewModelBase
    {
        IMyAccountService _myAccountService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;
        public MySubscriptionsViewModel(IMyAccountService myAccountService, ILocalDbService localDbService, IDialogService dialogService)
        {
            _myAccountService = myAccountService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            MySubscriptionsList = new ObservableCollection<MySubscriptionItemModel>();
        }

        private RelayCommand _LoadMySubscriptionsCommand;
        private RelayCommand<MySubscriptionItemModel> _OpenMySubscriptionDetailCommand;
        public ObservableCollection<MySubscriptionItemModel> MySubscriptionsList
        {
            get;
            private set;
        }
        public RelayCommand LoadMySubscriptionsCommand
        {
            get
            {
                return _LoadMySubscriptionsCommand
                    ?? (_LoadMySubscriptionsCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            MySubscriptionsList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var subscriptionlist = await _myAccountService.getMySubscriptions(usertokenrec);
                                foreach (var item in subscriptionlist)
                                {
                                    MySubscriptionsList.Add(item);
                                }
                                RaisePropertyChanged(() => MySubscriptionsList);
                            }
                        }
                        catch (System.Exception ex)
                        {

                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }
        public RelayCommand<MySubscriptionItemModel> OpenMySubscriptionDetailCommand
        {
            get
            {
                return _OpenMySubscriptionDetailCommand
                    ?? (_OpenMySubscriptionDetailCommand = new RelayCommand<MySubscriptionItemModel>(
                    async subscriptiondetail =>
                    {
                        try
                        {
                            var msg = new msg_MySubscriptionDetail()
                            {
                                Amount = subscriptiondetail.Amount,
                                Author = subscriptiondetail.Author,
                                Category = subscriptiondetail.Category,
                                Created = subscriptiondetail.Created,
                                Description = subscriptiondetail.Description,
                                Duration = subscriptiondetail.Duration,
                                EndDate = subscriptiondetail.EndDate,
                                Id = subscriptiondetail.Id,
                                ItemId = subscriptiondetail.ItemId,
                                Modified = subscriptiondetail.Modified,
                                Name = subscriptiondetail.Name,
                                Picture = subscriptiondetail.Picture,
                                StartDate = subscriptiondetail.StartDate,
                                Subject = subscriptiondetail.Subject,
                                TransactionId = subscriptiondetail.TransactionId,
                                Type = subscriptiondetail.Type,
                                url = subscriptiondetail.url,
                                UserEmail = subscriptiondetail.UserEmail
                            };
                            Messenger.Default.Send<msg_Transport<msg_MySubscriptionDetail>>(new msg_Transport<msg_MySubscriptionDetail>(msg));

                        }
                        catch (System.Exception ex)
                        {

                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }

    }
}

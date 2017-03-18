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
    public class MyEbooksViewModel : ViewModelBase
    {
        IMyAccountService _myAccountService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;

        public MyEbooksViewModel(IMyAccountService myAccountService, ILocalDbService localDbService, IDialogService dialogService)
        {
            _myAccountService = myAccountService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            MyEbooksList = new ObservableCollection<MyEbookItemModel>();
        }

        private RelayCommand _LoadMyEbooksCommand;
        private RelayCommand<MyEbookItemModel> _OpenMyEbookDetailCommand;
        public ObservableCollection<MyEbookItemModel> MyEbooksList
        {
            get;
            private set;
        }
        public RelayCommand LoadMyEbooksCommand
        {
            get
            {
                return _LoadMyEbooksCommand
                    ?? (_LoadMyEbooksCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            MyEbooksList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var ebbokslist = await _myAccountService.getMyEbooks(usertokenrec);
                                foreach (var item in ebbokslist)
                                {
                                    MyEbooksList.Add(item);
                                }
                                RaisePropertyChanged(() => MyEbooksList);
                            }
                        }
                        catch (System.Exception ex)
                        {

                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }
        public RelayCommand<MyEbookItemModel> OpenMyEbookDetailCommand
        {
            get
            {
                return _OpenMyEbookDetailCommand
                    ?? (_OpenMyEbookDetailCommand = new RelayCommand<MyEbookItemModel>(
                    async ebookdetail =>
                    {
                        try
                        {
                            var msg = new msg_MyEbookDetail()
                            {
                                Amount = ebookdetail.Amount,
                                Author = ebookdetail.Author,
                                Category = ebookdetail.Category,
                                Created = ebookdetail.Created,
                                Description = ebookdetail.Description,
                                Duration = ebookdetail.Duration,
                                EndDate = ebookdetail.EndDate,
                                Id = ebookdetail.Id,
                                Modified = ebookdetail.Modified,
                                Name = ebookdetail.Name,
                                Picture = ebookdetail.Picture,
                                StartDate = ebookdetail.StartDate,
                                Subject = ebookdetail.Subject,
                                TransactionId = ebookdetail.TransactionId,
                                Type = ebookdetail.Type,
                                url = ebookdetail.url,
                                UserName = ebookdetail.UserName
                            };
                            Messenger.Default.Send<msg_Transport<msg_MyEbookDetail>>(new msg_Transport<msg_MyEbookDetail>(msg));

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

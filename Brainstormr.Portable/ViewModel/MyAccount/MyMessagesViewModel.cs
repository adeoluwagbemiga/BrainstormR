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
    public class MyMessagesViewModel : ViewModelBase
    {
        IMyAccountService _myAccountService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;
        public MyMessagesViewModel(IMyAccountService myAccountService, ILocalDbService localDbService, IDialogService dialogService)
        {
            _myAccountService = myAccountService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            MyMessagesList = new ObservableCollection<MyMessageItemModel>();
        }
        private RelayCommand _LoadMyMessagesCommand;
        private RelayCommand<MyMessageItemModel> _OpenMyMessageDetailCommand;
        public ObservableCollection<MyMessageItemModel> MyMessagesList
        {
            get;
            private set;
        }
        public RelayCommand LoadMyMessagesCommand
        {
            get
            {
                return _LoadMyMessagesCommand
                    ?? (_LoadMyMessagesCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            MyMessagesList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var subscriptionlist = await _myAccountService.getMyMessages(usertokenrec);
                                foreach (var item in subscriptionlist)
                                {
                                    MyMessagesList.Add(item);
                                }
                                RaisePropertyChanged(() => MyMessagesList);
                            }
                        }
                        catch (System.Exception ex)
                        {

                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }
        public RelayCommand<MyMessageItemModel> OpenMyMessageDetailCommand
        {
            get
            {
                return _OpenMyMessageDetailCommand
                    ?? (_OpenMyMessageDetailCommand = new RelayCommand<MyMessageItemModel>(
                    async messagedetail =>
                    {
                        try
                        {
                            var msg = new msg_MyMessageDetail()
                            {
                                AccessToken = messagedetail.AccessToken,
                                Created = messagedetail.Created,
                                Id = messagedetail.Id,
                                InstructorId = messagedetail.InstructorId,
                                UserEmail = messagedetail.UserEmail,
                                InstructorImage = messagedetail.InstructorImage,
                                InstructorName = messagedetail.InstructorName,
                                Message = messagedetail.Message,
                                Modified = messagedetail.Modified,
                                Read = messagedetail.Read,
                                Reply = messagedetail.Reply,
                                Subject = messagedetail.Subject
                            };
                            Messenger.Default.Send<msg_Transport<msg_MyMessageDetail>>(new msg_Transport<msg_MyMessageDetail>(msg));

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

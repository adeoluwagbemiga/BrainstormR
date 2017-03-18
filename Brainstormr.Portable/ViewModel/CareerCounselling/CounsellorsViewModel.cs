using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.Model.CareerCounselling;
using Brainstormr.Portable.ViewModel.AskAnExpert.msg;
using Brainstormr.Portable.ViewModel.CareerCounselling.msg;
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

namespace Brainstormr.Portable.ViewModel.CareerCounselling
{
    public class CounsellorsViewModel : ViewModelBase
    {
        ICareerCounsellingService _careercounsellingService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;

        public CounsellorsViewModel(ICareerCounsellingService careercounsellingService, ILocalDbService localDbService, IDialogService dialogService)
        {
            _careercounsellingService = careercounsellingService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            CounsellorsList = new ObservableCollection<InstructingExpertItemModel>();

        }
        private RelayCommand _LoadCounsellorsCommand;

        private RelayCommand<InstructingExpertItemModel> _OpenCounsellorDetailCommand;
        public ObservableCollection<InstructingExpertItemModel> CounsellorsList
        {
            get;
            private set;
        }
        public RelayCommand LoadCounsellorsCommand
        {
            get
            {
                return _LoadCounsellorsCommand
                    ?? (_LoadCounsellorsCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            CounsellorsList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var counsellorlist = await _careercounsellingService.getAllCounsellors(usertokenrec);
                                foreach (var item in counsellorlist)
                                {
                                    CounsellorsList.Add(item);
                                }
                                RaisePropertyChanged(() => CounsellorsList);
                                //Messenger.Default.Send<msg_userloggedinAlready>(new msg_userloggedinAlready { loginresult = returnresult });
                            }
                        }
                        catch (System.Exception ex)
                        {

                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }
        public RelayCommand<InstructingExpertItemModel> OpenCounsellorDetailCommand
        {
            get
            {
                return _OpenCounsellorDetailCommand
                    ?? (_OpenCounsellorDetailCommand = new RelayCommand<InstructingExpertItemModel>(
                    async counsellor =>
                    {
                        try
                        {
                            var msg = new msg_CounsellorDetail(counsellor);
                            Messenger.Default.Send<msg_Transport<msg_CounsellorDetail>>(new msg_Transport<msg_CounsellorDetail>(msg));
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

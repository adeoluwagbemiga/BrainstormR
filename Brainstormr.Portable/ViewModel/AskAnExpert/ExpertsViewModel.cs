using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.Model.AskAnExpert;
using Brainstormr.Portable.ViewModel.AskAnExpert.msg;
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

namespace Brainstormr.Portable.ViewModel.AskAnExpert
{
    public class ExpertsViewModel : ViewModelBase
    {
        IAskAnExpertService _expertService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;

        public ExpertsViewModel(IAskAnExpertService expertService, ILocalDbService localDbService, IDialogService dialogService)
        {
            _expertService = expertService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            ExpertsList = new ObservableCollection<InstructingExpertItemModel>();

        }
        private RelayCommand _LoadExpertsCommand;

        private RelayCommand<InstructingExpertItemModel> _OpenExpertDetailCommand;
        public ObservableCollection<InstructingExpertItemModel> ExpertsList
        {
            get;
            private set;
        }
        public RelayCommand LoadExpertsCommand
        {
            get
            {
                return _LoadExpertsCommand
                    ?? (_LoadExpertsCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            ExpertsList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var expertlist = await _expertService.getAllExpert(usertokenrec);
                                //var instrsubjectlist = await _expertService.
                                foreach (var item in expertlist)
                                {
                                    ExpertsList.Add(item);
                                }
                                RaisePropertyChanged(() => ExpertsList);
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
        public RelayCommand<InstructingExpertItemModel> OpenExpertDetailCommand
        {
            get
            {
                return _OpenExpertDetailCommand
                    ?? (_OpenExpertDetailCommand = new RelayCommand<InstructingExpertItemModel>(
                    async expert =>
                    {
                        try
                        {
                            var msg = new msg_ExpertDetail(expert);
                            Messenger.Default.Send<msg_Transport<msg_ExpertDetail>>(new msg_Transport<msg_ExpertDetail>(msg));

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

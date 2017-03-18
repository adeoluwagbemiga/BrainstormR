using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.Model.Evaluation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.Evaluation
{
    public class EvaluationYearsViewModel : ViewModelBase
    {
        IEvaluationService _evaluationService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;
        public EvaluationYearsViewModel(IEvaluationService evaluationService, ILocalDbService localDbService, IDialogService dialogService)
        {
            _evaluationService = evaluationService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            EvaluationYearsList = new ObservableCollection<EvaluationYearItemModel>();
        }
        private RelayCommand _LoadEvaluationYearsCommand;
        public ObservableCollection<EvaluationYearItemModel> EvaluationYearsList
        {
            get;
            private set;
        }
        public RelayCommand LoadEvaluationsYearsCommand
        {
            get
            {
                return _LoadEvaluationYearsCommand
                    ?? (_LoadEvaluationYearsCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            EvaluationYearsList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var evaluationyearslist = await _evaluationService.getAllEvaluationYears(usertokenrec);
                                foreach (var item in evaluationyearslist)
                                {
                                    EvaluationYearsList.Add(item);
                                }
                                RaisePropertyChanged(() => EvaluationYearsList);
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

    }
}

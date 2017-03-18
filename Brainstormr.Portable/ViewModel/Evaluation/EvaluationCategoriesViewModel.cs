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
    public class EvaluationCategoriesViewModel : ViewModelBase
    {
        IEvaluationService _evaluationService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;
        public EvaluationCategoriesViewModel(IEvaluationService evaluationService, ILocalDbService localDbService, IDialogService dialogService)
        {
            _evaluationService = evaluationService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            EvaluationCategoriesList = new ObservableCollection<EvaluationCategoryItemModel>();
        }
        private RelayCommand _LoadEvaluationCategoriesCommand;
        public ObservableCollection<EvaluationCategoryItemModel> EvaluationCategoriesList
        {
            get;
            private set;
        }
        public RelayCommand LoadEvaluationCategoriesCommand
        {
            get
            {
                return _LoadEvaluationCategoriesCommand
                    ?? (_LoadEvaluationCategoriesCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            EvaluationCategoriesList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var evaluationcategorieslist = await _evaluationService.getAllEvaluationCategories(usertokenrec);
                                foreach (var item in evaluationcategorieslist)
                                {
                                    EvaluationCategoriesList.Add(item);
                                }
                                RaisePropertyChanged(() => EvaluationCategoriesList);
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

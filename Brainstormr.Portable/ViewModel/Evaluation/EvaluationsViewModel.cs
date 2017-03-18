using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.Model.Evaluation;
using Brainstormr.Portable.ViewModel.Evaluation.msg;
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

namespace Brainstormr.Portable.ViewModel.Evaluation
{
    public class EvaluationsViewModel : ViewModelBase
    {
        IEvaluationService _evaluationService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;
        public EvaluationsViewModel(IEvaluationService evaluationService, ILocalDbService localDbService, IDialogService dialogService)
        {
            _evaluationService = evaluationService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            EvaluationsList = new ObservableCollection<EvaluationItemModel>();
        }

        private RelayCommand _LoadEvaluationsCommand;
        private RelayCommand<EvaluationItemModel> _OpenEvaluationDetailCommand;

        public ObservableCollection<EvaluationItemModel> EvaluationsList
        {
            get;
            private set;
        }
        public RelayCommand LoadEvaluationsCommand
        {
            get
            {
                return _LoadEvaluationsCommand
                    ?? (_LoadEvaluationsCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            EvaluationsList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var evaluationslist = await _evaluationService.getAllEvaluations(usertokenrec);
                                foreach (var item in evaluationslist)
                                {
                                    EvaluationsList.Add(item);
                                }
                                RaisePropertyChanged(() => EvaluationsList);
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
        public RelayCommand<EvaluationItemModel> OpenEvaluationDetailCommand
        {
            get
            {
                return _OpenEvaluationDetailCommand
                    ?? (_OpenEvaluationDetailCommand = new RelayCommand<EvaluationItemModel>(
                    async evaluation =>
                    {
                        try
                        {
                            //var usertokenrec = _localDbService.getAccessToken();
                            //var evaluationscategorylist = await _evaluationService.getAllEvaluationCategories(usertokenrec);
                            msg_EvaluationDetail msg = new msg_EvaluationDetail(evaluation);
                            //msg.evaluation_dto.Id = evaluation.Id; msg.evaluation_dto.Name = evaluation.Name;
                            //msg.evaluation_dto.Code = evaluation.Code; msg.evaluation_dto.Duration = evaluation.Duration;
                            //msg.evaluation_dto.ImagePath = evaluation.ImagePath; msg.evaluation_dto.NoOfQuestions = evaluation.NoOfQuestions;
                            //msg.evaluation_dto.Subject = evaluation.Subject; msg.evaluation_dto.Year = evaluation.Year;
                            //msg.evaluation_dto.CategoryId = evaluation.CategoryId;
                            //Messenger.Default.Send<msg_Transport<msg_EvaluationDetail>>(new msg_Transport<msg_EvaluationDetail>(msg));
                            Messenger.Default.Send<msg_EvaluationDetail>(new msg_EvaluationDetail(msg.evaluation_dto));
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

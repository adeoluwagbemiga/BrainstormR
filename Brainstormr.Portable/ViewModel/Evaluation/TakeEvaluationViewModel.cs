using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.LocalDb.Evaluation;
using Brainstormr.Portable.Model.Evaluation;
using Brainstormr.Portable.ViewModel.Evaluation.msg;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.Evaluation
{
    public class TakeEvaluationViewModel : ViewModelBase
    {
        IEvaluationService _evaluationService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;
        ILocalEvaluationService _localEvaluationService;
        msg_TakeEvaluation _msgTakeEvaluation;
        public TakeEvaluationViewModel(IEvaluationService evaluationService, ILocalDbService localDbService,
            IDialogService dialogService, ILocalEvaluationService localEvaluationService, msg_TakeEvaluation msgTakeEvaluation)
        {
            _evaluationService = evaluationService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            _localEvaluationService = localEvaluationService;
            _msgTakeEvaluation = msgTakeEvaluation;
        }

        private RelayCommand<int> _LoadTakeEvaluationCommand;
        private RelayCommand _StartEvaluationCommand;

        private int _evalid;
        private string _category;
        private string _subject;
        private decimal _totalscore;
        private int _noofquestions;
        public int EvalId
        {
            get
            {
                return _evalid;
            }
            set
            {
                Set(() => EvalId, ref _evalid, value);
            }
        }
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                Set(() => Category, ref _category, value);
            }
        }
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                Set(() => Subject, ref _subject, value);
            }
        }
        public decimal TotalScore
        {
            get
            {
                return _totalscore;
            }
            set
            {
                Set(() =>TotalScore, ref _totalscore, value);
            }
        }
        public int NoOfQuestions
        {
            get
            {
                return _noofquestions;
            }
            set
            {
                Set(() => NoOfQuestions, ref _noofquestions, value);
            }
        }

        public RelayCommand<int> LoadTakeEvaluationCommand
        {
            get
            {
                return _LoadTakeEvaluationCommand
                    ?? (_LoadTakeEvaluationCommand = new RelayCommand<int>(
                    async evalid =>
                    {
                        try
                        {
                            //Messenger.Default.Send<msg_userloggedinAlready>(new msg_userloggedinAlready { loginresult = returnresult });
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                EvalId = evalid; RaisePropertyChanged(() => EvalId);
                                //var questions = await _localEvaluationService.getQuestions(evalid);
                                //if (questions != null)
                                //{
                                     var getquestions = await _evaluationService.getQuestions(usertokenrec, EvalId);
                                    TotalScore = 0.00m; RaisePropertyChanged(() => TotalScore);
                                    foreach (var item in getquestions)
                                    {
                                        //var questionid = questions.First().Id;
                                        
                                        var question = new Question()
                                        {
                                            Created = item.Created,
                                            Duration = item.Duration,
                                            Id = item.Id,
                                            ImagePath = item.ImagePath,
                                            isInstruction = item.isInstruction,
                                            Mark = item.Mark,
                                            Modified = item.Modified,
                                            QuestionCategory = item.QuestionCategory,
                                            QuestionText = item.QuestionText,
                                            Subject = item.Subject,
                                            Year = item.Year,
                                            EvaluationId = EvalId
                                        };
                                        await _localEvaluationService.saveQuestions(question);
                                        var questionoptions = await _localEvaluationService.getQuestionOptions(item.Id);
                                        if (getquestions == null)
                                        {
                                            var getquestionoptions = await _evaluationService.getQuestionOptions(usertokenrec, item.Id);
                                            foreach (var option in getquestionoptions)
                                            {
                                                var qoption = new QuestionOption()
                                                {
                                                    Created = option.Created,
                                                    Id = option.Id,
                                                    IsAnswer = option.IsAnswer,
                                                    Modified = option.Modified,
                                                    OptionText = option.OptionText,
                                                    QuestionId = option.QuestionId
                                                };
                                                await _localEvaluationService.saveQuestionOptions(qoption);
                                            }
                                        }
                                    //}
                                
                                Category = getquestions.First().QuestionCategory; RaisePropertyChanged(() => Category);
                                Subject = getquestions.First().Subject; RaisePropertyChanged(() => Subject);
                                //TotalScore = 0.00m; RaisePropertyChanged(() => TotalScore);
                                NoOfQuestions = getquestions.Count(); RaisePropertyChanged(() => NoOfQuestions);
                                
                                   
                                }
                            }
                        }
                        catch (System.Exception ex)
                        {
                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }
        public RelayCommand StartEvaluationCommand
        {
            get
            {
                return _StartEvaluationCommand
                    ?? (_StartEvaluationCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            var msg = new msg_StartEvaluation(EvalId);
                            Messenger.Default.Send<msg_StartEvaluation>(new msg_StartEvaluation(msg._evalId));
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

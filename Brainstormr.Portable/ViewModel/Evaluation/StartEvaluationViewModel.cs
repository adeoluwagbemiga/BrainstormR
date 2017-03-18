using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.LocalDb.Evaluation;
using Brainstormr.Portable.Model.Evaluation;
using Brainstormr.Portable.ViewModel.Evaluation.msg;
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

namespace Brainstormr.Portable.ViewModel.Evaluation
{
    public class StartEvaluationViewModel : ViewModelBase
    {
        IEvaluationService _evaluationService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;
        ILocalEvaluationService _localEvaluationService;
        msg_StartEvaluation _msgStartEvaluation;

        public StartEvaluationViewModel(IEvaluationService evaluationService, ILocalDbService localDbService,
            IDialogService dialogService, ILocalEvaluationService localEvaluationService, msg_StartEvaluation msgStartEvaluation)
        {
            _evaluationService = evaluationService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            _localEvaluationService = localEvaluationService;
            _msgStartEvaluation = msgStartEvaluation;
            QuestionOptionsList = new ObservableCollection<QuestionOptionItemModel>();
        }

        private RelayCommand<int> _LoadStartEvaluationCommand;
        private RelayCommand<QuestionOptionItemModel> _AnswerSelectedCommand;
        private RelayCommand _NextQuestionCommand;
        private RelayCommand _PreviousQuestionCommand;
        private RelayCommand _SubmitEvaluationCommand;

        private int _evalid;
        private int _questionid;
        private string _category;
        private string _subject;
        private decimal _totalscore;
        private int _noofquestions;
        private string _questionText;
        private int _timeAllowed;
        private string _imagePath;

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
        public int QuestionId
        {
            get
            {
                return _questionid;
            }
            set
            {
                Set(() => QuestionId, ref _questionid, value);
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
                Set(() => TotalScore, ref _totalscore, value);
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
        public string QuestionText
        {
            get
            {
                return _questionText;
            }
            set
            {
                Set(() => QuestionText, ref _questionText, value);
            }
        }
        public int TimeAllowed
        {
            get
            {
                return _timeAllowed;
            }
            set
            {
                Set(() => TimeAllowed, ref _timeAllowed, value);
            }
        }
        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                Set(() => ImagePath, ref _imagePath, value);
            }
        }
        public ObservableCollection<QuestionOptionItemModel> QuestionOptionsList
        {
            get;
            private set;
        }
        public RelayCommand<int> LoadStartEvaluationCommand
        {
            get
            {
                return _LoadStartEvaluationCommand
                    ?? (_LoadStartEvaluationCommand = new RelayCommand<int>(
                    async evalId =>
                    {
                        try
                        {
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                //EvalId = _msgStartEvaluation._evalId; RaisePropertyChanged(() => EvalId);
                                EvalId = evalId; RaisePropertyChanged(() => EvalId);
                                //var questions = await _localEvaluationService.getQuestions(EvalId);
                                var questions = await _evaluationService.getQuestions(usertokenrec, EvalId);
                                var firstquestion = questions.First();
                                QuestionId = firstquestion.Id; RaisePropertyChanged(() => QuestionId);
                                Category = firstquestion.QuestionCategory; RaisePropertyChanged(() => Category);
                                ImagePath = firstquestion.ImagePath; RaisePropertyChanged(() => ImagePath);
                                TimeAllowed = firstquestion.Duration; RaisePropertyChanged(() => TimeAllowed);
                                Subject = questions.First().Subject; RaisePropertyChanged(() => Subject);
                                TotalScore = 0.00m; RaisePropertyChanged(() => TotalScore);
                                NoOfQuestions = questions.Count(); RaisePropertyChanged(() => NoOfQuestions);
                                QuestionText = firstquestion.QuestionText; RaisePropertyChanged(() => QuestionText);
                                //_localEvaluationService.updateMyEvaluation(TotalScore, EvalId);
                                var questionoptions = await _localEvaluationService.getQuestionOptions(QuestionId);
                                foreach (var item in questionoptions)
                                {
                                    var option = new QuestionOptionItemModel();
                                    option.Id = item.Id;
                                    option.QuestionId = item.QuestionId;
                                    option.OptionText = item.OptionText;
                                    option.IsAnswer = item.IsAnswer;
                                    QuestionOptionsList.Add(option);
                                }
                                RaisePropertyChanged(() => QuestionOptionsList);

                            }
                        }
                        catch (System.Exception ex)
                        {
                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }
        public RelayCommand<QuestionOptionItemModel> AnswerSelectedCommand
        {
            get
            {
                return _AnswerSelectedCommand
                    ?? (_AnswerSelectedCommand = new RelayCommand<QuestionOptionItemModel>(
                    async optionselected =>
                    {
                        try
                        {
                            var questions =  _localEvaluationService.getQuestions(EvalId).Result.OrderBy(a => a.Id);
                            if (optionselected.IsAnswer == true)
                            {
                                var mark = decimal.Parse(questions.First().Mark.ToString());
                                TotalScore += mark; RaisePropertyChanged(() => TotalScore);
                                await _localEvaluationService.updateMyEvaluation(TotalScore, EvalId);
                            }
                            else
                            {
                                TotalScore += 0.00m; RaisePropertyChanged(() => TotalScore);
                            }
                            QuestionId += 1; RaisePropertyChanged(() => QuestionId);
                            if (QuestionId != questions.Last().Id)
                            {
                                var question = questions.FirstOrDefault(a => a.Id == QuestionId);
                                QuestionText = question.QuestionText; RaisePropertyChanged(() => QuestionText);
                                ImagePath = question.ImagePath; RaisePropertyChanged(() => ImagePath);
                                var questionoptions = _localEvaluationService.getQuestionOptions(QuestionId).Result.OrderBy(a => a.Id);
                                foreach (var item in questionoptions)
                                {
                                    var optionrec = new QuestionOptionItemModel();
                                    optionrec.Id = item.Id;
                                    optionrec.QuestionId = item.QuestionId;
                                    optionrec.OptionText = item.OptionText;
                                    optionrec.IsAnswer = item.IsAnswer;
                                    QuestionOptionsList.Add(optionrec);
                                }
                                RaisePropertyChanged(() => QuestionOptionsList);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }
        public RelayCommand NextQuestionCommand
        {
            get
            {
                return _NextQuestionCommand
                    ?? (_NextQuestionCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            var questions = _localEvaluationService.getQuestions(EvalId).Result.OrderBy(a => a.Id);
                            QuestionId += 1; RaisePropertyChanged(() => QuestionId);
                        if (QuestionId != questions.Last().Id || questions != null)
                            {
                                var question = questions.FirstOrDefault(a => a.Id == QuestionId);
                                QuestionText = question.QuestionText; RaisePropertyChanged(() => QuestionText);
                                ImagePath = question.ImagePath; RaisePropertyChanged(() => ImagePath);
                                var questionoptions = _localEvaluationService.getQuestionOptions(QuestionId).Result.OrderBy(a => a.Id);
                                foreach (var item in questionoptions)
                                {
                                    var optionrec = new QuestionOptionItemModel();
                                    optionrec.Id = item.Id;
                                    optionrec.QuestionId = item.QuestionId;
                                    optionrec.OptionText = item.OptionText;
                                    optionrec.IsAnswer = item.IsAnswer;
                                    QuestionOptionsList.Add(optionrec);
                                }
                                RaisePropertyChanged(() => QuestionOptionsList);
                            }
                        }
                        catch (System.Exception ex)
                        {

                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }
        public RelayCommand PreviousQuestionCommand
        {
            get
            {
                return _PreviousQuestionCommand
                    ?? (_PreviousQuestionCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            var questions = _localEvaluationService.getQuestions(EvalId).Result.OrderBy(a => a.Id);
                            QuestionId -= 1; RaisePropertyChanged(() => QuestionId);
                            if (QuestionId != questions.Last().Id)
                            {
                                var question = questions.FirstOrDefault(a => a.Id == QuestionId);
                                QuestionText = question.QuestionText; RaisePropertyChanged(() => QuestionText);
                                ImagePath = question.ImagePath; RaisePropertyChanged(() => ImagePath);
                                var questionoptions = _localEvaluationService.getQuestionOptions(QuestionId).Result.OrderBy(a => a.Id);
                                foreach (var item in questionoptions)
                                {
                                    var optionrec = new QuestionOptionItemModel();
                                    optionrec.Id = item.Id;
                                    optionrec.QuestionId = item.QuestionId;
                                    optionrec.OptionText = item.OptionText;
                                    optionrec.IsAnswer = item.IsAnswer;
                                    QuestionOptionsList.Add(optionrec);
                                }
                                RaisePropertyChanged(() => QuestionOptionsList);
                            }
                        }
                        catch (System.Exception ex)
                        {

                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }
        public RelayCommand SubmitEvaluationCommand
        {
            get
            {
                return _SubmitEvaluationCommand
                    ?? (_SubmitEvaluationCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            var questions = _localEvaluationService.getQuestions(EvalId).Result.OrderBy(a => a.Id);
                            if (QuestionId == questions.Last().Id)
                            {
                                var usertokenrec = _localDbService.getAccessToken();
                                await _localEvaluationService.updateMyEvaluation(TotalScore, EvalId);
                                var myeval = await _localEvaluationService.getMyEvaluation(EvalId);
                                var updateresult = await _evaluationService.updateUserEvaluation(usertokenrec, EvalId, myeval.TotalScore, myeval.AvailableTotal,
                                    myeval.NoOfQuestions, myeval.Duration);
                                var myevaldbmodel = new MyEvaluation()
                                {
                                    TotalScore = updateresult.TotalScore,
                                    AvailableTotal = updateresult.AvailableTotal,
                                    NoOfQuestions = updateresult.NoOfQuestions,
                                    PassPercentage = updateresult.PassPercentage,
                                    EvalId = EvalId,
                                    Modified = updateresult.Modified
                                };
                                await _localEvaluationService.saveMyEvaluation(myevaldbmodel);
                                Messenger.Default.Send<msg_MyEvaluationDetail>(new msg_MyEvaluationDetail(updateresult));
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

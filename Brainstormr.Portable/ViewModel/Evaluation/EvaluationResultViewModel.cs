using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.LocalDb.Evaluation;
using Brainstormr.Portable.Model.Evaluation;
using Brainstormr.Portable.ViewModel.Evaluation.msg;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.Evaluation
{
    public class EvaluationResultViewModel : ViewModelBase
    {
        //IEvaluationService _evaluationService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;
        ILocalEvaluationService _localEvaluationService;
        msg_EvaluationResult _msgEvaluationResult;
        public EvaluationResultViewModel(ILocalDbService localDbService,
            IDialogService dialogService, ILocalEvaluationService localEvaluationService, msg_EvaluationResult msgEvaluationResult)
        {
            _localDbService = localDbService;
            _dialogService = dialogService;
            _localEvaluationService = localEvaluationService;
            _msgEvaluationResult = msgEvaluationResult;
        }

        private RelayCommand _LoadEvaluationResultCommand;

        private string _questionCategory;
        private string _subject;
        private int _year;
        private int _noOfQuestions;
        private int _duration;
        private decimal _totalScore;
        private decimal _availableTotal;
        private decimal _passPercentage;
        public string QuestionCategory
        {
            get
            {
                return _questionCategory;
            }
            set
            {
                Set(() => QuestionCategory, ref _questionCategory, value);
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
        public int Year
        {
            get
            {
                return _year;
            }
            set
            {
                Set(() => Year, ref _year, value);
            }
        }
        public int NoOfQuestions
        {
            get
            {
                return _noOfQuestions;
            }
            set
            {
                Set(() => NoOfQuestions, ref _noOfQuestions, value);
            }
        }
        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                Set(() => Duration, ref _duration, value);
            }
        }
        public decimal TotalScore
        {
            get
            {
                return _totalScore;
            }
            set
            {
                Set(() => TotalScore, ref _totalScore, value);
            }
        }
        public decimal AvailableTotal
        {
            get
            {
                return _availableTotal;
            }
            set
            {
                Set(() => AvailableTotal, ref _availableTotal, value);
            }
        }
        public decimal PassPercentage
        {
            get
            {
                return _passPercentage;
            }
            set
            {
                Set(() => PassPercentage, ref _passPercentage, value);
            }
        }
        public RelayCommand LoadEvaluationResultCommand
        {
            get
            {
                return _LoadEvaluationResultCommand
                    ?? (_LoadEvaluationResultCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            //QuestionCategory = msg_Detail.counsellor_dto.Id; RaisePropertyChanged(() => QuestionCategory);
                            //Messenger.Default.Send<msg_userloggedinAlready>(new msg_userloggedinAlready { loginresult = returnresult });

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

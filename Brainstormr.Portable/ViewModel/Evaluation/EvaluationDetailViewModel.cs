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
    public class EvaluationDetailViewModel : ViewModelBase
    {
        msg_EvaluationDetail _msgEvalDetail;
        IDialogService _dialogService;

        public EvaluationDetailViewModel(msg_EvaluationDetail msgEvalDetail, IDialogService dialogService)
        {
            _msgEvalDetail = msgEvalDetail;
            _dialogService = dialogService;
        }

        private RelayCommand _OpenTakeEvaluationCommand;
        private int _evalid;
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

        public RelayCommand OpenTakeEvaluationCommand
        {
            get
            {
                return _OpenTakeEvaluationCommand
                    ?? (_OpenTakeEvaluationCommand = new RelayCommand (
                    async () =>
                    {
                        try
                        {
                            //EvalId = evalid;
                            var msg = new msg_TakeEvaluation(_msgEvalDetail.evaluation_dto);
                            //Messenger.Default.Send<msg_Transport<msg_TakeEvaluation>>(new msg_Transport<msg_TakeEvaluation>(msg));
                            Messenger.Default.Send<msg_TakeEvaluation>(new msg_TakeEvaluation(msg.evaluation_dto));
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

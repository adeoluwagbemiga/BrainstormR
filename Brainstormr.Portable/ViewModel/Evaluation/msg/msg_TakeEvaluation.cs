using Brainstormr.Portable.Model.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.Evaluation.msg
{
    public class msg_TakeEvaluation
    {
        public EvaluationItemModel evaluation_dto { get; private set; }
        public msg_TakeEvaluation(EvaluationItemModel _evaluation_dto)
        {
            evaluation_dto = _evaluation_dto;
        }
        //public int _evalId { get; set; }
        //public msg_TakeEvaluation(int evalId)
        //{
        //    _evalId = evalId;
        //}
    }
}

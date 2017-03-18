using Brainstormr.Portable.Model.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.Evaluation.msg
{
    public class msg_EvaluationDetail
    {
        public EvaluationItemModel evaluation_dto { get; private set; }
        public msg_EvaluationDetail(EvaluationItemModel _evaluation_dto)
        {
            evaluation_dto = _evaluation_dto;
        }
    }
}

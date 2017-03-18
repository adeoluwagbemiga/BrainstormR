using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.Evaluation.msg
{
    public class msg_EvaluationResult
    {
        public int _evalId { get; set; }
        public msg_EvaluationResult(int evalId)
        {
            _evalId = evalId;
        }
    }
}

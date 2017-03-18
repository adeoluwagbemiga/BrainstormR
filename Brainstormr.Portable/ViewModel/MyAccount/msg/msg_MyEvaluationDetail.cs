using Brainstormr.Portable.Model.MyAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.MyAccount.msg
{
    public class msg_MyEvaluationDetail
    {
        public MyEvaluationItemModel _myEval { get; set; }
        public msg_MyEvaluationDetail(MyEvaluationItemModel myEval)
        {
            _myEval = myEval;
        }
        //public int Id { get; set; }
        //public string UserEmail { get; set; }
        //public int QuestionId { get; set; }
        //public string QuestionCategory { get; set; }
        //public string Subject { get; set; }
        //public int Year { get; set; }
        //public int NoOfQuestions { get; set; }
        //public int Duration { get; set; }
        //public decimal TotalScore { get; set; }
        //public decimal AvailableTotal { get; set; }
        //public decimal PassPercentage { get; set; }
        //public string Created { get; set; }
        //public string Modified { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.Evaluation
{
    public class QuestionOptionItemModel
    {
        public int QuestionId { get; set; }
        public string OptionText { get; set; }
        public bool IsAnswer { get; set; }
        public int Id { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}

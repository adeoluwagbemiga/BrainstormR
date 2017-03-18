using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.Evaluation
{
    public class EvaluationItemModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public int NoOfQuestions { get; set; }
        public string ImagePath { get; set; }
    }
}

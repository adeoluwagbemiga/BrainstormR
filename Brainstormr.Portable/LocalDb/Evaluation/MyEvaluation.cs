using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.Evaluation
{
    [Table("MyEvaluations")]
    public class MyEvaluation
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int EvalId { get; set; }
        public string UserEmail { get; set; }
        public string QuestionCategory { get; set; }
        public string Subject { get; set; }
        public int Year { get; set; }
        public int NoOfQuestions { get; set; }
        public int Duration { get; set; }
        public decimal TotalScore { get; set; }
        public decimal AvailableTotal { get; set; }
        public decimal PassPercentage { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}

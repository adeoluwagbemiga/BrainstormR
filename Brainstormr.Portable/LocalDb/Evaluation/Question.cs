using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.Evaluation
{
    [Table("Questions")]
    public class Question
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public string QuestionCategory { get; set; }
        public string Subject { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public string QuestionText { get; set; }
        public decimal Mark { get; set; }
        public bool isInstruction { get; set; }
        //public object Image { get; set; }
        public string ImagePath { get; set; }
        //public object QuestionOptions { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.Evaluation
{
    public class EvaluationCategoryItemModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PictureName { get; set; }
        public string ImagePath { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}

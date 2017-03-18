using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.CareerCounselling
{
    public class InstructorSubjectReviewItemModel
    {
        public int Id { get; set; }
        public int InstructorId { get; set; }
        public string Subject { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public int Vote { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}

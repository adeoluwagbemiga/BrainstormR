using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.CareerCounselling
{
    [Table("InstructorSubjects")]
    public class InstructorSubject
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Subject { get; set; }
        public int InstructorId { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}

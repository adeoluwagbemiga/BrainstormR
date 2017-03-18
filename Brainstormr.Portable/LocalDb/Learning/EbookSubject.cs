using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.Learning
{
    [Table("EbookSubjects")]
    public class EbookSubject
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}

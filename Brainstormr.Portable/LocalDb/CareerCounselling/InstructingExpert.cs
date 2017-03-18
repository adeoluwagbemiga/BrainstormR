using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.CareerCounselling
{
    [Table("InstructingExperts")]
    public class InstructingExpert
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string About { get; set; }
        public string PictureName { get; set; }
        public string Portfolio { get; set; }
        public string University { get; set; }
        public string Degree { get; set; }
        public bool IsCounsellor { get; set; }
        public bool IsTutor { get; set; }
        public bool IsExpert { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}

using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.Learning
{
    [Table("Ebooks")]
    public class Ebook
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public bool Featured { get; set; }
        public decimal Amount { get; set; }
        public string FileName { get; set; }
        public string PreviewFile { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string PreviewPath { get; set; }
        public string FilePath { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}

using Brainstormr.Portable.Model.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.Learning.msg
{
    public class msg_EbookDetail
    {
        public EbookItemModel ebookdto { get; private set; }
        public msg_EbookDetail(EbookItemModel _ebookdto)
        {
            ebookdto = _ebookdto;
        }
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Category { get; set; }
        //public string Subject { get; set; }
        //public string Description { get; set; }
        //public string Author { get; set; }
        //public bool Featured { get; set; }
        //public int Amount { get; set; }
        //public string FileName { get; set; }
        //public string PreviewFile { get; set; }
        //public string ImageName { get; set; }
        //public string ImagePath { get; set; }
        //public string PreviewPath { get; set; }
        //public string FilePath { get; set; }
        //public DateTime Created { get; set; }
        //public DateTime Modified { get; set; }

    }
}

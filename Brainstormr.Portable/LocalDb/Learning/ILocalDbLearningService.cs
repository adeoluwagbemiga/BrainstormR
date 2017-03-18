using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.Learning
{
    public interface ILocalDbLearningService
    {
        Task saveEbook(Ebook ebookitem);
        Task saveEbookCategories(EbookCategory ebookcategitem);
        Task saveSubjects(EbookSubject ebooksubjectitem);
        Task<List<Ebook>> getAllEbooks();
        Task<List<EbookCategory>> getAllEbookCategories();
        Task<List<EbookSubject>> getAllEbookSubjects();
        Task<Ebook> getEbook(int ebookId);
    }
}

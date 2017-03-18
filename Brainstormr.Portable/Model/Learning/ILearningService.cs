using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.Learning
{
    public interface ILearningService
    {
        Task<IEnumerable<EbookItemModel>> getAllEbooks(string accessToken);
        Task<IEnumerable<EbookCategoryItemModel>> getAllEbookCategories(string accessToken);
        Task<IEnumerable<EbookSubjectItemModel>> getAllEbookSubjects(string accessToken);
        
    }
}

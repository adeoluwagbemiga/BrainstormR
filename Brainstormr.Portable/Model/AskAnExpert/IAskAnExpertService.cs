using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.AskAnExpert
{
    public interface IAskAnExpertService
    {
        Task<IEnumerable<InstructingExpertItemModel>> getAllExpert(string accessToken);
        Task<IEnumerable<InstructorSubjectItemModel>> getAllInstructorSubjects(string accessToken);
        Task<IEnumerable<InstructorSubjectReviewItemModel>> getAllInstructorSubjectReviews(string accessToken);
        Task<IEnumerable<InstructorSubjectReviewItemModel>> getInstructorSubjectReviews(string accessToken, int instructorId);
        Task<IEnumerable<InstructorSubjectItemModel>> getInstructorSubjects(string accessToken, int instructorId);
        Task<InstructingExpertItemModel> getExpert(string accessToken, int instructorId);
        Task<string> sendMessage(string accessToken, int instructorId, string subject, string message, string category);
    }
}

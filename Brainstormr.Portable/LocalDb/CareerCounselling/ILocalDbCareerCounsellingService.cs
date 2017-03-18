using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.CareerCounselling
{
    public interface ILocalDbCareerCounsellingService
    {
        Task saveInstructingExpert(InstructingExpert intructoritem);
        Task saveInstructorSubject(InstructorSubject intructorsubjectitem);
        Task saveInstructorSubjectReview(InstructorSubjectReview intructorsubjectreviewitem);
        Task<List<InstructingExpert>> getAllInstructingExperts();
        Task<List<InstructorSubject>> getAllInstructorSubjects(int instrExpertId);
        Task<List<InstructorSubjectReview>> getAllInstructorSubjectReviews(int instrExpertId);
        Task<InstructingExpert> getInstructingExpert(int instrExpertId);

    }
}

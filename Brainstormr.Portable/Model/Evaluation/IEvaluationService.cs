using Brainstormr.Portable.Model.MyAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.Evaluation
{
    public interface IEvaluationService
    {
        Task<IEnumerable<EvaluationItemModel>> getAllEvaluations(string accessToken);
        Task<IEnumerable<EvaluationCategoryItemModel>> getAllEvaluationCategories(string accessToken);
        Task<IEnumerable<EvaluationYearItemModel>> getAllEvaluationYears(string accessToken);
        Task<MySubscriptionItemModel> updateUserSubscription(string accessToken, int subscriptionId, int duration);
        Task<List<QuestionItemModel>> getQuestions(string accessToken, int evalId);
        Task<IEnumerable<QuestionOptionItemModel>> getQuestionOptions(string accessToken, int questionId);
        Task<MyEvaluationItemModel> updateUserEvaluation(string accessToken, int evalId, decimal totalScore, decimal availableTotal, int noOfQuestions, int duration);
    }
}

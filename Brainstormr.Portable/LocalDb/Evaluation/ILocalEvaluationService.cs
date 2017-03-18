using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.Evaluation
{
    public interface ILocalEvaluationService
    {
        Task saveQuestions(Question questionItem);
        Task<List<Question>> getQuestions(int evalId);
        Task saveQuestionOptions(QuestionOption questionOptionItem);
        Task<List<QuestionOption>> getQuestionOptions(int questionId);
        Task saveMyEvaluation(MyEvaluation myEvalItem);
        Task updateMyEvaluation(decimal totalScore, int evalId);
        Task<MyEvaluation> getMyEvaluation(int evalId);
    }
}

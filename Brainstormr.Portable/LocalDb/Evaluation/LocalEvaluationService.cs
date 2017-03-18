using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.Evaluation
{
    public class LocalEvaluationService : ILocalEvaluationService
    {
        SQLiteConnection db;
        public LocalEvaluationService(ISQLiteConnectionService _SQLiteConnectionService)
        {
            db = _SQLiteConnectionService.getConnection();
            db.CreateTable<Question>();
            db.CreateTable<QuestionOption>();
            db.CreateTable<MyEvaluation>();
        }

        public async Task<MyEvaluation> getMyEvaluation(int evalId)
        {
            return await Task.Run(() =>
            {

                MyEvaluation newrec = new MyEvaluation();
                var myevalrec = db.Table<MyEvaluation>().FirstOrDefault(a => a.EvalId == evalId);
                return myevalrec == null ? newrec : myevalrec;
            });
        }

        public async Task<List<QuestionOption>> getQuestionOptions(int questionId)
        {
            return await Task.Run(() =>
            {

                List<QuestionOption> newlist = new List<QuestionOption>();
                var optionslist = db.Table<QuestionOption>().Where(a => a.QuestionId == questionId).ToList();
                foreach (var item in optionslist)
                {
                    newlist.Add(item);
                }
                return newlist;
            });
        }

        public async Task<List<Question>> getQuestions(int evalId)
        {
            return await Task.Run(() =>
            {
                List<Question> newlist = new List<Question>();
                var questionslist = db.Table<Question>().Where(a => a.EvaluationId == evalId).ToList();
                foreach (var item in questionslist)
                {
                    newlist.Add(item);
                }
                return newlist;
            });
        }

        public async Task saveMyEvaluation(MyEvaluation myEvalItem)
        {
            await Task.Run(() =>
            {
                var myeval = db.Table<MyEvaluation>().FirstOrDefault(a => a.EvalId == myEvalItem.EvalId);
                if (myeval == null)
                {
                    myeval = new MyEvaluation();
                    myeval.AvailableTotal = myEvalItem.AvailableTotal;
                    myeval.Created = myEvalItem.Created;
                    myeval.Duration = myEvalItem.Duration;
                    myeval.EvalId = myEvalItem.EvalId;
                    myeval.Modified = myEvalItem.Modified;
                    myeval.NoOfQuestions = myEvalItem.NoOfQuestions;
                    myeval.PassPercentage = myEvalItem.PassPercentage;
                    myeval.QuestionCategory = myEvalItem.QuestionCategory;
                    myeval.Subject = myEvalItem.Subject;
                    myeval.TotalScore = myEvalItem.TotalScore;
                    myeval.UserEmail = myEvalItem.UserEmail;
                    myeval.Year = myEvalItem.Year;

                    db.Insert(myeval);
                }
                else
                {
                    myeval.AvailableTotal = myEvalItem.AvailableTotal;
                    myeval.Duration = myEvalItem.Duration;
                    myeval.Modified = myEvalItem.Modified;
                    myeval.PassPercentage = myEvalItem.PassPercentage;
                    myeval.QuestionCategory = myEvalItem.QuestionCategory;
                    myeval.Subject = myEvalItem.Subject;
                    myeval.TotalScore = myEvalItem.TotalScore;
                    myeval.Year = myEvalItem.Year;
                    db.Update(myeval);
                }
            });
        }

        public async Task saveQuestionOptions(QuestionOption questionOptionItem)
        {
            await Task.Run(() =>
            {
                var myquestionoption = db.Table<QuestionOption>().FirstOrDefault(a => a.QuestionId == questionOptionItem.QuestionId);
                if (myquestionoption == null)
                {
                    myquestionoption = new QuestionOption()
                    {
                        Created = questionOptionItem.Created,
                        QuestionId = questionOptionItem.QuestionId,
                        IsAnswer = questionOptionItem.IsAnswer,
                        Modified = questionOptionItem.Modified,
                        OptionText = questionOptionItem.OptionText,
                         Id = questionOptionItem.Id
                    };

                    db.Insert(myquestionoption);
                }
                else
                {
                    myquestionoption.IsAnswer = questionOptionItem.IsAnswer;
                    myquestionoption.Modified = questionOptionItem.Modified;
                    myquestionoption.OptionText = questionOptionItem.OptionText;

                    db.Update(myquestionoption);
                }
            });
        }

        public async Task saveQuestions(Question questionItem)
        {
            await Task.Run(() =>
            {

                var myquestion = db.Table<Question>().FirstOrDefault(a => a.EvaluationId == questionItem.EvaluationId);
                if (myquestion == null)
                {
                    myquestion = new Question()
                    {
                        Created = questionItem.Created,
                        Duration = questionItem.Duration,
                        EvaluationId = questionItem.EvaluationId,
                        ImagePath = questionItem.ImagePath,
                        isInstruction = questionItem.isInstruction,
                        Mark = questionItem.Mark,
                        Modified = questionItem.Modified,
                        QuestionCategory = questionItem.QuestionCategory,
                        QuestionText = questionItem.QuestionText,
                        Subject = questionItem.Subject,
                        Year = questionItem.Year,
                        Id = questionItem.Id
                    };

                    db.Insert(myquestion);
                }
                else
                {
                    myquestion.Duration = questionItem.Duration;
                    myquestion.Modified = questionItem.Modified;
                    myquestion.ImagePath = questionItem.ImagePath;
                    myquestion.isInstruction = questionItem.isInstruction;
                    myquestion.Mark = questionItem.Mark;
                    myquestion.QuestionCategory = questionItem.QuestionCategory;
                    myquestion.QuestionText = questionItem.QuestionText;

                    db.Update(myquestion);
                }
            });
        }

        public async Task updateMyEvaluation(decimal totalScore, int evalId)
        {
            throw new NotImplementedException();
        }
    }
}

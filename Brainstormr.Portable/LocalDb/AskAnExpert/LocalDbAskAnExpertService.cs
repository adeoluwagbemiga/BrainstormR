using Brainstormr.Portable.LocalDb.CareerCounselling;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.AskAnExpert
{
    public class LocalDbAskAnExpertService : ILocalDbAskAnExpertService
    {
        SQLiteConnection db;
        public LocalDbAskAnExpertService(ISQLiteConnectionService _SQLiteConnectionService)
        {
            db = _SQLiteConnectionService.getConnection();
            db.CreateTable<InstructingExpert>();
            db.CreateTable<InstructorSubjectReview>();
            db.CreateTable<InstructorSubject>();
        }

        public async Task<List<InstructingExpert>> getAllInstructingExperts()
        {
            return await Task.Run(() =>
            {
                List<InstructingExpert> newlist = new List<InstructingExpert>();
                var instructinexpertlist = db.Table<InstructingExpert>().ToList();
                foreach (var item in instructinexpertlist)
                {
                    newlist.Add(item);
                }
                return newlist;
            });
        }

        public async Task<List<InstructorSubjectReview>> getAllInstructorSubjectReviews(int instrExpertId)
        {
            return await Task.Run(() =>
            {
                List<InstructorSubjectReview> newlist = new List<InstructorSubjectReview>();
                var instructorsubrevlist = db.Table<InstructorSubjectReview>().Where(a => a.InstructorId == instrExpertId).ToList();
                foreach (var item in instructorsubrevlist)
                {
                    newlist.Add(item);
                }
                return newlist;
            });
        }

        public async Task<List<InstructorSubject>> getAllInstructorSubjects(int instrExpertId)
        {
            return await Task.Run(() =>
            {
                List<InstructorSubject> newlist = new List<InstructorSubject>();
                var instructorsublist = db.Table<InstructorSubject>().Where(a => a.InstructorId == instrExpertId).ToList();
                foreach (var item in instructorsublist)
                {
                    newlist.Add(item);
                }
                return newlist;
            });
        }

        public async Task<InstructingExpert> getInstructingExpert(int instrExpertId)
        {
            return await Task.Run(() =>
            {
                var instructingexpert = db.Table<InstructingExpert>().FirstOrDefault(a => a.Id == instrExpertId);
                return instructingexpert;
            });
        }

        public async Task saveInstructingExpert(InstructingExpert intructoritem)
        {
            await Task.Run(() =>
            {
                var expert = db.Table<InstructingExpert>().FirstOrDefault(a => a.Id == intructoritem.Id);
                if (expert == null)
                {
                    expert = new InstructingExpert();
                    expert.About = intructoritem.About;
                    expert.Created = intructoritem.Created;
                    expert.Degree = intructoritem.Degree;
                    expert.Email = intructoritem.Email;
                    expert.Id = intructoritem.Id;
                    expert.ImagePath = intructoritem.ImagePath;
                    expert.IsCounsellor = intructoritem.IsCounsellor;
                    expert.IsExpert = intructoritem.IsExpert;
                    expert.IsTutor = intructoritem.IsTutor;
                    expert.Modified = intructoritem.Modified;
                    expert.Name = intructoritem.Name;
                    expert.Phone = intructoritem.Phone;
                    expert.PictureName = intructoritem.PictureName;
                    expert.Portfolio = intructoritem.Portfolio;
                    expert.University = intructoritem.University;

                    db.Insert(expert);
                }
                else
                {
                    expert.About = intructoritem.About;
                    expert.Degree = intructoritem.Degree;
                    expert.Email = intructoritem.Email;
                    expert.ImagePath = intructoritem.ImagePath;
                    expert.IsCounsellor = intructoritem.IsCounsellor;
                    expert.IsExpert = intructoritem.IsExpert;
                    expert.IsTutor = intructoritem.IsTutor;
                    expert.Modified = intructoritem.Modified;
                    expert.Name = intructoritem.Name;
                    expert.Phone = intructoritem.Phone;
                    expert.PictureName = intructoritem.PictureName;
                    expert.Portfolio = intructoritem.Portfolio;
                    expert.University = intructoritem.University;

                    db.Update(expert);
                }
            });
        }

        public async Task saveInstructorSubject(InstructorSubject intructorsubjectitem)
        {
            await Task.Run(() =>
            {
                var subject = db.Table<InstructorSubject>().FirstOrDefault(a => a.Id == intructorsubjectitem.Id);
                if (subject == null)
                {
                    subject = new InstructorSubject();
                    subject.Created = intructorsubjectitem.Created;
                    subject.Id = intructorsubjectitem.Id;
                    subject.InstructorId = intructorsubjectitem.InstructorId;
                    subject.Modified = intructorsubjectitem.Modified;
                    subject.Subject = intructorsubjectitem.Subject;

                    db.Insert(subject);
                }
                else
                {
                    subject.Modified = intructorsubjectitem.Modified;
                    subject.Subject = intructorsubjectitem.Subject;

                    db.Update(subject);
                }
            });
        }

        public async Task saveInstructorSubjectReview(InstructorSubjectReview intructorsubjectreviewitem)
        {
            await Task.Run(() =>
            {
                var subjectrev = db.Table<InstructorSubjectReview>().FirstOrDefault(a => a.Id == intructorsubjectreviewitem.Id);
                if (subjectrev == null)
                {
                    subjectrev = new InstructorSubjectReview();
                    subjectrev.Created = intructorsubjectreviewitem.Created;
                    subjectrev.Id = intructorsubjectreviewitem.Id;
                    subjectrev.InstructorId = intructorsubjectreviewitem.InstructorId;
                    subjectrev.Modified = intructorsubjectreviewitem.Modified;
                    subjectrev.Subject = intructorsubjectreviewitem.Subject;
                    subjectrev.Comment = intructorsubjectreviewitem.Comment;
                    subjectrev.Username = intructorsubjectreviewitem.Username;
                    subjectrev.Vote = intructorsubjectreviewitem.Vote;

                    db.Insert(subjectrev);
                }
                else
                {
                    subjectrev.Modified = intructorsubjectreviewitem.Modified;
                    subjectrev.Subject = intructorsubjectreviewitem.Subject;
                    subjectrev.Comment = intructorsubjectreviewitem.Comment;
                    subjectrev.Username = intructorsubjectreviewitem.Username;
                    subjectrev.Vote = intructorsubjectreviewitem.Vote;

                    db.Update(subjectrev);
                }
            });
        }
    }
}

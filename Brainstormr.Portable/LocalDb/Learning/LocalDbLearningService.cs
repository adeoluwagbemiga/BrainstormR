using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.Learning
{
    public class LocalDbLearningService : ILocalDbLearningService
    {
        SQLiteConnection db;
        public LocalDbLearningService(ISQLiteConnectionService _SQLiteConnectionService)
        {
            db = _SQLiteConnectionService.getConnection();
            db.CreateTable<Ebook>();
            db.CreateTable<EbookCategory>();
            db.CreateTable<EbookSubject>();
        }
        public async Task<List<EbookCategory>> getAllEbookCategories()
        {
            return await Task.Run(() =>
            {
                List<EbookCategory> newlist = new List<EbookCategory>();
                var ebookcatglist = db.Table<EbookCategory>().ToList();
                foreach (var item in ebookcatglist)
                {
                    newlist.Add(item);
                }
                return newlist;
            });
        }

        public async Task<List<Ebook>> getAllEbooks()
        {
            return await Task.Run(() =>
            {
                List<Ebook> newlist = new List<Ebook>();
                var ebooklist = db.Table<Ebook>().ToList();
                foreach (var item in ebooklist)
                {
                    newlist.Add(item);
                }
                return newlist;
            });
        }

        public async Task<List<EbookSubject>> getAllEbookSubjects()
        {
            return await Task.Run(() =>
            {
                List<EbookSubject> newlist = new List<EbookSubject>();
                var ebooksubjectlist = db.Table<EbookSubject>().ToList();
                foreach (var item in ebooksubjectlist)
                {
                    newlist.Add(item);
                }
                return newlist;
            });
        }

        public async Task<Ebook> getEbook(int ebookId)
        {
            return await Task.Run(() =>
            {
                var ebook = db.Table<Ebook>().FirstOrDefault(a => a.Id == ebookId);
                return ebook;
            });
        }

        public async Task saveEbook(Ebook ebookitem)
        {
            await Task.Run(() =>
            {
                var ebook = db.Table<Ebook>().FirstOrDefault(a => a.Id == ebookitem.Id);
                if (ebook == null)
                {
                    ebook = new Ebook();
                    ebook.Amount = ebookitem.Amount;
                    ebook.Author = ebookitem.Author;
                    ebook.Category = ebookitem.Category;
                    ebook.Created = ebookitem.Created;
                    ebook.Description = ebookitem.Description;
                    ebook.Featured = ebookitem.Featured;
                    ebook.FileName = ebookitem.FileName;
                    ebook.FilePath = ebookitem.FilePath;
                    ebook.Id = ebookitem.Id;
                    ebook.ImageName = ebookitem.ImageName;
                    ebook.ImagePath = ebookitem.ImagePath;
                    ebook.Modified = ebookitem.Modified;
                    ebook.Name = ebookitem.Name;
                    ebook.PreviewFile = ebookitem.PreviewFile;
                    ebook.PreviewPath = ebookitem.PreviewPath;
                    ebook.Subject = ebookitem.Subject;

                    db.Insert(ebook);
                }
                else
                {
                    ebook.Amount = ebookitem.Amount;
                    ebook.Author = ebookitem.Author;
                    ebook.Category = ebookitem.Category;
                    ebook.Description = ebookitem.Description;
                    ebook.Featured = ebookitem.Featured;
                    ebook.FileName = ebookitem.FileName;
                    ebook.FilePath = ebookitem.FilePath;
                    ebook.ImageName = ebookitem.ImageName;
                    ebook.ImagePath = ebookitem.ImagePath;
                    ebook.Modified = ebookitem.Modified;
                    ebook.Name = ebookitem.Name;
                    ebook.PreviewFile = ebookitem.PreviewFile;
                    ebook.PreviewPath = ebookitem.PreviewPath;
                    ebook.Subject = ebookitem.Subject;

                    db.Update(ebook);
                }
            });
            
        }

        public async Task saveEbookCategories(EbookCategory ebookcategitem)
        {
            await Task.Run(() =>
            {
                var ebookcateg = db.Table<EbookCategory>().FirstOrDefault(a => a.Id == ebookcategitem.Id);
                if (ebookcateg == null)
                {
                    ebookcateg = new EbookCategory();
                    ebookcateg.Created = ebookcategitem.Created;
                    ebookcateg.Id = ebookcategitem.Id;
                    ebookcateg.Modified = ebookcategitem.Modified;
                    ebookcateg.Name = ebookcategitem.Name;

                    db.Insert(ebookcateg);
                }
                else
                {
                    ebookcateg.Modified = ebookcategitem.Modified;
                    ebookcateg.Name = ebookcategitem.Name;

                    db.Update(ebookcateg);
                }
            });
        }

        public async Task saveSubjects(EbookSubject ebooksubjectitem)
        {
            await Task.Run(() =>
            {
                var ebooksub = db.Table<EbookSubject>().FirstOrDefault(a => a.Id == ebooksubjectitem.Id);
                if (ebooksub == null)
                {
                    ebooksub = new EbookSubject();
                    ebooksub.Created = ebooksubjectitem.Created;
                    ebooksub.Id = ebooksubjectitem.Id;
                    ebooksub.Modified = ebooksubjectitem.Modified;
                    ebooksub.Name = ebooksubjectitem.Name;

                    db.Insert(ebooksub);
                }
                else
                {
                    ebooksub.Modified = ebooksubjectitem.Modified;
                    ebooksub.Name = ebooksubjectitem.Name;

                    db.Update(ebooksub);
                }
            });

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brainstormr.Portable.Model.Login;
using SQLite.Net;

namespace Brainstormr.Portable.LocalDb
{
    public class LocalDbService : ILocalDbService
    {
        SQLiteConnection db;
        public LocalDbService(ISQLiteConnectionService _SQLiteConnectionService)
        {
            db = _SQLiteConnectionService.getConnection();
            db.CreateTable<UserToken>();
            db.CreateTable<UserInfo>();
            db.CreateTable<ExternalLoginInfo>();
        }
        public string getAccessToken()
        {
            var tokenrec = db.Table<UserToken>().FirstOrDefault();
            return tokenrec == null ? "" : tokenrec.AccessToken;
        }

        public UserInfo getUserInfo()
        {
            var userrec = db.Table<UserInfo>().FirstOrDefault();
            return userrec == null ? new UserInfo() : userrec;
        }

        public TokenResponseModel getUserTokenInfo()
        {
            var usertokenrec = db.Table<TokenResponseModel>().FirstOrDefault();
            return usertokenrec == null ? new TokenResponseModel() : usertokenrec;
        }

        public void saveExternalLoginDetails(ExternalLoginInfo externalLoginDetails)
        {
            var externalloginrec = db.Table<ExternalLoginInfo>().FirstOrDefault();
            if (externalloginrec == null)
            {
                externalloginrec = new ExternalLoginInfo();
                externalloginrec.ExternalAccessToken = externalLoginDetails.ExternalAccessToken;
                externalloginrec.UserName = externalLoginDetails.UserName;
                externalloginrec.Name = externalLoginDetails.Name;
                externalloginrec.Picture = externalLoginDetails.Picture;
                externalloginrec.Provider = externalLoginDetails.Provider;
                db.Insert(externalloginrec);
            }
            else
            {
                externalloginrec.ExternalAccessToken = externalLoginDetails.ExternalAccessToken;
                externalloginrec.Name = externalLoginDetails.Name;
                externalloginrec.Picture = externalLoginDetails.Picture;
                externalloginrec.Provider = externalLoginDetails.Provider;
                db.Update(externalloginrec);
            }
        }

        public void saveToken(string accesstoken, string username)
        {
            var tokenrec = db.Table<UserToken>().FirstOrDefault();
            if (tokenrec == null)
            {
                tokenrec = new UserToken();
                tokenrec.AccessToken = accesstoken;
                tokenrec.UserName = username;
                db.Insert(tokenrec);
            }
            else
            {
                tokenrec.AccessToken = accesstoken;
                db.Update(tokenrec);
            }
        }

        public void updateUserInfo(UserInfo user)
        {
            var userrec = db.Table<UserInfo>().FirstOrDefault();
            if (userrec == null)
            {
                userrec = new UserInfo();
                userrec.UserName = user.UserName;
                userrec.Name = user.Name;
                userrec.AvatarUrl = user.AvatarUrl;
                userrec.City = user.City;
                userrec.Country = user.Country;
                userrec.Course = user.Course;
                userrec.HasRegistered = user.HasRegistered;
                userrec.Institution = user.Institution;
                userrec.LoginProvider = user.LoginProvider;
                userrec.Profession = user.Profession;
                db.Insert(userrec);
            }
            else
            {
                userrec.Name = user.Name;
                userrec.AvatarUrl = user.AvatarUrl;
                userrec.City = user.City;
                userrec.Country = user.Country;
                userrec.Course = user.Course;
                userrec.HasRegistered = user.HasRegistered;
                userrec.Institution = user.Institution;
                userrec.LoginProvider = user.LoginProvider;
                userrec.Profession = user.Profession;
                db.Update(userrec);
            }
        }
    }
}

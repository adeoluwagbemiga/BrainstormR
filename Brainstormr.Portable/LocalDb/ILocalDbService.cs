using Brainstormr.Portable.Model.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb
{
    public interface ILocalDbService
    {
        string getAccessToken();
        void saveToken(string accesstoken, string username);
        void updateUserInfo(UserInfo user);
        UserInfo getUserInfo();
        TokenResponseModel getUserTokenInfo();
        void saveExternalLoginDetails(ExternalLoginInfo externalLoginDetails);

    }
}

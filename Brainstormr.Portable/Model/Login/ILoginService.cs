using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.Login
{
    public interface ILoginService
    {
        Task<TokenResponseModel> ValidateUser(string username, string password);
        Task logOutUser(string accesstoken);
        Task<UserInfoModel> getUserProfileinfo(string accesstoken);
        Task<TokenResponseModel> ValidateExternalLogin(string username, string provider, string name, string picture, string externalaccesstoken);
        //UserName, Provider, Name, Picture, ExternalAccessToken
    }
}

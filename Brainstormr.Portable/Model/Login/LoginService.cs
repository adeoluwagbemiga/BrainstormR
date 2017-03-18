using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.Login
{
    public class LoginService : ILoginService
    {
        string base_urladdress = GlobalVal.webapibaseurl;

        public async Task<UserInfoModel> getUserProfileinfo(string accesstoken)
        {
            try
            {
                UserInfoModel result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accesstoken));
                    var response = await client.GetAsync("/api/RemoteAccount/UserInfo");
                    result = await response.Content.ReadAsAsync<UserInfoModel>();
                }
                return result;
            }
            catch (Exception ex)
            {
                //throw new SecurityException("Error while loading user info: ", ex.Message);
                throw new SecurityException(ex.Message);
            }
        }

        public async Task logOutUser(string useraccesstoken)
        {
            string url = base_urladdress + "/api/remoteaccount/logout";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", useraccesstoken));
                var response = await client.PostAsync("/api/remoteaccount/logout", null);
            }
        }

        public async Task<TokenResponseModel> ValidateExternalLogin(string username, string provider, string name, string picture, string externalaccesstoken)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GlobalVal.webapibaseurl);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("UserName", WebUtility.HtmlEncode(username)),
                    new KeyValuePair<string, string>("Provider", WebUtility.HtmlEncode(provider)),
                    new KeyValuePair<string, string>("Name", WebUtility.HtmlEncode(name)),
                    new KeyValuePair<string, string>("Picture", WebUtility.HtmlEncode(picture)),
                    new KeyValuePair<string, string>("ExternalAccessToken", WebUtility.HtmlEncode(externalaccesstoken)),
                    new KeyValuePair<string, string>("Content-Type", "application/x-www-form-urlencoded")
                });
                var response = await client.PostAsync("/api/RemoteAccount/LoginExtenal", content);
                if (response.IsSuccessStatusCode)
                {
                    TokenResponseModel tokenResponse = await response.Content.ReadAsAsync<TokenResponseModel>();
                    return tokenResponse;
                }
                else
                {
                    throw new SecurityException(response.ReasonPhrase);
                }
            }
        }

        public async Task<TokenResponseModel> ValidateUser(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GlobalVal.webapibaseurl);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", WebUtility.HtmlEncode(username)),
                    new KeyValuePair<string, string>("password", WebUtility.HtmlEncode(password)),
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("Content-Type", "application/x-www-form-urlencoded")
                });
                var response = await client.PostAsync("/Token", content);
                if (response.IsSuccessStatusCode)
                {
                    TokenResponseModel tokenResponse = await response.Content.ReadAsAsync<TokenResponseModel>();
                    return tokenResponse;
                }
                else
                {
                    throw new SecurityException(response.ReasonPhrase);
                }
            }
        }
    }
}

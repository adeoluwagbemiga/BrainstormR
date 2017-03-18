using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.MyAccount
{
    public class MyAccountService : IMyAccountService
    {
        public async Task<IEnumerable<MyEbookItemModel>> getMyEbooks(string accessToken)
        {
            try
            {
                IEnumerable<MyEbookItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/MyEbooks");
                    result = await response.Content.ReadAsAsync<IEnumerable<MyEbookItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<IEnumerable<MyEvaluationItemModel>> getMyEvaluations(string accessToken)
        {
            try
            {
                IEnumerable<MyEvaluationItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/MyEvals");
                    result = await response.Content.ReadAsAsync<IEnumerable<MyEvaluationItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<IEnumerable<MyMessageItemModel>> getMyMessages(string accessToken)
        {
            try
            {
                IEnumerable<MyMessageItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/MyMessages");
                    result = await response.Content.ReadAsAsync<IEnumerable<MyMessageItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<IEnumerable<MySubscriptionItemModel>> getMySubscriptions(string accessToken)
        {
            try
            {
                IEnumerable<MySubscriptionItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/MySubscriptions");
                    result = await response.Content.ReadAsAsync<IEnumerable<MySubscriptionItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.Learning
{
    public class LearningService : ILearningService
    {
        public async Task<IEnumerable<EbookCategoryItemModel>> getAllEbookCategories(string accessToken)
        {
            try
            {
                IEnumerable<EbookCategoryItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/EbookCategories");
                    result = await response.Content.ReadAsAsync<IEnumerable<EbookCategoryItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                //throw new SecurityException("Error while loading user info: ", ex.Message);
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<IEnumerable<EbookItemModel>> getAllEbooks(string accessToken)
        {
            try
            {
                IEnumerable<EbookItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/Ebooks");
                    result = await response.Content.ReadAsAsync<IEnumerable<EbookItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                //throw new SecurityException("Error while loading user info: ", ex.Message);
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<IEnumerable<EbookSubjectItemModel>> getAllEbookSubjects(string accessToken)
        {
            try
            {
                IEnumerable<EbookSubjectItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/Subjects");
                    result = await response.Content.ReadAsAsync<IEnumerable<EbookSubjectItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                //throw new SecurityException("Error while loading user info: ", ex.Message);
                throw new SecurityException(ex.Message);
            }
        }
    }
}

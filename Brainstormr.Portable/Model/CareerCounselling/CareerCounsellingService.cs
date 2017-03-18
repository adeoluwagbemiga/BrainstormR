using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.CareerCounselling
{
    public class CareerCounsellingService : ICareerCounsellingService
    {
        public async Task<IEnumerable<InstructingExpertItemModel>> getAllCounsellors(string accessToken)
        {
            try
            {
                IEnumerable<InstructingExpertItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/Counsellors");
                    result = await response.Content.ReadAsAsync<IEnumerable<InstructingExpertItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<IEnumerable<InstructorSubjectReviewItemModel>> getAllInstructorSubjectReviews(string accessToken)
        {
            try
            {
                IEnumerable<InstructorSubjectReviewItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/InstructorSubjectReviews");
                    result = await response.Content.ReadAsAsync<IEnumerable<InstructorSubjectReviewItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<IEnumerable<InstructorSubjectItemModel>> getAllInstructorSubjects(string accessToken)
        {
            try
            {
                IEnumerable<InstructorSubjectItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/InstructorSubjects");
                    result = await response.Content.ReadAsAsync<IEnumerable<InstructorSubjectItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<InstructingExpertItemModel> getCounsellor(string accessToken, int instructorId)
        {
            try
            {
                InstructingExpertItemModel result = null;
                var instructorlist = await getAllCounsellors(accessToken);
                result = instructorlist.FirstOrDefault(a => a.Id == instructorId && a.IsCounsellor == true);
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<IEnumerable<InstructorSubjectReviewItemModel>> getInstructorSubjectReviews(string accessToken, int instructorId)
        {
            try
            {
                IEnumerable<InstructorSubjectReviewItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/InstructorSubjectReviews/" + instructorId);
                    result = await response.Content.ReadAsAsync<IEnumerable<InstructorSubjectReviewItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<IEnumerable<InstructorSubjectItemModel>> getInstructorSubjects(string accessToken, int instructorId)
        {
            try
            {
                IEnumerable<InstructorSubjectItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/InstructorSubjects/" + instructorId);
                    result = await response.Content.ReadAsAsync<IEnumerable<InstructorSubjectItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<string> sendMessage(string accessToken, int instructorId, string subject, string message, string category)
        {
            try
            {
                string result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    //SendMessage? InstructorId = 1 & Subject = English + Language & Message = How +do +you +do +professor % 3F & Category = Tutor
                    var response = await client.GetAsync("/api/Remote/SendMessage?InstructorId=" + instructorId + "&Subject=" + subject + "&Message=" + message + "&Category=" + category);
                    result = await response.Content.ReadAsAsync<string>();
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

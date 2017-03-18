using Brainstormr.Portable.Model.MyAccount;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;


namespace Brainstormr.Portable.Model.Evaluation
{
    public class EvaluationService : IEvaluationService
    {
        public async Task<IEnumerable<EvaluationCategoryItemModel>> getAllEvaluationCategories(string accessToken)
        {
            try
            {
                IEnumerable<EvaluationCategoryItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/EvalCategories");
                    result = await response.Content.ReadAsAsync<IEnumerable<EvaluationCategoryItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<IEnumerable<EvaluationItemModel>> getAllEvaluations(string accessToken)
        {
            try
            {
                IEnumerable<EvaluationItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/Evaluations");
                    result = await response.Content.ReadAsAsync<IEnumerable<EvaluationItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<IEnumerable<EvaluationYearItemModel>> getAllEvaluationYears(string accessToken)
        {
            try
            {
                IEnumerable<EvaluationYearItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/EvalYears");
                    result = await response.Content.ReadAsAsync<IEnumerable<EvaluationYearItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<IEnumerable<QuestionOptionItemModel>> getQuestionOptions(string accessToken, int questionId)
        {
            try
            {
                IEnumerable<QuestionOptionItemModel> result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/GetQuestionOptions/" + questionId);
                    result = await response.Content.ReadAsAsync<IEnumerable<QuestionOptionItemModel>>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<List<QuestionItemModel>> getQuestions(string accessToken, int evalId)
        {
            try
            {
                List<QuestionItemModel> result = new List<QuestionItemModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/GetQuestions/" + evalId);
                    //result = await response.Content.ReadAsAsync<IEnumerable<QuestionItemModel>>();
                    //var tr =  response.Content.ReadAsAsync<string>();
                    //result = JsonConvert.DeserializeObject<IEnumerable<QuestionItemModel>>(tr.ToString());
                    //var res = await response.Content.ReadAsStringAsync();
                    //result = JsonConvert.DeserializeObject<List<QuestionItemModel>>(res);
                    //HttpResponseMessage response = client.GetAsync(apiurl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var questionlist = response.Content.ReadAsAsync<List<QuestionItemModel>>().Result;
                        foreach (var a in questionlist)
                        {
                            var newrec = new QuestionItemModel() {
                                Id = (int)a.Id,
                                Created = a.Created,
                                Duration = a.Duration,
                                ImagePath = a.ImagePath,
                                isInstruction = a.isInstruction,
                                Mark = a.Mark,
                                Modified = a.Modified,
                                QuestionCategory = a.QuestionCategory,
                                QuestionText = a.QuestionText,
                                Subject = a.Subject,
                                Year = a.Year
                            };
                            result.Add(newrec);
                        }
                        //result = questionlist.Select(a => new QuestionItemModel()
                        //{
                        //    Id = (int)a.Id,
                        //    Created = a.Created,
                        //    Duration = a.Duration,
                        //    ImagePath = a.ImagePath,
                        //    isInstruction = a.isInstruction,
                        //    Mark = a.Mark,
                        //    Modified = a.Modified,
                        //    QuestionCategory = a.QuestionCategory,
                        //    QuestionText = a.QuestionText,
                        //    Subject = a.Subject,
                        //    Year = a.Year
                        //}).ToList();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        public async Task<MyEvaluationItemModel> updateUserEvaluation(string accessToken, int evalId, decimal totalScore, decimal availableTotal, int noOfQuestions, int duration)
        {
            try
            {
                MyEvaluationItemModel result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    //UpdateUserEvaluation? QuestionId = id & TotalScore = tscore & AvailableTotal = availtotal & NoOfQuestions = noofq & Duration = duration
                    var response = await client.GetAsync("/api/Remote/UpdateUserEvaluation?QuestionId=" + evalId + "&TotalScore=" + totalScore +
                        "&AvailableTotal=" + availableTotal + "&NoOfQuestions=" + noOfQuestions + "&Duration=" + duration);
                    result = await response.Content.ReadAsAsync<MyEvaluationItemModel>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }


        public async Task<MySubscriptionItemModel> updateUserSubscription(string accessToken, int subscriptionId, int duration)
        {
            try
            {
                MySubscriptionItemModel result = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVal.webapibaseurl);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("authorization", String.Format("bearer {0}", accessToken));
                    var response = await client.GetAsync("/api/Remote/UpdateUserSubscription/" + subscriptionId + "?Duration=" + duration);
                    result = await response.Content.ReadAsAsync<MySubscriptionItemModel>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new SecurityException(ex.Message);
            }
        }

        //private static T _download_serialized_json_data<T>(string url) where T : new()
        //{
        //    using (var w = new HttpClient())
        //    {
        //        var json_data = string.Empty;
        //        // attempt to download JSON data as a string
        //        try
        //        {
        //            json_data = w.(url);
        //        }
        //        catch (Exception) { }
        //        // if string with JSON data is not empty, deserialize it to class and return its instance 
        //        return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
        //    }
        //}

    }
}

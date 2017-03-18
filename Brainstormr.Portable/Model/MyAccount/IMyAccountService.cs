using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.MyAccount
{
    public interface IMyAccountService
    {
        Task<IEnumerable<MyEbookItemModel>> getMyEbooks(string accessToken);
        Task<IEnumerable<MyMessageItemModel>> getMyMessages(string accessToken);
        Task<IEnumerable<MyEvaluationItemModel>> getMyEvaluations(string accessToken);
        Task<IEnumerable<MySubscriptionItemModel>> getMySubscriptions(string accessToken);

        //Task<UserInfoModel> getUserProfileinfo(string accesstoken);
    }
}

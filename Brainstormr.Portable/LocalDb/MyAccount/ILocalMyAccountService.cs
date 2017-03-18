using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb.MyAccount
{
    public interface ILocalMyAccountService
    {
        void updateMySubscription(MySubscription mySubscriptionItem);
    }
}

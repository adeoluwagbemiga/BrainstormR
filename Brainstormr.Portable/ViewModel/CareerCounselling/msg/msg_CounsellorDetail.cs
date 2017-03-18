using Brainstormr.Portable.Model.CareerCounselling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.CareerCounselling.msg
{
    public class msg_CounsellorDetail
    {
        public InstructingExpertItemModel counsellor_dto { get; set; }
        public msg_CounsellorDetail(InstructingExpertItemModel _counsellor_dto)
        {
            counsellor_dto = _counsellor_dto;
        }
    }
}

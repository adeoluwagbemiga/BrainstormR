using Brainstormr.Portable.Model.AskAnExpert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.AskAnExpert.msg
{
    public class msg_ExpertDetail
    {
        public InstructingExpertItemModel expert_dto { get; set; }
        public msg_ExpertDetail(InstructingExpertItemModel _expert_dto)
        {
            expert_dto = _expert_dto;
        }
    }
}

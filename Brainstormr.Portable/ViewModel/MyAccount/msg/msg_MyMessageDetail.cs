using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.MyAccount.msg
{
    public class msg_MyMessageDetail
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
        public string InstructorImage { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Reply { get; set; }
        public string Read { get; set; }
        public string AccessToken { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}

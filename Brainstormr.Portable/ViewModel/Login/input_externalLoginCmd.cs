using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.Login
{
    public class input_externalLoginCmd
    {
        public string userName { get; set; }
        public string provider { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string externalAccessToken { get; set; }
    }
}

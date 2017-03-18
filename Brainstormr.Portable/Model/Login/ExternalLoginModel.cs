using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.Login
{
    public class ExternalLoginModel
    {
        //UserName, Provider, Name, Picture, ExternalAccessToken
        public string UserName { get; set; }
        public string Provider { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string ExternalAccessToken { get; set; }
    }
}

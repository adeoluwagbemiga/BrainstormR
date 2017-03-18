using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb
{
    [Table("ExternalLoginInfo")]
    public class ExternalLoginInfo
    {
        [PrimaryKey]
        public string UserName { get; set; }
        public string Provider { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string ExternalAccessToken { get; set; }
    }
}

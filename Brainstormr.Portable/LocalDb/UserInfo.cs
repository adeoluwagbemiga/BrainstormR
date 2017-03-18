using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [PrimaryKey]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Institution { get; set; }
        public string Course { get; set; }
        public string Profession { get; set; }
        public string HasRegistered { get; set; }
        public string LoginProvider { get; set; }
    }
}

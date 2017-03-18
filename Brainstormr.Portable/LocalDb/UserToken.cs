using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb
{
    [Table("UserToken")]
    public class UserToken
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string AccessToken { get; set; }
        public string UserName { get; set; }
    }
}

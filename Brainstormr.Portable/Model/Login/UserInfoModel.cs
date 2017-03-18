using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.Login
{
    public class UserInfoModel
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("AvatarUrl")]
        public string AvatarUrl { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("Institution")]
        public string Institution { get; set; }

        [JsonProperty("Course")]
        public string Course { get; set; }

        [JsonProperty("Profession")]
        public string Profession { get; set; }

        [JsonProperty("HasRegistered")]
        public string HasRegistered { get; set; }

        [JsonProperty("LoginProvider")]
        public string LoginProvider { get; set; }
    }
}

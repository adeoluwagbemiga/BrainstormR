using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.MyAccount
{
    public class MyMessageItemModel
    {
  //      {
  //  "UserEmail": "chinemere.okereke@gmail.com",
  //  "InstructorId": 1,
  //  "InstructorName": "Wole Soyinka",
  //  "InstructorImage": "/Resources/Instructor/6_2016/B2j1CY2IAAAipIt.jpg",
  //  "Subject": "English Language",
  //  "Message": "Hello Professor, can you make a brief speech on the state of the nation? Thank you",
  //  "Reply": "Hello. The state of the nation as currently is saddens my heart. We need money, jobs and a thinking leadership. We will get them someday.\r\n\r\nRegards\r\n\r\nProf",
  //  "Read": true,
  //  "AccessToken": "",
  //  "Id": 2,
  //  "Created": "2016-07-03T10:17:14.093",
  //  "Modified": "2016-07-12T04:36:57.723"
  //}
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

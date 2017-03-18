using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.Model.Evaluation
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class QuestionItemModel
    {
        [JsonProperty(PropertyName = "QuestionCategory")]
        public string QuestionCategory { get; set; }

        [JsonProperty(PropertyName = "Subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "Year")]
        public int Year { get; set; }

        [JsonProperty(PropertyName = "Duration")]
        public int Duration { get; set; }

        [JsonProperty(PropertyName = "QuestionText")]
        public string QuestionText { get; set; }

        [JsonProperty(PropertyName = "Mark")]
        public decimal Mark { get; set; }

        [JsonProperty(PropertyName = "isInstruction")]
        public bool isInstruction { get; set; }

        [JsonProperty(PropertyName = "ImagePath")]
        public string ImagePath { get; set; }

        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "Created")]
        public string Created { get; set; }

        [JsonProperty(PropertyName = "Modified")]
        public string Modified { get; set; }
    }
}

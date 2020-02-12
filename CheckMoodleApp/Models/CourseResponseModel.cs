using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CheckMoodleApp.Models
{
    public class CourseResponseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("shortname")]
        public string ShortName { get; set; }

        [JsonProperty("fullname")]
        public string FullName { get; set; }

        [JsonProperty("displayname")]
        public string DisplayName { get; set; }
    }
}

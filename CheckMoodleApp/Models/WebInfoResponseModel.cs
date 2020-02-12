using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CheckMoodleApp.Models
{
    public class WebInfoResponseModel
    {
        [JsonProperty("userid")]
        public string UserId { get; set; }
    }
}

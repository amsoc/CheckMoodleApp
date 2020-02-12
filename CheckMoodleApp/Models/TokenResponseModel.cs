using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CheckMoodleApp.Models
{
    public class TokenResponseModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("privatetoken")]
        public string PrivateToken {get; set;}
    }
}

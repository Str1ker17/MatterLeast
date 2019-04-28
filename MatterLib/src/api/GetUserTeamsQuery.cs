using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace MatterLib {
    public class GetUserTeamsQuery : APIQuery {
        [JsonIgnore]
        public String user_id { get; set; }

        protected override string target { get { return String.Format("/users/{0}/teams", this.user_id); } }

        protected override string method { get { return "GET"; } }

        protected override Response CollectSufficientData(HttpWebResponse response, string responseString) {
            //return JsonConvert.DeserializeObject<ResponseImpl>("{" + responseString + "}");
            return new ResponseImpl {teams = JsonConvert.DeserializeObject<List<Team>>(responseString)};
        }

        public class ResponseImpl : Response {
            public List<Team> teams { get; set; }
        }
    }
}

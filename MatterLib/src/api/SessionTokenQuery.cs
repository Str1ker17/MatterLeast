using System;
using System.Net;

namespace MatterLib
{
    // follow this rule: define 2 classes in single file,
    // one is for query and one for response.
    public class SessionTokenQuery : APIQuery
    {
        protected sealed override String target { get { return "/users/login"; } }
        protected sealed override String method { get { return "POST"; } }

        public String login_id { get; set; }
        public String password { get; set; }

        protected override Response CollectSufficientData(HttpWebResponse response, String responseString) {
            return new ResponseImpl { Token = response.Headers["Token"] };
        }

        /// <summary>
        /// On a success, we get user object and 
        /// </summary>
        public class ResponseImpl : Response {
            public String Token { get; set; }
        }
    }
}

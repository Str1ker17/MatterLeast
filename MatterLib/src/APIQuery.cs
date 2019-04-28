using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace MatterLib {
    public abstract class APIQuery {
        /// <summary>
        /// Query address is constructed from host/base_target/target.
        /// </summary>
        protected readonly String base_target = "api/v4";

        /// <summary>
        /// Query-defined target, like "users/login".
        /// </summary>
        protected abstract String target { get; }

        /// <summary>
        /// Query-defined method, like "GET" or "PUT".
        /// </summary>
        protected abstract String method { get; }

        /// <summary>
        /// Server which request is sent to.
        /// </summary>
        public String https_server { get; set; }

        /// <summary>
        /// Authorization token. Can be null.
        /// </summary>
        public String token { get; set; }

        /// <summary>
        /// Constructs query with default parameters.
        /// </summary>
        protected APIQuery() {
            this.https_server = DefaultAPIQuerySettings.https_server;
            this.token = DefaultAPIQuerySettings.token;
        }

        /// <summary>
        /// What's not clear, dude?
        /// </summary>
        /// <param name="response"></param>
        /// <param name="responseString"></param>
        /// <returns></returns>
        protected abstract Response CollectSufficientData(HttpWebResponse response, String responseString);

        /// <summary>
        /// Universal entrypoint to execute an API query.
        /// </summary>
        /// <returns></returns>
        public Response Execute() {
            // do it now right here
            HttpWebRequest hwr = (HttpWebRequest) WebRequest.CreateDefault(
                new Uri(
                    "https://" + https_server + "/"
                  + base_target
                  + this.target
                )
            );
            hwr.Method = this.method;
            hwr.ContentType = "application/json";

            // add token to request
            if (token != null) {
                hwr.Headers.Add("Authorization", String.Format("Bearer {0}", token));
            }

            // if this is not a GET query...
            if (this.method != "GET") {
                String postData = JsonConvert.SerializeObject(this);
                byte[] postArray = Encoding.UTF8.GetBytes(postData);
                hwr.ContentLength = postArray.Length;
                using (Stream stream = hwr.GetRequestStream()) {
                    stream.Write(postArray, 0, postArray.Length);
                }
            }

            HttpWebResponse response;
            try {
                response = (HttpWebResponse) hwr.GetResponse();
            }
            catch (WebException e) {
                if (e.Response == null) {
                    // serious error; TODO: report it
                    throw;
                }

                response = (HttpWebResponse) e.Response;
                APIException apiex = new APIException();
                Stream responseStream1 = response.GetResponseStream();
                if (responseStream1 != null) {
                    String responseString1 =
                        new StreamReader(responseStream1).ReadToEnd();
                    apiex = JsonConvert.DeserializeObject<APIException>(responseString1);
                }

                throw apiex;
            }

            Stream responseStream = response.GetResponseStream();
            if (responseStream == null) {
                // TODO: be more descriptive
                throw new APIException();
            }

            String responseString = new StreamReader(responseStream).ReadToEnd();
            return CollectSufficientData(response, responseString);
        }

        /// <summary>
        /// Response type for this specific query.
        /// Extend fields in derived class if you need it.
        /// </summary>
        public abstract class Response {
            // TODO: add non-serializable headers collection field
        }
    }
}

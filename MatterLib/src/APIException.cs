using System;

namespace MatterLib {
    public class APIException : Exception {
        public String id { get; set; }
        public String message { get; set; }
        public String detailed_error { get; set; }
        public String request_id { get; set; }
        public int status_code { get; set; }
        public bool is_oauth { get; set; }

        public override String ToString() {
            return String.Format("{0} {1}:\r\n{2}\r\n\r\n{3} (request_id={4})"
                               , status_code, id
                               , message, detailed_error
                               , request_id);
        }
    }
}

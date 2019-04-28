using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatterLib {
    public static class DefaultAPIQuerySettings {
        /// <summary>
        /// Default server which request is sent to.
        /// </summary>
        public static String https_server { get; set; }

        /// <summary>
        /// Default authorization token.
        /// </summary>
        public static String token { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MattermostApp {
    class UserSettings {
        // TODO: move account-specific fields to "accounts" array
        public String server { get; set; }
        public String login { get; set; }
        public String password { get; set; }
    }
}

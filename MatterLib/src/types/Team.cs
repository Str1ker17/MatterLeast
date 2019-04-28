using System;

namespace MatterLib {
    public class Team {
        public String id { get; set; }
        public long create_at { get; set; }
        public long update_at { get; set; }
        public long delete_at { get; set; }
        public String display_name { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String email { get; set; }
        public String type { get; set; }
        public String allowed_domains { get; set; }
        public String invite_id { get; set; }
        public bool allow_open_invite { get; set; }
    }
}

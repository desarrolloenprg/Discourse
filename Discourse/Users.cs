using System;
using System.Collections.Generic;

namespace Discourse
{
    internal class Users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string avatar_template { get; set; }
        public string email { get; set; }
        public List<object> secondary_emails { get; set; }
        public bool active { get; set; }
        public bool admin { get; set; }
        public bool moderator { get; set; }
        public DateTime? last_seen_at { get; set; }
        public DateTime? last_emailed_at { get; set; }
        public DateTime created_at { get; set; }
        public double? last_seen_age { get; set; }
        public double? last_emailed_age { get; set; }
        public double created_at_age { get; set; }
        public string username_lower { get; set; }
        public int trust_level { get; set; }
        public object manual_locked_trust_level { get; set; }
        public int flag_level { get; set; }
        public string title { get; set; }
        public bool approved { get; set; }
        public int time_read { get; set; }
        public bool staged { get; set; }
        public int days_visited { get; set; }
        public int posts_read_count { get; set; }
        public int topics_entered { get; set; }
        public int post_count { get; set; }
        public int reply_count { get; set; }
        public int likes_received {get; set;}
        public int likes_given { get; set; }
    }
}
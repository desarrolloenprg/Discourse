using System;
using System.Collections.Generic;

namespace Discourse
{
    internal class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string avatar_template { get; set; }
        public string title { get; set; }

    }

    internal class DirectoryItem
    {
        public int id { get; set; }
        public int likes_received { get; set; }
        public int likes_given { get; set; }
        public int topics_entered { get; set; }
        public int topic_count { get; set; }
        public int post_count { get; set; }
        public int posts_read { get; set; }
        public int days_visited { get; set; }
        public User user { get; set; }

    }

    internal class Meta
    {
        public DateTime last_updated_at { get; set; }
        public int total_rows_directory_items { get; set; }
        public string load_more_directory_items { get; set; }

    }

    internal class Items
    {
        public List<DirectoryItem> directory_items { get; set; }
        public Meta meta { get; set; }

    }
}
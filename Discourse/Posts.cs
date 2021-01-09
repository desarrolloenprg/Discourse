using System;
using System.Collections.Generic;

namespace Discourse
{
    internal class ActionsSummary
    {
        public int id { get; set; }
        public bool can_act { get; set; }

    }

    internal class LatestPost
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string avatar_template { get; set; }
        public DateTime created_at { get; set; }
        public string cooked { get; set; }
        public int post_number { get; set; }
        public int post_type { get; set; }
        public DateTime updated_at { get; set; }
        public int reply_count { get; set; }
        public object reply_to_post_number { get; set; }
        public int quote_count { get; set; }
        public int incoming_link_count { get; set; }
        public int reads { get; set; }
        public int readers_count { get; set; }
        public double score { get; set; }
        public bool yours { get; set; }
        public int topic_id { get; set; }
        public string topic_slug { get; set; }
        public string topic_title { get; set; }
        public string topic_html_title { get; set; }
        public int category_id { get; set; }
        public string display_username { get; set; }
        public object primary_group_name { get; set; }
        public object primary_group_flair_url { get; set; }
        public object primary_group_flair_bg_color { get; set; }
        public object primary_group_flair_color { get; set; }
        public int version { get; set; }
        public bool can_edit { get; set; }
        public bool can_delete { get; set; }
        public bool can_recover { get; set; }
        public bool can_wiki { get; set; }
        public string user_title { get; set; }
        public string raw { get; set; }
        public List<ActionsSummary> actions_summary { get; set; }
        public bool moderator { get; set; }
        public bool admin { get; set; }
        public bool staff { get; set; }
        public int user_id { get; set; }
        public bool hidden { get; set; }
        public int trust_level { get; set; }
        public object deleted_at { get; set; }
        public bool user_deleted { get; set; }
        public object edit_reason { get; set; }
        public bool can_view_edit_history { get; set; }
        public bool wiki { get; set; }
        public object reviewable_id { get; set; }
        public int reviewable_score_count { get; set; }
        public int reviewable_score_pending_count { get; set; }
        public string notice_type { get; set; }
        public bool? static_doc { get; set; }

    }

    internal class Posts
    {
        public List<LatestPost> latest_posts { get; set; }

    }

}
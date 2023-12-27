using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Links
    {
        public string[]? Homepage { get; set; }
        public string[]? Blockchain_site { get; set; }
        public string[]? Official_forum_url { get; set; }
        public string[]? Chat_url { get; set; }
        public string[]? Announcement_url { get; set; }
        public string? Twitter_screen_name { get; set; }
        public string? Facebook_username { get; set; }
        public string? Bitcointalk_thread_identifier { get; set; }
        public string? Telegram_channel_identifier { get; set; }
        public string? Subreddit_url { get; set; }
        public Image? Images { get; set; }      // Image model contains links to small, medium and large images.
    }
}

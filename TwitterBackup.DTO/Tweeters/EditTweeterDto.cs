using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackup.DTO.Tweeters
{
    public class EditTweeterDto
    {
        public string UserName { get; set; }

        public string TweeterId { get; set; } //Id_str

        public string Description { get; set; }

        public string TweeterComments { get; set; }

    }
}

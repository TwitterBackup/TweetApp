using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackup.DTO.Tweets
{
    public class EditTweeterDto
    {
        public string UserName { get; set; }

        public string TweetId { get; set; } //Id_str

        public string Text { get; set; }

        public string TweetComments { get; set; }

    }
}

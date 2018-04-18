using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackup.Models
{
    public class TweetHashtag
    {
        public string TweetId { get; set; }

        public Tweet Tweet { get; set; }

        public string HashtagId { get; set; }

        public Hashtag Hashtag { get; set; }
    }
}

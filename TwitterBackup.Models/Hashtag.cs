using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models.Abstracts;

namespace TwitterBackup.Models
{
    public class Hashtag : DataModel
    {
        public Hashtag()
        {
            this.HashtagTweets = new HashSet<TweetHashtag>();
        }

        [Key]
        public string HashtagId { get; set; }

        public string Text { get; set; }

        public ICollection<TweetHashtag> HashtagTweets { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Models
{
    public class Hashtag
    {
        public Hashtag()
        {
            this.TweetHashtags = new HashSet<TweetHashtag>();
        }

        [Key]
        public string HashtagId { get; set; }

        [StringLength(300, MinimumLength = 1, ErrorMessage = "Parameter length - 1 to 300 characters")]
        public string Text { get; set; }

        public ICollection<TweetHashtag> TweetHashtags { get; set; }
    }
}

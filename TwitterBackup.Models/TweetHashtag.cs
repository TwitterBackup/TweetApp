using System;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Models
{
    public class TweetHashtag: IDeletable
    {
        [Required]
        public string TweetId { get; set; }

        [Required]
        public Tweet Tweet { get; set; }

        [Required]
        public string HashtagId { get; set; }

        [Required]
        public Hashtag Hashtag { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}

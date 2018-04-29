using System;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Models
{
    public class TweetHashtag
    {
        [Required]
        public string TweetId { get; set; }

        [Required]
        public Tweet Tweet { get; set; }

        [Required]
        public string HashtagId { get; set; }

        [Required]
        public Hashtag Hashtag { get; set; }
    }
}

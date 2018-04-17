using System.Collections.Generic;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.DTO.User;
using TwitterBackup.Services.Data.Contracts;

namespace TwitterBackup.Services.Data
{
    public class TweetDbService : ITweetDbService
    {
        public void Add(TweetDto tweet)
        {
            throw new System.NotImplementedException();
        }

        public void AddMany(IEnumerable<TweetDto> tweets)
        {
            throw new System.NotImplementedException();
        }

        public TweetDto GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TweetDto> GetTweetsByUser(UserDto user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TweetDto> GetTweetsByTweeter(TweetDto user)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TweetDto tweet)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(TweetDto tweet)
        {
            throw new System.NotImplementedException();
        }
    }
}

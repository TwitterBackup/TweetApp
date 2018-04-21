using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.Repository;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.DTO.User;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;

namespace TwitterBackup.Services.Data
{
    public class TweetDbService
    {
        private readonly IRepository<Tweet> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMappingProvider mapper;


        public TweetDbService(IRepository<Tweet> repository, IUnitOfWork unitOfWork, IMappingProvider mapper)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void AddTweetToDb(TweetDto tweetDto)
        {
            var tweet = this.mapper.MapTo<Tweet>(tweetDto);
            this.repository.Add(tweet);
            this.unitOfWork.CompleteWork();
        }

        public void AddManyTweetsToDb(IEnumerable<TweetDto> tweets)
        {
            throw new System.NotImplementedException();
        }

        public TweetDto GetTweetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TweetDto> GetTweetsByUser(UserDto user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<TweetDto>> GetTweetsByTweeterIdAsync(string id)
        {
            var allTweetsFromDb = await repository.GetAllAsync();

            var tweetsByTweeterId = allTweetsFromDb.Where(x => x.Tweeter.TweeterId == id);

            var tweetsDto = mapper.ProjectTo<TweetDto>(tweetsByTweeterId);

            return tweetsDto;
        }

        public void UpdateTweet(TweetDto tweet)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTweet(TweetDto tweet)
        {
            throw new System.NotImplementedException();
        }
    }
}

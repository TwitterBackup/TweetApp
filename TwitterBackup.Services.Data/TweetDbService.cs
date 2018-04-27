//using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.Repository;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;

namespace TwitterBackup.Services.Data
{
    public class TweetDbService : ITweetDbService
    {
        private readonly IRepository<TweetHashtag> tweetHashtagRepository;
        private readonly IRepository<Tweet> tweetRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMappingProvider mappingProvider;
        private readonly IRepository<UserTweet> userTweetRepository;


        public TweetDbService(IRepository<Tweet> tweetRepository, IUnitOfWork unitOfWork, IMappingProvider mappingProvider, IRepository<UserTweet> userTweetRepository, IRepository<TweetHashtag> tweetHashtagRepository)
        {
            this.tweetHashtagRepository = tweetHashtagRepository ?? throw new ArgumentNullException(nameof(tweetHashtagRepository));
            this.tweetRepository = tweetRepository ?? throw new ArgumentNullException(nameof(tweetRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
            this.userTweetRepository = userTweetRepository ?? throw new ArgumentNullException(nameof(userTweetRepository));
        }

        public void AddTweetToDb(TweetDto tweetDto)
        {
            var tweet = this.mappingProvider.MapTo<Tweet>(tweetDto);
            this.tweetRepository.Add(tweet);
            this.unitOfWork.CompleteWork();
        }

        public void AddManyTweetsToDb(IEnumerable<TweetDto> tweets)
        {
            throw new System.NotImplementedException();
        }

        public TweetDto TweetById(string tweetId, string userId)
        {
            var userTweet = this.tweetRepository.GetTweetById(tweetId, userId);

            var tweetHastags = tweetHashtagRepository.GetTweetHashtagsByTweetId(tweetId);

            var tweetDto = mappingProvider.MapTo<TweetDto>(userTweet);

            tweetDto.Hashtags = tweetHastags;

            return tweetDto;
        }

        public async Task<IEnumerable<TweetDto>> GetAllTweetsAsync()
        {
            var savedTweets = await this.tweetRepository.GetAllUsersWithTweetsAsync(1, 10);
            var savedTweetsList = savedTweets as List<UserTweet> ?? savedTweets.ToList();

            var tweetDtosList = this.mappingProvider.ProjectTo<UserTweet, TweetDto>(savedTweetsList).ToList();

            var tweetsDtosWithHashtags = AddHashtagsToTweetDtos(tweetDtosList, savedTweetsList);

            return tweetsDtosWithHashtags;
        }

        public async Task<IEnumerable<TweetDto>> GetTweetsByUserIdAsync(string userId)
        {
            var savedTweets = await this.userTweetRepository
                    .FindTweetsAsync(userTweet => (userTweet.User.Id == userId) && userTweet.IsDeleted == false);
            var savedTweetsList = savedTweets.ToList();

            var tweetDtosList = this.mappingProvider.ProjectTo<UserTweet, TweetDto>(savedTweetsList).ToList();

            var tweetsDtosWithHashtags = AddHashtagsToTweetDtos(tweetDtosList, savedTweetsList);

            return tweetsDtosWithHashtags;
        }

        IEnumerable<TweetDto> ITweetDbService.GetTweetsByTweeterIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateTweet(EditTweetDto editTweetDto, string userId)
        {
            var userTweetForEdit = userTweetRepository.GetTweetById(editTweetDto.TweetId, userId);

            userTweetForEdit.TweetComments = editTweetDto.TweetComments;
            userTweetForEdit.ModifiedOn = DateTime.Now;

            unitOfWork.CompleteWork();
        }

        public void SoftDeleteTweetByIdPerUserId(string tweetId, string userId)
        {
            var userTweet = userTweetRepository.GetByCompositeId(userId, tweetId);

            if (userTweet == null)
            {
                throw new ArgumentException();
            }
            userTweet.IsDeleted = true;
            userTweet.DeletedOn = DateTime.Now;
            unitOfWork.CompleteWork();
        }

        public void SoftDeleteTweetByIdForAllUsers(string tweetId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TweetDto>> GetTweetsByTweeterIdAsync(string id)
        {
            var allTweetsFromDb = await tweetRepository.GetAllAsync(1);

            var tweetsByTweeterId = allTweetsFromDb.Where(x => x.Tweeter.TweeterId == id);

            var tweetsDto = mappingProvider.ProjectTo<Tweet, TweetDto>(tweetsByTweeterId);

            return tweetsDto;
        }

        private IEnumerable<TweetDto> AddHashtagsToTweetDtos(IReadOnlyList<TweetDto> tweetDtosList, IEnumerable<UserTweet> savedTweetsList)
        {
            var counter = 0;

            foreach (var userTweet in savedTweetsList)
            {
                var tweetHastags = tweetHashtagRepository.GetTweetHashtagsByTweetId(userTweet.TweetId);
                tweetDtosList[counter].Hashtags = tweetHastags;
                counter++;
            }

            return tweetDtosList;
        }


    }
}

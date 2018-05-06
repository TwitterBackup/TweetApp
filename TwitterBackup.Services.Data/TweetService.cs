//using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.Repository;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.DTO.User;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;

namespace TwitterBackup.Services.Data
{
    public class TweetService : ITweetService
    {
        private readonly IRepository<Hashtag> hashtagRepository;
        private readonly IRepository<TweetHashtag> tweetHashtagRepository;
        private readonly IRepository<Tweet> tweetRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMappingProvider mappingProvider;
        private readonly IRepository<UserTweet> userTweetRepository;


        public TweetService(IRepository<Tweet> tweetRepository, IUnitOfWork unitOfWork, IMappingProvider mappingProvider,
            IRepository<UserTweet> userTweetRepository, IRepository<TweetHashtag> tweetHashtagRepository, IRepository<Hashtag> hashtagRepository)
        {
            this.hashtagRepository = hashtagRepository ?? throw new ArgumentNullException(nameof(hashtagRepository));
            this.tweetHashtagRepository = tweetHashtagRepository ?? throw new ArgumentNullException(nameof(tweetHashtagRepository));
            this.tweetRepository = tweetRepository ?? throw new ArgumentNullException(nameof(tweetRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
            this.userTweetRepository = userTweetRepository ?? throw new ArgumentNullException(nameof(userTweetRepository));
        }

        public TweetDto GetTweetForUser(string userId, string tweetId)
        {
            var userTweet = this.userTweetRepository
                .IncludeDbSet(x => x.User, x => x.Tweet, x => x.Tweet.Tweeter)
                .SingleOrDefault(relation => relation.UserId == userId && relation.TweetId == tweetId && relation.IsDeleted == false);

            if (userTweet == null)
                throw new ArgumentNullException();

            if (userTweet.TweetComments != null)
                return mappingProvider.MapTo<TweetDto>(userTweet);

            userTweet.TweetComments = string.Empty;
            unitOfWork.CompleteWork();
            return mappingProvider.MapTo<TweetDto>(userTweet);
        }

        //public IEnumerable<TweetDto> GetAllTweets()
        //{
        //    var userTweetsList = userTweetRepository
        //        .IncludeDbSet(x => x.User, x => x.Tweet, x => x.Tweet.Tweeter, x => x.Tweet.TweetHashtags)
        //        .Where(tweet => tweet.IsDeleted == false).ToList();

        //    var tweetDtosList = this.mappingProvider.ProjectTo<UserTweet, TweetDto>(userTweetsList).ToList();

        //    var tweetsDtosWithHashtags = GetHashtagsToTweetDtos(tweetDtosList, userTweetsList);

        //    return tweetsDtosWithHashtags;
        //}

        //private IEnumerable<TweetDto> GetHashtagsToTweetDtos(List<TweetDto> tweetDtosList, List<UserTweet> userTweetsList)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<IEnumerable<TweetDto>> GetTweetsByUserIdAsync(string userId)
        //{
        //    var userTweets = await this.userTweetRepository
        //            .FindAsync(userTweet => (userTweet.User.Id == userId) && userTweet.IsDeleted == false);
        //    var savedTweetsList = userTweets.ToList();

        //    var tweetDtosList = this.mappingProvider.ProjectTo<UserTweet, TweetDto>(savedTweetsList).ToList();

        //    var tweetsDtosWithHashtags = GetHashtagsToTweetDtos(tweetDtosList, savedTweetsList);

        //    return tweetsDtosWithHashtags;
        //}

        //public void UpdateTweet(EditTweeterDto editTweetDto, string userId)
        //{
        //    var userTweetForEdit = userTweetRepository.GetById(editTweetDto.TweetId);

        //    userTweetForEdit.TweetComments = editTweetDto.TweetComments;
        //    userTweetForEdit.ModifiedOn = DateTime.Now;

        //    unitOfWork.CompleteWork();
        //}

        //public void SoftDeleteTweetByIdPerUserId(string tweetId, string userId)
        //{
        //    var userTweet = userTweetRepository.GetByCompositeId(userId, tweetId);

        //    if (userTweet == null)
        //    {
        //        throw new ArgumentException();
        //    }
        //    userTweet.IsDeleted = true;
        //    userTweet.DeletedOn = DateTime.Now;
        //    unitOfWork.CompleteWork();
        //}

        public async Task AddTweetForUserAsync(string userId, ApiTweetDto apiTweetDto)
        {
            //check whether such tweetId exist
            if (tweetRepository.Any(tweet => tweet.TweetId == apiTweetDto.TweetId))
            {
                //check whether link between tweet and user exist
                if (userTweetRepository.Any(userTweet => userTweet.TweetId == apiTweetDto.TweetId && userTweet.UserId == userId))
                {
                    //this link is probably softdeleted, make it active
                    var existingRelationUserTweet = userTweetRepository.SingleOrDefault(userTweet =>
                        userTweet.TweetId == apiTweetDto.TweetId && userTweet.UserId == userId);

                    existingRelationUserTweet.IsDeleted = false;
                    existingRelationUserTweet.ModifiedOn = DateTime.Now;
                    await unitOfWork.CompleteWorkAsync();
                }
                else
                {
                    await AddRelationUserTweetAsync(userId, apiTweetDto);
                }
            }
            else
            {
                //no such tweet => 1. create it, 2. add relation User - Tweet, 3. Add its hashtags
                var newTweet = mappingProvider.MapTo<Tweet>(apiTweetDto);
                await tweetRepository.AddAsync(newTweet);
                await unitOfWork.CompleteWorkAsync();

                await AddRelationUserTweetAsync(userId, apiTweetDto);
                if (apiTweetDto.Hashtags.Count > 0)
                {
                    await AddHashtagsToTweetAsync(apiTweetDto.Hashtags, apiTweetDto.TweetId);
                }
            }
        }

        private async Task AddHashtagsToTweetAsync(ICollection<Hashtag> hashtags, string tweetId)
        {
            foreach (var tag in hashtags)
            {
                //check whether hashtag exist
                if (!hashtagRepository.Any(t => t.Text == tag.Text))
                {
                    await hashtagRepository.AddAsync(tag);
                }
                await AddRelationTweetHashtagAsync(tweetId, tag);
            }
        }

        private async Task AddRelationTweetHashtagAsync(string tweetId, Hashtag tag)
        {
            //add new relation Tweet - Hashtag
            var tweetHashtag = new TweetHashtag()
            {
                HashtagId = hashtagRepository.SingleOrDefault(t => t.Text == tag.Text).HashtagId,
                TweetId = tweetId
            };
            await tweetHashtagRepository.AddAsync(tweetHashtag);
            await unitOfWork.CompleteWorkAsync();
        }

        private IEnumerable<TweetDto> GetHashtagsToTweetDtos(IReadOnlyList<TweetDto> tweetDtosList, IEnumerable<UserTweet> userTweetsList)
        {
            var counter = 0;

            foreach (var userTweet in userTweetsList)
            {
                var tweetHastags = tweetHashtagRepository.GetTweetHashtagsByTweetId(userTweet.TweetId);
                tweetDtosList[counter].Hashtags = tweetHastags;
                counter++;
            }

            return tweetDtosList;
        }

        public IEnumerable<TweetDto> GetAllTweetsForAdmin()
        {
            var userTweetsList = userTweetRepository
                .IncludeDbSet(x => x.User, x => x.Tweet, x => x.Tweet.Tweeter, x => x.Tweet.TweetHashtags)
                .Where(tweet => tweet.IsDeleted == false).ToList();

            var tweetDtosList = this.mappingProvider.ProjectTo<UserTweet, TweetDto>(userTweetsList).ToList();

            var tweetsDtosWithHashtags = GetHashtagsToTweetDtos(tweetDtosList, userTweetsList);

            return tweetsDtosWithHashtags;
        }

        public IEnumerable<TweetDto> GetAllTweetsForUser(string userId)
        {
            var userTweets = this.userTweetRepository
                .IncludeDbSet(x => x.User, x => x.Tweet, x => x.Tweet.Tweeter, x => x.Tweet.TweetHashtags)
                .Where(userTweet => (userTweet.User.Id == userId) && userTweet.IsDeleted == false);

            //.FindAsync(userTweet => (userTweet.User.Id == userId) && userTweet.IsDeleted == false);
            var savedTweetsList = userTweets.ToList();

            var tweetDtosList = this.mappingProvider.ProjectTo<UserTweet, TweetDto>(savedTweetsList).ToList();

            var tweetsDtosWithHashtags = GetHashtagsToTweetDtos(tweetDtosList, savedTweetsList);

            return tweetsDtosWithHashtags;
        }

        public IEnumerable<TweetDto> GetAllTweetsByAuthorAsync(string tweeterId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TweetDto> GetAllTweetsByTweeterForUser(string userId, string tweeterId)
        {
            var userTweets = userTweetRepository
                    .IncludeDbSet(x => x.Tweet, x => x.User, x => x.Tweet.Tweeter)
                    .Where(x => x.IsDeleted == false && x.User.Id == userId && x.Tweet.Tweeter.TweeterId == tweeterId);

            var tweetDtos = mappingProvider.ProjectTo<UserTweet, TweetDto>(userTweets);

            return tweetDtos;
        }

        public async Task AddNoteToSavedTweetForUserAsync(string userId, string tweetId, string note)
        {
            var userTweetForEdit = userTweetRepository
                .SingleOrDefault(userTweet => userTweet.UserId == userId && userTweet.TweetId == tweetId);

            userTweetForEdit.TweetComments = note;
            userTweetForEdit.ModifiedOn = DateTime.Now;

            await unitOfWork.CompleteWorkAsync();
        }

        public async Task RemoveSavedTweetForUserAsync(string userId, string tweetId)
        {
            var userTweet = userTweetRepository.GetByCompositeId(userId, tweetId);

            if (userTweet == null)
            {
                throw new ArgumentException();
            }
            userTweet.IsDeleted = true;
            userTweet.DeletedOn = DateTime.Now;
            await unitOfWork.CompleteWorkAsync();
        }

        public async Task RemoveSavedTweetForAllUsersAsync(string tweetId)
        {
            var userTweets = await userTweetRepository.FindAsync(relation => relation.TweetId == tweetId);
            var userTweetsList = userTweets.ToList();

            if (!userTweetsList.Any())
            {
                throw new ArgumentException();
            }
            foreach (var userTweet in userTweetsList)
            {
                userTweet.IsDeleted = true;
            }
            await unitOfWork.CompleteWorkAsync();
        }

        private async Task AddRelationUserTweetAsync(string userId, ApiTweetDto apiTweetDto)
        {
            var newRelationUserTweet = new UserTweet()
            {
                UserId = userId,
                TweetId = apiTweetDto.TweetId,
                SavedOn = DateTime.Now
            };
            await userTweetRepository.AddAsync(newRelationUserTweet);
            await unitOfWork.CompleteWorkAsync();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.Repository;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;

namespace TwitterBackup.Services.Data
{
    public class TweeterService : ITweeterService
    {
        private readonly IRepository<UserTweeter> userTweeterRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Tweeter> tweeterRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMappingProvider mappingProvider;

        public TweeterService(IRepository<Tweeter> tweeterRepository, IUnitOfWork unitOfWork, IMappingProvider mappingProvider, IRepository<UserTweeter> userTweeterRepository, IRepository<ApplicationUser> userRepository)
        {
            this.userTweeterRepository = userTweeterRepository;
            this.userRepository = userRepository;
            this.tweeterRepository = tweeterRepository ?? throw new ArgumentNullException(nameof(tweeterRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        public async Task AddFavoriteTweeterForUserAsync(string userId, TweeterDto tweeterDto)
        {
            //check whether such tweeterId exist
            if (tweeterRepository.Any(tweeter => tweeter.TweeterId == tweeterDto.TweeterId))
            {
                //check whether link between tweeter and user exist
                if (userTweeterRepository.Any(userTweeter => userTweeter.TweeterId == tweeterDto.TweeterId && userTweeter.UserId == userId))
                {
                    //this link is probably softdeleted, make it active
                    var existingRelationUserTweeter = userTweeterRepository.SingleOrDefault(userTweeter =>
                        userTweeter.TweeterId == tweeterDto.TweeterId && userTweeter.UserId == userId);

                    existingRelationUserTweeter.IsDeleted = false;
                    existingRelationUserTweeter.ModifiedOn = DateTime.Now;
                    await unitOfWork.CompleteWorkAsync();
                }
                else
                {
                    await AddRelationUserTweeterAsync(userId, tweeterDto);
                }
            }
            else
            {
                //no such tweeter => 1. create it, 2. add relation User - Tweet
                //tweeterDto.TweeterComments = string.Empty;
                var newTweeter = mappingProvider.MapTo<Tweeter>(tweeterDto);
                await tweeterRepository.AddAsync(newTweeter);
                await unitOfWork.CompleteWorkAsync();

                await AddRelationUserTweeterAsync(userId, tweeterDto);
            }

        }

        private async Task AddRelationUserTweeterAsync(string userId, TweeterDto tweeterDto)
        {
            var newRelationUserTweeter = new UserTweeter()
            {
                UserId = userId,
                TweeterId = tweeterDto.TweeterId,
                SavedOn = DateTime.Now
            };
            await userTweeterRepository.AddAsync(newRelationUserTweeter);
            await unitOfWork.CompleteWorkAsync();
        }

        public TweeterDto GetTweeterForUser(string userId, string tweeterId)
        {
            var userTweeter = this.userTweeterRepository
                .IncludeDbSet(x => x.User, x => x.Tweeter)
                .SingleOrDefault(relation => relation.UserId == userId && relation.TweeterId == tweeterId && relation.IsDeleted == false);

            if (userTweeter == null)
                throw new ArgumentNullException();

            if (userTweeter.TweeterComments != null)
                return mappingProvider.MapTo<TweeterDto>(userTweeter);

            userTweeter.TweeterComments = string.Empty;
            unitOfWork.CompleteWork();
            return mappingProvider.MapTo<TweeterDto>(userTweeter);
        }

        public IEnumerable<TweeterDto> GetUserFavouriteTweeters(string userId)
        {
            var favoriteTweeters = this.userTweeterRepository
                .IncludeDbSet(x => x.User, x => x.Tweeter)
                .Where(userTweeter => userTweeter.User.Id == userId && userTweeter.IsDeleted == false);

            return this.mappingProvider.ProjectTo<UserTweeter, TweeterDto>(favoriteTweeters);
        }

        //public async Task<IEnumerable<TweeterDto>> GetUserFavouriteTweetersAsync(string userId)
        //{
        //    var favoriteTweeters = await this.tweeterRepository.FindAsync
        //        (tweeter => tweeter.UserTweeters.Any(userTweeter => userTweeter.User.Id == userId && userTweeter.IsDeleted == false));

        //    return this.mappingProvider.ProjectTo<Tweeter, TweeterDto>(favoriteTweeters);
        //}

        public IEnumerable<TweeterDto> GetAllSavedTweetersForAdmin()
        {

            var favoriteTweeters = this.userTweeterRepository
                .IncludeDbSet(x => x.Tweeter, x => x.User)
                .Where(userTweeter => userTweeter.IsDeleted == false).ToList();


            var result = this.mappingProvider.ProjectTo<UserTweeter, TweeterDto>(favoriteTweeters);
            return result;

            //var favoriteTweeters = this.tweeterRepository.GetAll();

            //return this.mappingProvider.ProjectTo<Tweeter, TweeterDto>(favoriteTweeters);
        }

        public async Task AddNoteToSavedTweeterForUserAsync(string userId, string tweeterId, string note)
        {
            var userTweeterForEdit = userTweeterRepository
                .SingleOrDefault(userTweeter => userTweeter.UserId == userId && userTweeter.TweeterId == tweeterId);

            userTweeterForEdit.TweeterComments = note;
            userTweeterForEdit.ModifiedOn = DateTime.Now;

            await unitOfWork.CompleteWorkAsync();
        }

        public async Task RemoveSavedTweeterForUserAsync(string userId, string tweeterId)
        {
            var userTweeter = userTweeterRepository.GetByCompositeId(userId, tweeterId);

            if (userTweeter == null)
            {
                throw new ArgumentException();
            }
            userTweeter.IsDeleted = true;
            userTweeter.DeletedOn = DateTime.Now;
            await unitOfWork.CompleteWorkAsync();
        }

        public void RemoveSavedTweeterForAllUsers(string tweeterId)
        {
            //ZA VSI4ki USERI OBIKALQME I SMENQME FLAGA
        }

        public IEnumerable<TweeterDto> SearchFavoriteTweetersForUser(string userId, string searchString)
        {
            var favoriteTweeters = this.userTweeterRepository
                .IncludeDbSet(x => x.User, x => x.Tweeter)
                .Where(userTweeter => userTweeter.User.Id == userId && userTweeter.IsDeleted == false 
                && userTweeter.Tweeter.ScreenName.ToLower().Contains(searchString.ToLower()));

            return this.mappingProvider.ProjectTo<UserTweeter, TweeterDto>(favoriteTweeters);
        }
    }
}

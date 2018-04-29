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

        public void AddFavoriteTweeterForUser(string userId, TweeterDto tweeter)
        {
            //Does exist in db allready KOQ E PROVERKATA
            try
            {
                //DA SE GETNE
                //PROVERQWAME DALI E ISTRIT I GO PRAIM VIDIM
            }
            catch (Exception)
            {
                //AKO NE SE GETNE DA SE DOBAVI
            }
        }

        public TweeterDto GetFavoriteTweeterForUser(string userId, string tweeterId)
        {

            var tweeter = tweeterRepository.GetById(tweeterId);
            var tweeterDto = mappingProvider.MapTo<TweeterDto>(tweeter);
            return tweeterDto;
        }

        public IEnumerable<TweeterDto> GetUserFavouriteTweeters(string userId)
        {
            var favoriteTweeters = this.tweeterRepository.Find
                (tweeter => tweeter.UserTweeters.Any(userTweeter => userTweeter.User.Id == userId && userTweeter.IsDeleted == false));

            return this.mappingProvider.ProjectTo<Tweeter, TweeterDto>(favoriteTweeters);
        }

        public async Task<IEnumerable<TweeterDto>> GetUserFavouriteTweetersAsync(string userId)
        {
            var favoriteTweeters = await this.tweeterRepository.FindAsync
                (tweeter => tweeter.UserTweeters.Any(userTweeter => userTweeter.User.Id == userId && userTweeter.IsDeleted == false));

            return this.mappingProvider.ProjectTo<Tweeter, TweeterDto>(favoriteTweeters);
        }

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

        public void AddNoteToSavedTweeterForUser(string userId, string tweeterId, string note)
        {
            throw new NotImplementedException();
        }

        public void RemoveSavedTweeterForUser(string userId, string tweeterId)
        {
            //GETVAME GO I MU SMENQME FLAGA
        }

        public void RemoveSavedTweeterForAllUsers(string tweeterId)
        {
            //ZA VSI4ki USERI OBIKALQME I SMENQME FLAGA
        }
    }
}

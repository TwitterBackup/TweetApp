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
    public class TweeterDbService : ITweeterDbService
    {
        private readonly IRepository<UserTweeter> userTweeterRepository;
        private readonly IRepository<ApplicationUser> user;
        private readonly IRepository<Tweeter> tweeterRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMappingProvider mappingProvider;

        public TweeterDbService(IRepository<Tweeter> tweeterRepository, IUnitOfWork unitOfWork, IMappingProvider mappingProvider, IRepository<UserTweeter> userTweeterRepository, IRepository<ApplicationUser> user)
        {
            this.userTweeterRepository = userTweeterRepository;
            this.user = user;
            this.tweeterRepository = tweeterRepository ?? throw new ArgumentNullException(nameof(tweeterRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        public void Add(TweeterDto tweeter)
        {
            throw new NotImplementedException();
        }

        public TweeterDto GetById(string id)
        {
            var tweeter = tweeterRepository.GetById(id);
            var tweeterDto = mappingProvider.MapTo<TweeterDto>(tweeter);
            return tweeterDto;
        }

        public async Task<IEnumerable<TweeterDto>> GetUserFavouriteTweetersAsync(string userId)
        {

            var favoriteTweeters = await this.tweeterRepository.FindAsync
                (tweeter => tweeter.TweeterUsers.Any(userTweeter => userTweeter.User.Id == userId));

            //var userTweeters = await this.userTweeterRepository.FindAsync(ut => ut.UserId == userId);
            //var favoriteTweeters = userTweeters.Select(x => x.Tweeter);

            var result = this.mappingProvider.ProjectTo<Tweeter, TweeterDto>(favoriteTweeters);

            return result;
        }

        public void Update(TweeterDto tweeter)
        {
            throw new NotImplementedException();
        }

        public void Delete(TweeterDto tweeter)
        {
            throw new NotImplementedException();
        }
    }
}

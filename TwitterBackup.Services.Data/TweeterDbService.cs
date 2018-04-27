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
        private readonly IRepository<Tweeter> tweeterRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMappingProvider mappingProvider;

        public TweeterDbService()
        {

        }
        public TweeterDbService(IRepository<Tweeter> tweeterRepository, IUnitOfWork unitOfWork, IMappingProvider mappingProvider)
        {
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
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TweeterDto>> GetUserFavouriteTweetersAsync(string userName)
        {
            return null;
            var favouriteTweeters = await this.tweeterRepository.FindAsync(tweeter =>
                tweeter.TweeterUsers.Any(tweeterUser => tweeterUser.User.UserName == userName));

            //            var result = this.mappingProvider.ProjectTo<TweeterDto>(favouriteTweeters);

            //          return result;
        }

        public async Task<IEnumerable<TweeterDto>> GetUserFavouriteTweetersByCriteriaAsync(string userName, string searchCriteria)
        {
            return null;
            throw new NotImplementedException();
        }

        public void Update(TweeterDto tweeter)
        {
            // get teetwer
            // map to tweeterdto
            // 
            throw new NotImplementedException();
        }

        public void Delete(TweeterDto tweeter)
        {
            throw new NotImplementedException();
        }
    }
}

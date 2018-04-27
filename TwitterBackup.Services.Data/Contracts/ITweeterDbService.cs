using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.DTO.User;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface ITweeterDbService
    {
        void Add(TweeterDto tweeter);

        TweeterDto GetById(string id);

        Task<IEnumerable<TweeterDto>> GetUserFavouriteTweetersAsync(string userId);

        void Update(TweeterDto tweeter);

        void Delete(TweeterDto tweeter);
    }
}

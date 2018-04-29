using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface ITweeterService
    {
        void AddFavoriteTweeterForUser(string userId, TweeterDto tweeter);

        TweeterDto GetTweeterForUser(string userId, string tweeterId);

        IEnumerable<TweeterDto> GetUserFavouriteTweeters(string userId);

        //Task<IEnumerable<TweeterDto>> GetUserFavouriteTweetersAsync(string userId);

        IEnumerable<TweeterDto> GetAllSavedTweetersForAdmin();

        Task AddNoteToSavedTweeterForUserAsync(string userId, string tweeterId, string note);

        Task RemoveSavedTweeterForUserAsync(string userId, string tweeterId);

        void RemoveSavedTweeterForAllUsers(string tweeterId);

    }
}

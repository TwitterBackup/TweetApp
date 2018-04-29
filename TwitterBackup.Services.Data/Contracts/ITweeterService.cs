using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface ITweeterService
    {
        void AddFavoriteTweeterForUser(string userId, TweeterDto tweeter);

        TweeterDto GetFavoriteTweeterForUser(string userId, string tweeterId);

        IEnumerable<TweeterDto> GetUserFavouriteTweeters(string userId);
        Task<IEnumerable<TweeterDto>> GetUserFavouriteTweetersAsync(string userId);

        IEnumerable<TweeterDto> GetAllSavedTweetersForAdmin();

        void AddNoteToSavedTweeterForUser(string userId, string tweeterId, string note);

        void RemoveSavedTweeterForUser(string userId, string tweeterId);

        void RemoveSavedTweeterForAllUsers(string tweeterId);

    }
}

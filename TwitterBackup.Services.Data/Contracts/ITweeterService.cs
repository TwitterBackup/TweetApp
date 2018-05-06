using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface ITweeterService
    {
        Task AddFavoriteTweeterForUserAsync(string userId, TweeterDto tweeterDto);

        TweeterDto GetTweeterForUser(string userId, string tweeterId);

        IEnumerable<TweeterDto> GetUserFavouriteTweeters(string userId);

        IEnumerable<TweeterDto> GetAllSavedTweetersForAdmin();

        Task AddNoteToSavedTweeterForUserAsync(string userId, string tweeterId, string note);

        Task RemoveSavedTweeterForUserAsync(string userId, string tweeterId);

        IEnumerable<TweeterDto> SearchFavoriteTweetersForUser(string userId, string searchString);

        IEnumerable<TweeterDto> SearchFavoriteTweetersForAdmin(string searchString);
    }
}

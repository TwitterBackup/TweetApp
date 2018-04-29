using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.Models;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface ITweeterService
    {
        Task AddFavoriteTweeterForUserAsync(string userId, TweeterDto tweeterDto);

        TweeterDto GetTweeterForUser(string userId, string tweeterId);

        IEnumerable<TweeterDto> GetUserFavouriteTweeters(string userId);

        //Task<IEnumerable<TweeterDto>> GetUserFavouriteTweetersAsync(string userId);

        IEnumerable<TweeterDto> GetAllSavedTweetersForAdmin();

        Task AddNoteToSavedTweeterForUserAsync(string userId, string tweeterId, string note);

        Task RemoveSavedTweeterForUserAsync(string userId, string tweeterId);

        void RemoveSavedTweeterForAllUsers(string tweeterId);

        IEnumerable<TweeterDto> SearchFavoriteTweetersForUser(string userId, string searchString);
    }
}

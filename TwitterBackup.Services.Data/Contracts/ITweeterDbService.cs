using System.Collections.Generic;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.DTO.User;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface ITweeterDbService
    {
        void Add(TweeterDto tweeter);

        TweeterDto GetById(string id);

        IEnumerable<TweeterDto> GetUserFavouriteTweeters(UserDto user);

        void Update(TweeterDto tweeter);

        void Delete(TweeterDto tweeter);
    }
}

using System;
using System.Collections.Generic;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.DTO.User;
using TwitterBackup.Services.Data.Contracts;

namespace TwitterBackup.Services.Data
{
    public class TweeterDbService : ITweeterDbService
    {
        public void Add(TweeterDto tweeter)
        {
            throw new NotImplementedException();
        }

        public TweeterDto GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TweeterDto> GetUserFavouriteTweeters(UserDto user)
        {
            throw new NotImplementedException();
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

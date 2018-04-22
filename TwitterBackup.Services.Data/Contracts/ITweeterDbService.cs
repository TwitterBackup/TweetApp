﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;

namespace TwitterBackup.Services.Data.Contracts
{
    public interface ITweeterDbService
    {
        void Add(TweeterDto tweeter);

        TweeterDto GetById(string id);

        Task<IEnumerable<TweeterDto>> GetUserFavouriteTweetersAsync(string userName);

        Task<IEnumerable<TweeterDto>> GetUserFavouriteTweetersByCriteriaAsync(string userName, string searchCriteria);

        void Update(TweeterDto tweeter);

        void Delete(TweeterDto tweeter);
    }
}

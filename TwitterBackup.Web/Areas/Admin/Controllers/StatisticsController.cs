using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.DTO.User;
using TwitterBackup.Services.Data.Contracts;
using TwitterBackup.Web.Areas.Admin.Models;

namespace TwitterBackup.Web.Areas.Admin.Controllers
{
    [ResponseCache(Duration = 60)] //as noted by ASP.NET Response Caching will be supported in ASP.NET 2.1
    public class StatisticsController : AdminController
    {
        private readonly IMemoryCache memoryCache;

        private readonly ITweeterService tweeterService;
        private readonly ITweetService tweetService;
        private readonly IUserService userService;

        public StatisticsController(IUserService userService, ITweetService tweetService, ITweeterService tweeterService, IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            this.tweeterService = tweeterService ?? throw new ArgumentNullException(nameof(tweeterService));
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<IActionResult> Index()
        {
            //memoryCache.Set<string>("timestamp", DateTime.Now.ToString());


            if (!memoryCache.TryGetValue<ICollection<StatisticsViewModel>>("statsListCached", out ICollection<StatisticsViewModel> statsListCached))
            {

                var tweetsPerUser = tweetService.GetAllTweetsForAdmin().ToList();

                var tweetersPerUser = tweeterService.GetAllSavedTweetersForAdmin().ToList();

                var users = await userService.GetAllUsersAsync();

                var usersList = users as IList<UserDto> ?? users.ToList();

                var statsList = usersList.Select(user => new StatisticsViewModel()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    NumberOfTweets = tweetsPerUser.Count(x => x.User.UserName == user.UserName),
                    NumberOfTweeters = tweetersPerUser.Count(x => x.User.UserName == user.UserName)
                }).ToList();

                          //set the duration of the cached object
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(1),
                    SlidingExpiration = TimeSpan.FromMinutes(1)
                };

                //set the object (statsList) to the inmemory cache
                memoryCache.Set<ICollection<StatisticsViewModel>>("statsListCached", statsList, options);
                memoryCache.Set<ICollection<UserDto>>("usersListCached", usersList, options);

                memoryCache.Set<string>("lastUpdated", DateTime.Now.ToString(), options);
            }

            var modelCached = memoryCache.Get<ICollection<StatisticsViewModel>>("statsListCached");
            var usersListCached = memoryCache.Get<ICollection<UserDto>>("usersListCached");
            ViewData["LastUpdated"] = memoryCache.Get<string>("lastUpdated");

            ViewData["TotalUsers"] = usersListCached.Count();
            ViewData["TotalTweets"] = modelCached.Select(x => x.NumberOfTweets).Sum();
            ViewData["TotalRetweets"] = 0;
            ViewData["TotalTweeters"] = modelCached.Select(x => x.NumberOfTweeters).Sum();

            return View(modelCached);

        }
    }
}
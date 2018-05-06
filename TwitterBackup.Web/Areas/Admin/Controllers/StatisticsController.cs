using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Services.Data.Contracts;
using TwitterBackup.Web.Areas.Admin.Models;

namespace TwitterBackup.Web.Areas.Admin.Controllers
{
    [ResponseCache(Duration = 120)]
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
            var tweetsPerUser = tweetService.GetAllTweetsForAdmin().ToList();

            var tweetersPerUser = tweeterService.GetAllSavedTweetersForAdmin().ToList();

            var users = await userService.GetAllUsersAsync();

            var statsList = users.Select(user => new StatisticsViewModel()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                NumberOfTweets = tweetsPerUser.Count(x => x.User.UserName == user.UserName),
                NumberOfTweeters = tweetersPerUser.Count(x => x.User.UserName == user.UserName)
            }).ToList();

            ViewData["TotalUsers"] = users.Count();
            ViewData["TotalTweets"] = statsList.Select(x => x.NumberOfTweets).Sum();
            ViewData["TotalRetweets"] = 0;
            ViewData["TotalTweeters"] = statsList.Select(x => x.NumberOfTweeters).Sum();

            return View(statsList);

        }
    }
}
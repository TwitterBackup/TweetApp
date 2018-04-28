using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Services.Data.Contracts;
using TwitterBackup.Services.TwitterAPI.Contracts;
using TwitterBackup.Web.Models.TweeterViewModels;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class FavouriteTweetersController : Controller
    {
        private readonly ITweeterService tweeterService;
        private readonly ITweeterDbService tweeterDbService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMappingProvider mappingProvider;

        public FavouriteTweetersController(
            ITweeterService tweeterService,
            ITweeterDbService tweeterDbService,
            IHttpContextAccessor httpContextAccessor,
            IMappingProvider mappingProvider)
        {
            this.tweeterService = tweeterService ?? throw new ArgumentNullException(nameof(tweeterService));
            this.tweeterDbService = tweeterDbService ?? throw new ArgumentNullException(nameof(tweeterDbService));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        [HttpGet]
        public async Task<IActionResult> SearchResults(string searchString)
        {
            var searchResult = await this.tweeterService.SearchTweetersAsync(searchString);

            var userName = this.httpContextAccessor.HttpContext.User.Identity.Name;

            var userFavourites = await this.tweeterDbService.GetUserFavouriteTweetersByCriteriaAsync(userName, searchString);

            if (searchResult == null && userFavourites == null)
            {
                return this.View();
            }

            if (searchResult == null)
            {
                var result = this.mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(userFavourites);

                foreach (var tweeterViewModel in result)
                {
                    tweeterViewModel.IsLikedFromUser = true;
                }

                return this.View(result);
            }

            if (userFavourites == null)
            {
                var result = this.mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(searchResult).ToList();

                return this.View(result);
            }

            var userFavouriteSet = this.mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(userFavourites).ToHashSet();
            var searchResultSet = this.mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(searchResult);

            foreach (var tweeterViewModel in userFavouriteSet)
            {
                tweeterViewModel.IsLikedFromUser = true;
            }

            var mergedResult = userFavouriteSet.Union(searchResultSet);

            return this.View(mergedResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTweeterToFavourite(TweeterViewModel tweeterViewModel)
        {
            if (this.ModelState.IsValid)
            {
                var tweeterDto = await this.tweeterService.GetTweeterByScreenNameAsync(tweeterViewModel.ScreenName);

                if (tweeterDto == null)
                {
                    return this.Json("Invalid tweeter");
                }

                var usernName = this.httpContextAccessor.HttpContext.User.Identity.Name;

                try
                {
                    this.tweeterDbService.AddTweeterToFavourite(tweeterDto, usernName);
                }
                catch (Exception e)
                {
                    return this.Json("unable to add tweeter to favourite");
                }

                return this.Json("success");
            }

            return this.Json("error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveTweeterFromFavourite(TweeterViewModel tweeterViewModel)
        {
            if (this.ModelState.IsValid)
            {
                var tweeterDto = await this.tweeterService.GetTweeterByScreenNameAsync(tweeterViewModel.ScreenName);

                if (tweeterDto == null)
                {
                    return this.Json("Invalid tweeter");
                }

                var usernName = this.httpContextAccessor.HttpContext.User.Identity.Name;

                try
                {
                    this.tweeterDbService.RemoveTweeterFromFavourite(tweeterDto, usernName);
                }
                catch (Exception e)
                {
                    return this.Json("unable to remove tweeter to favourite.");
                }

                return this.Json("success");
            }

            return this.Json("error");
        }
    }
}

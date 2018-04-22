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
            this.tweeterService = tweeterService ?? throw new System.ArgumentNullException(nameof(tweeterService));
            this.tweeterDbService = tweeterDbService ?? throw new System.ArgumentNullException(nameof(tweeterDbService));
            this.httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchResults(string searchString)
        {
            var searchResult = await this.tweeterService.SearchTweetersAsync(searchString);

            var userName = this.httpContextAccessor.HttpContext.User.Identity.Name;

            var userFavourites = await this.tweeterDbService.GetUserFavouriteTweetersByCriteriaAsync(userName, searchString);

            if (searchResult == null && userFavourites == null)
            {
                return this.View();
            }

            if (userFavourites == null)
            {
                var result = this.mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(searchResult);

                foreach (var tweeterViewModel in result)
                {
                    tweeterViewModel.IsLikedFromUser = true;
                }

                return this.View(result);
            }

            if (searchResult == null)
            {
                var result = this.mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(userFavourites);

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
    }
}

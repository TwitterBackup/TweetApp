using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;
using TwitterBackup.Services.TwitterAPI.Contracts;
using TwitterBackup.Web.Models.TweeterViewModels;
using TwitterBackup.Web.Models.TweetViewModels;
using EditTweeterDto = TwitterBackup.DTO.Tweeters.EditTweeterDto;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class TweetersController : Controller
    {
        private readonly ITweeterApiService tweeterApiService;
        private readonly ITweetApiService tweetApiService;
        private readonly IUserService userService;
        private readonly IUserService userDbService;
        private readonly ITweeterService tweeterService;
        private readonly ITweetService tweetService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMappingProvider mappingProvider;

        public TweetersController(UserManager<ApplicationUser> userManager, IMappingProvider mappingProvider,
            ITweeterService tweeterService, ITweetService tweetService, IUserService userDbService, IUserService userService,
            ITweeterApiService tweeterApiService, ITweetApiService tweetApiService)
        {
            this.tweeterApiService = tweeterApiService ?? throw new ArgumentNullException(nameof(tweeterApiService));
            this.tweetApiService = tweetApiService ?? throw new ArgumentNullException(nameof(tweetApiService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.userDbService = userDbService ?? throw new ArgumentNullException(nameof(userDbService));
            this.tweeterService = tweeterService ?? throw new ArgumentNullException(nameof(tweeterService));
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        private bool CurrentUserIsAdmin()
        {
            return this.HttpContext.User.IsInRole("Admin");
        }

        // GET: Tweeter
        public async Task<IActionResult> Index()
        {
            IEnumerable<TweeterDto> tweetersDto;

            if (this.CurrentUserIsAdmin())
            {
                tweetersDto = this.tweeterService.GetAllSavedTweetersForAdmin();
                this.ViewData["IsAdmin"] = true;
            }
            else
            {
                var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);
                tweetersDto = this.tweeterService.GetUserFavouriteTweeters(currentUser.Id);
                this.ViewData["IsAdmin"] = false;
            }

            var tweeterViewModels = this.mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(tweetersDto);

            if (!tweeterViewModels.Any())
            {
                this.TempData["Result"] = "No saved Tweeters at this moment!";
            }

            return this.View(tweeterViewModels);

        }

        // GET: Tweeter/Edit/5
        public async Task<IActionResult> Edit(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                this.TempData["Result"] = "UserName cannot be null or whitespace.";
                return this.RedirectToAction(nameof(this.Index));
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                this.TempData["Result"] = "No tweeter selected!";
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["IsAdmin"] = this.CurrentUserIsAdmin();

            var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);


            if (this.ModelState.IsValid)
            {
                string userId;

                try
                {
                    userId = this.CurrentUserIsAdmin() ? this.userDbService.FindUserIdByUserName(name) : currentUser.Id;
                }

                catch (ArgumentException)
                {
                    this.TempData["Result"] = "You haven't saved such Tweeter. Nothing is deleted.";
                    return this.RedirectToAction(nameof(this.Index));
                }

                var tweeterDto = this.tweeterService.GetTweeterForUser(userId, id);
                if (tweeterDto == null)
                {
                    this.TempData["Result"] = "Such tweet is not found. Please check and try again!";
                    return this.RedirectToAction(nameof(this.Index));
                }

                var editTweeterViewModel = this.mappingProvider.MapTo<EditTweeterViewModel>(tweeterDto);
                if (editTweeterViewModel.TweeterComments == null)
                    editTweeterViewModel.TweeterComments = "";
                editTweeterViewModel.UserName = name;

                return this.PartialView(editTweeterViewModel);
            }
            else
            {
                this.TempData["Result"] = "Something went wrong. No tweet was deleted";
                return this.RedirectToAction(nameof(this.Index));
            }
        }

        // POST: Tweeter/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(EditTweeterViewModel tweeterForEdit)
        {
            if (tweeterForEdit.TweeterId == null)
            {
                this.ViewData["Result"] = "Such tweeter is not found. Please check and try again!";
                return this.RedirectToAction(nameof(this.Index));
            }

            if (this.CurrentUserIsAuthorizedAsync(tweeterForEdit.TweeterId) == false)
            {
                this.ViewData["Result"] = "You do not have right to edit this tweeter!";
                return this.RedirectToAction(nameof(this.Index));
            }

            var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                try
                {
                    var userId = this.CurrentUserIsAdmin()
                        ? this.userService.FindUserIdByUserName(tweeterForEdit.UserName)
                        : currentUser.Id;

                    var tweetDto = this.mappingProvider.MapTo<EditTweeterDto>(tweeterForEdit);
                    await this.tweeterService.AddNoteToSavedTweeterForUserAsync(userId, tweeterForEdit.TweeterId,
                        tweeterForEdit.TweeterComments);
                    this.TempData["Result"] = "Tweet was successfully edited";
                    return this.RedirectToAction(nameof(this.Index));
                }
                catch (ArgumentException)
                {
                    this.TempData["Result"] = "You haven't saved such Tweet. No tweet edited.";
                    return this.RedirectToAction(nameof(this.Index));
                }
            }

            this.TempData["Result"] = "Something went wrong. No tweet was edited";
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Tweeter/Delete/5
        public async Task<IActionResult> Delete(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                this.TempData["Result"] = "UserName cannot be null or whitespace.";
                return this.RedirectToAction(nameof(this.Index));
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                this.TempData["Result"] = "No tweet selected!";
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["IsAdmin"] = this.CurrentUserIsAdmin();

            var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);


            if (this.ModelState.IsValid)
            {
                var userId = string.Empty;

                try
                {
                    userId = this.CurrentUserIsAdmin() ? this.userService.FindUserIdByUserName(name) : currentUser.Id;
                }

                catch (ArgumentException)
                {
                    this.TempData["Result"] = "You haven't saved such Tweeter. No tweeter deleted.";
                    return this.RedirectToAction(nameof(this.Index));
                }

                var tweeterDto = this.tweeterService.GetTweeterForUser(userId, id);
                if (tweeterDto == null)
                {
                    this.TempData["Result"] = "Such tweeter is not found. Please check and try again!";
                    return this.RedirectToAction(nameof(this.Index));
                }

                var tweeter = this.mappingProvider.MapTo<TweeterViewModel>(tweeterDto);
                tweeter.UserName = name;

                return this.PartialView(tweeter);
            }
            else
            {
                this.TempData["Result"] = "Something went wrong. No tweeter was deleted";
                return this.RedirectToAction(nameof(this.Index));
            }

        }

        // POST: Tweeter/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id, string UserName)
        {
            if (UserName == null)
            {
                this.TempData["Result"] = "User cannot be null!";
                return this.RedirectToAction(nameof(this.Index));
            }

            if (id == null)
            {
                this.TempData["Result"] = "Such tweeter does not exist. ";
                return this.RedirectToAction(nameof(this.Index));
            }

            if (this.CurrentUserIsAuthorizedAsync(id) == false)
            {
                this.TempData["Result"] = "You do not have right to edit this tweeter!";
                return this.RedirectToAction(nameof(this.Index));
            }

            var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                try
                {
                    var userId = this.CurrentUserIsAdmin() ? this.userService.FindUserIdByUserName(UserName) : currentUser.Id;
                    await this.tweeterService.RemoveSavedTweeterForUserAsync(userId, id);
                    this.TempData["Result"] = "Tweet was successfully deleted";
                    return this.RedirectToAction(nameof(this.Index));
                }
                catch (ArgumentException)
                {
                    this.TempData["Result"] = "You haven't saved such Tweet. No tweet deleted.";
                    return this.RedirectToAction(nameof(this.Index));
                }
            }

            this.TempData["Result"] = "Something went wrong. No tweet was deleted";
            return this.RedirectToAction(nameof(this.Index));

        }


        private bool CurrentUserIsAuthorizedAsync(string resourceId)
        {
            if (this.CurrentUserIsAdmin())
                return true;
            else
            {
                var currentUserId = this.userManager.GetUserId(this.HttpContext.User);
                var resource = this.tweeterService.GetTweeterForUser(currentUserId, resourceId);

                return resource != null;
            }
        }





        //---------------------





        [HttpGet]
        public async Task<IActionResult> SearchResults(string searchString)
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);


            var accessToken = await this.userManager.GetAuthenticationTokenAsync(user, "Twitter", "access_token");
            var accessTokenSecret = await this.userManager.GetAuthenticationTokenAsync(user, "Twitter", "access_token_secret");

            var searchResult = await this.tweeterApiService.SearchTweetersAsync(searchString);

            var userId = this.userManager.GetUserId(this.HttpContext.User);

            var tweeterDtos = this.tweeterService.SearchFavoriteTweetersForUser(userId, searchString);

            if (searchResult == null && !tweeterDtos.Any())
            {
                return this.View();
            }

            if (searchResult == null)
            {
                var result = this.mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(tweeterDtos);

                foreach (var tweeterViewModel in result)
                {
                    tweeterViewModel.IsLikedFromUser = true;
                }

                return this.View(result);
            }

            ICollection<TweeterDto> resultsList = null;

            if (searchResult != null)
            {
                resultsList = searchResult.ToList();
                foreach (var result in resultsList)
                {
                    result.User = new ApplicationUser();
                    result.UserName = string.Empty;
                }
            }

            if (!tweeterDtos.Any())
            {
                var resultViewModel = this.mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(resultsList);

                return this.View(resultViewModel);
            }

            var userFavouriteSet = this.mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(tweeterDtos)
                .ToHashSet();
            var searchResultSet = this.mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(resultsList)
                .ToHashSet();

            //foreach (var tweeterViewModel in userFavouriteSet)
            //{
            //    tweeterViewModel.IsLikedFromUser = true;
            //}

            //var mergedResult = userFavouriteSet.Union(searchResultSet);

            //return this.View(mergedResult);

            foreach (var tweeterViewModel in userFavouriteSet)
            {
                if (searchResultSet.Any(x => x.TweeterId == tweeterViewModel.TweeterId))
                    searchResultSet
                        .SingleOrDefault(x => x.TweeterId == tweeterViewModel.TweeterId)
                        .IsLikedFromUser = true;
                else
                {
                    tweeterViewModel.IsLikedFromUser = true;
                    searchResultSet.Add(tweeterViewModel);
                }
            }

            return this.View(searchResultSet.OrderByDescending(tweeter => tweeter.IsLikedFromUser));

        }

        [HttpPost]
        public async Task<IActionResult> AddTweeterToFavourite(TweeterViewModel tweeterViewModel)
        {
            if (this.ModelState.IsValid)
            {
                var tweeterDto = await this.tweeterApiService.GetTweeterByScreenNameAsync(tweeterViewModel.ScreenName);

                if (tweeterDto == null)
                {
                    return this.Json("Invalid tweeter.");
                }

                var userId = this.userManager.GetUserId(this.HttpContext.User);

                try
                {
                    await this.tweeterService.AddFavoriteTweeterForUserAsync(userId, tweeterDto);
                }
                catch (Exception e)
                {
                    var error = e.Message;

                    return this.Json("Unable to add tweeter to favourite.");
                }

                return this.Json("success");
            }

            return this.Json("Tweeter shoud have screen name.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveTweeterFromFavourite(TweeterViewModel tweeterViewModel)
        {
            if (this.ModelState.IsValid)
            {
                var tweeterDto = await this.tweeterApiService.GetTweeterByScreenNameAsync(tweeterViewModel.ScreenName);

                if (tweeterDto == null)
                {
                    return this.Json("Invalid tweeter.");
                }

                var userId = this.userManager.GetUserId(this.HttpContext.User);

                try
                {
                    await this.tweeterService.RemoveSavedTweeterForUserAsync(userId, tweeterDto.TweeterId);
                }
                catch (Exception)
                {
                    return this.Json("Unable to remove tweeter to favourite.");
                }

                return this.Json("success");
            }

            return this.Json("Tweeter shoud have screen name.");


        }

        [AllowAnonymous]
        public async Task<IActionResult> Profile(string tweeterId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);

            var tweeter = this.tweeterService.GetTweeterForUser(currentUser.Id, tweeterId);
            var savedTweets = this.tweetService.GetAllTweetsByTweeterForUser(currentUser.Id, tweeterId);
            var newTweets = await this.tweetApiService.GetUserTimelineAsync(tweeterId);

            var tweeterViewModel = this.mappingProvider.MapTo<TweeterViewModel>(tweeter);
            tweeterViewModel.IsLikedFromUser = true;
            var savedTweetsViewModels = this.mappingProvider.ProjectTo<TweetDto, TweetViewModel>(savedTweets).ToList();

            foreach (var savedTweetsViewModel in savedTweetsViewModels)
            {
                savedTweetsViewModel.IsLikedFromUser = true;
            }

            var newTweesViewModel = new List<TweetViewModel>();
            if (newTweets != null)
            {
                foreach (var apiTweetDto in newTweets)
                {
                    if (savedTweets.Any(tweet => tweet.TweetId == apiTweetDto.TweetId))
                    {
                        continue;
                    }
                    var tweetViewModel = new TweetViewModel()
                    {
                        CreatedAt = apiTweetDto.CreatedAt,
                        FavoriteCount = apiTweetDto.FavoriteCount,
                        Language = apiTweetDto.Language,
                        QuoteCount = apiTweetDto.QuoteCount,
                        RetweetCount = apiTweetDto.RetweetCount,
                        Text = apiTweetDto.Text,
                        TweetComments = apiTweetDto.TweetComments,
                        Tweeter = new Tweeter()
                        {
                            Name = apiTweetDto.Tweeter.Name,
                            ScreenName = apiTweetDto.Tweeter.ScreenName
                        },
                        TweeterName = apiTweetDto.TweeterName,
                        TweetId = apiTweetDto.TweetId
                    };

                    if (apiTweetDto.Hashtags != null)
                    {
                        tweetViewModel.Hashtags = string.Join(" ", apiTweetDto.Hashtags);
                    }

                    newTweesViewModel.Add(tweetViewModel);
                }
            }

            var profileViewModel = new TweeterProfileViewModel()
            {
                Tweeter = tweeterViewModel,
                SavedTweets = savedTweetsViewModels,
                NewTweets = newTweesViewModel
            };

            return this.View(profileViewModel);
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;
using TwitterBackup.Services.TwitterAPI.Contracts;
using TwitterBackup.Web.Models.TweetViewModels;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class TweetsController : Controller
    {
        private readonly ITweetApiService tweetApiService;
        private readonly IUserService userService;
        private readonly IMappingProvider mappingProvider;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITweetService tweetService;

        public TweetsController(ITweetService tweetService, IMappingProvider mappingProvider, UserManager<ApplicationUser> userManager,
            IUserService userService, ITweetApiService tweetApiService)
        {
            this.tweetApiService = tweetApiService ?? throw new ArgumentNullException(nameof(tweetApiService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        private bool CurrentUserIsAdmin()
        {
            return this.HttpContext.User.IsInRole("Admin");
        }


        //Add: SavedTweets
        public async Task<IActionResult> Add(string tweetId)
        {
            if (this.ModelState.IsValid)
            {
                var apiTweetDto = await this.tweetApiService.GetTweetByIdAsync(tweetId);
                var userId = this.userManager.GetUserId(this.HttpContext.User);

                await this.tweetService.AddTweetForUserAsync(userId, apiTweetDto);

                this.TempData["Result"] = "Tweet saved successfully";

                return this.RedirectToAction(nameof(this.Index));
            }

            this.TempData["Result"] = "Something went wrong. Please try again!";
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: SavedTweets
        public async Task<IActionResult> Index()
        {
            IEnumerable<TweetDto> tweetsDto;

            if (this.CurrentUserIsAdmin())
            {
                tweetsDto = this.tweetService.GetAllTweetsForAdmin();
                this.ViewData["IsAdmin"] = true;
            }
            else
            {
                var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);
                tweetsDto = this.tweetService.GetAllTweetsForUser(currentUser.Id);
                this.ViewData["IsAdmin"] = false;
            }

            var tweetViewModels = this.mappingProvider.ProjectTo<TweetDto, TweetViewModel>(tweetsDto);

            if (!tweetViewModels.Any())
            {
                this.TempData["Result"] = "No saved SavedTweets at this moment!";
            }

            var likedTweets = tweetViewModels.ToList();

            likedTweets.ForEach(tweet => tweet.IsLikedFromUser = true);

            this.ViewData["NoSavedTweetsMessage"] = "No saved tweets";

            return this.View("AllTweets", likedTweets);
        }

        public async Task<IActionResult> TweeterTweetsLikedFromUser(string tweeterId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);

            var tweets = this.tweetService.GetAllTweetsByTweeterForUser(currentUser.Id, tweeterId);

            var tweetsViewModel = this.mappingProvider.ProjectTo<TweetDto, TweetViewModel>(tweets);

            var likedTweets = tweetsViewModel.ToList();

            likedTweets.ForEach(tweet => tweet.IsLikedFromUser = true);

            this.ViewData["NoSavedTweetsMessage"] = "No saved tweets";

            return this.PartialView("AllTweets", likedTweets);
        }

        public async Task<IActionResult> TweeterNewTweets(string tweeterId)
        {
            var tweetsNew = await this.tweetApiService.GetUserTimelineAsync(tweeterId);

            var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);

            var tweetsSaved = this.tweetService.GetAllTweetsByTweeterForUser(currentUser.Id, tweeterId);

            var unsavedTweets = tweetsNew.ToList();

            unsavedTweets.RemoveAll(newTweet => tweetsSaved.Any(savedTweet => savedTweet.TweetId == newTweet.TweetId));

            var tweetsViewModel = new List<TweetViewModel>();
            if (unsavedTweets != null)
            {
                foreach (var apiTweetDto in unsavedTweets)
                {
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
                            ScreenName = apiTweetDto.Tweeter.ScreenName,
                            ProfileImageUrl = apiTweetDto.Tweeter.ProfileImageUrl,
                            TweeterId = apiTweetDto.TweetId
                        },
                        TweeterName = apiTweetDto.TweeterName,
                        UserName = currentUser.UserName,
                        TweetId = apiTweetDto.TweetId
                    };

                    if (apiTweetDto.Hashtags != null)
                    {
                        tweetViewModel.Hashtags = string.Join(" ", apiTweetDto.Hashtags);
                    }

                    tweetsViewModel.Add(tweetViewModel);
                }
            }

            // var tweetsViewModel = this.mappingProvider.ProjectTo<ApiTweetDto, TweetViewModel>(tweets);
            this.ViewData["NoSavedTweetsMessage"] = "No new tweets";

            return this.PartialView("AllTweets", tweetsViewModel);
        }

        // GET: SavedTweets/Edit/5
        public async Task<IActionResult> Edit(string id, string name)
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
                string userId;

                try
                {
                    userId = this.CurrentUserIsAdmin() ? this.userService.FindUserIdByUserName(name) : currentUser.Id;
                }

                catch (ArgumentException)
                {
                    this.TempData["Result"] = "You haven't saved such Tweet. No tweet deleted.";
                    return this.RedirectToAction(nameof(this.Index));
                }

                var tweetDto = this.tweetService.GetTweetForUser(userId, id);
                if (tweetDto == null)
                {
                    this.TempData["Result"] = "Such tweet is not found. Please check and try again!";
                    return this.RedirectToAction(nameof(this.Index));
                }

                var tweet = this.mappingProvider.MapTo<EditTweetViewModel>(tweetDto);

                return this.PartialView(tweet);
            }
            else
            {
                this.TempData["Result"] = "Something went wrong. No tweet was deleted";
                return this.RedirectToAction(nameof(this.Index));
            }
        }

        // POST: SavedTweets/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(EditTweetViewModel tweetForEdit)
        {
            if (tweetForEdit.TweetId == null)
            {
                this.ViewData["Result"] = "Such tweet is not found. Please check and try again!";
                return this.RedirectToAction(nameof(this.Index));
            }

            if (this.CurrentUserIsAuthorizedAsync(tweetForEdit.TweetId) == false)
            {
                this.ViewData["Result"] = "You do not have right to edit this tweet!";
                return this.RedirectToAction(nameof(this.Index));
            }

            var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                try
                {
                    var userId = this.CurrentUserIsAdmin() ? this.userService.FindUserIdByUserName(tweetForEdit.UserName) : currentUser.Id;

                    var tweetDto = this.mappingProvider.MapTo<EditTweeterDto>(tweetForEdit);
                    await this.tweetService.AddNoteToSavedTweetForUserAsync(userId, tweetForEdit.TweetId, tweetForEdit.TweetComments);
                    this.TempData["Result"] = "Tweet was successfully edited";
                    return this.Json("success");
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

        // GET: Tweets/Remove/5
        public async Task<IActionResult> Remove(string id, string name)
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
                    TempData["Result"] = "You haven't saved such Tweet. No tweet removed.";
                    return RedirectToAction(nameof(Index));
                }

                var tweetDto = this.tweetService.GetTweetForUser(userId, id);
                if (tweetDto == null)
                {
                    this.TempData["Result"] = "Such tweet is not found. Please check and try again!";
                    return this.RedirectToAction(nameof(this.Index));
                }

                var tweet = this.mappingProvider.MapTo<TweetViewModel>(tweetDto);
                tweet.UserName = name;

                return this.PartialView(tweet);
            }
            else
            {
                TempData["Result"] = "Something went wrong. No tweet was removed";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Tweets/Remove/5
        [HttpPost, ActionName("Remove")]
        public async Task<IActionResult> RemoveConfirmed(string tweetId, string userName)
        {
            if (userName == null)
            {
                this.TempData["Result"] = "User cannot be null!";
                return this.RedirectToAction(nameof(this.Index));
            }

            if (tweetId == null)
            {
                this.TempData["Result"] = "Such tweet does not exist. ";
                return this.RedirectToAction(nameof(this.Index));
            }

            if (CurrentUserIsAuthorizedAsync(tweetId) == false)
            {
                TempData["Result"] = "You do not have right to remove this tweet!";
                return RedirectToAction(nameof(Index));
            }

            var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                try
                {
                    var userId = CurrentUserIsAdmin() ? userService.FindUserIdByUserName(userName) : currentUser.Id;
                    await tweetService.RemoveSavedTweetForUserAsync(userId, tweetId);
                    TempData["Result"] = "Tweet was successfully removed";
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException)
                {
                    TempData["Result"] = "You haven't saved such Tweet. No tweet removed.";
                    return RedirectToAction(nameof(Index));
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
                var resource = this.tweetService.GetTweetForUser(currentUserId, resourceId);

                return resource != null;
            }
        }
    }
}

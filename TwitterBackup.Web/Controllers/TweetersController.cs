using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;
using TwitterBackup.Services.TwitterAPI.Contracts;
using TwitterBackup.Web.Models.TweeterViewModels;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class TweetersController : Controller
    {
        private readonly ITweeterApiService tweeterApiService;
        private readonly IUserService userService;
        private readonly ITweeterService tweeterService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMappingProvider mappingProvider;

        public TweetersController(UserManager<ApplicationUser> userManager, IMappingProvider mappingProvider,
            ITweeterService tweeterService, IUserService userService, ITweeterApiService tweeterApiService)
        {
            this.tweeterApiService = tweeterApiService ?? throw new ArgumentNullException(nameof(tweeterApiService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.tweeterService = tweeterService ?? throw new ArgumentNullException(nameof(tweeterService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        public ActionResult MyImage(string url)
        {
            // Get this from your database
            string absoluteImagePath = url;
            return Json(absoluteImagePath);
        }

        // GET: Tweeter
        public async Task<IActionResult> Index(string userName, string searchString)
        {
            IEnumerable<TweeterDto> tweetersDto;
            bool isAdmin = false;

            if (CurrentUserIsAdmin())
            {
                if (userName != null) //get tweets for specific user
                {
                    var userId = userService.FindUserIdByUserName(userName);
                    tweetersDto = searchString != null ? tweeterService.SearchFavoriteTweetersForUser(userId, searchString) : tweeterService.GetUserFavouriteTweeters(userId);
                }
                else
                {
                    tweetersDto = searchString != null ? tweeterService.SearchFavoriteTweetersForAdmin(searchString) : tweeterService.GetAllSavedTweetersForAdmin();
                }
                isAdmin = true;

            }
            else
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);

                tweetersDto = searchString != null ? tweeterService.SearchFavoriteTweetersForUser(currentUser.Id, searchString) : tweeterService.GetUserFavouriteTweeters(currentUser.Id);
            }

            var tweeterViewModels = mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(tweetersDto).ToList();

            if (!tweeterViewModels.Any())
            {
                TempData["Result"] = "No saved Tweeters at this moment!";
            }

            foreach (var tweeter in tweeterViewModels)
            {
                tweeter.IsAdmin = isAdmin;
                tweeter.IsLikedFromUser = true;
            }

            return View(tweeterViewModels);

        }

        // GET: Tweeter/Edit/5
        public async Task<IActionResult> Edit(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Result"] = "UserName cannot be null or whitespace.";
                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["Result"] = "No tweeter selected!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["IsAdmin"] = CurrentUserIsAdmin();

            var currentUser = await userManager.GetUserAsync(HttpContext.User);


            if (ModelState.IsValid)
            {
                string userId;

                try
                {
                    userId = this.CurrentUserIsAdmin() ? userService.FindUserIdByUserName(name) : currentUser.Id;
                }

                catch (ArgumentException)
                {
                    TempData["Result"] = "You haven't saved such Tweeter. Nothing is deleted.";
                    return RedirectToAction(nameof(Index));
                }

                var tweeterDto = tweeterService.GetTweeterForUser(userId, id);
                if (tweeterDto == null)
                {
                    TempData["Result"] = "Such tweet is not found. Please check and try again!";
                    return RedirectToAction(nameof(Index));
                }

                var editTweeterViewModel = mappingProvider.MapTo<EditTweeterViewModel>(tweeterDto);
                if (editTweeterViewModel.TweeterComments == null)
                    editTweeterViewModel.TweeterComments = "";
                editTweeterViewModel.UserName = name;

                return PartialView(editTweeterViewModel);
            }
            else
            {
                TempData["Result"] = "Something went wrong. No tweet was deleted";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Tweeter/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(EditTweeterViewModel tweeterForEdit)
        {
            if (tweeterForEdit.TweeterId == null)
            {
                ViewData["Result"] = "Such tweeter is not found. Please check and try again!";
                return RedirectToAction(nameof(Index));
            }

            if (CurrentUserIsAuthorizedAsync(tweeterForEdit.TweeterId) == false)
            {
                ViewData["Result"] = "You do not have right to edit this tweeter!";
                return RedirectToAction(nameof(Index));
            }

            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = CurrentUserIsAdmin()
                        ? userService.FindUserIdByUserName(tweeterForEdit.UserName)
                        : currentUser.Id;

                    mappingProvider.MapTo<EditTweeterDto>(tweeterForEdit);

                    await tweeterService.AddNoteToSavedTweeterForUserAsync(userId, tweeterForEdit.TweeterId,
                        tweeterForEdit.TweeterComments);
                    TempData["Result"] = "Tweet was successfully edited";
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException)
                {
                    TempData["Result"] = "You haven't saved such Tweet. No tweet edited.";
                    return RedirectToAction(nameof(Index));
                }
            }

            TempData["Result"] = "Something went wrong. No tweeter was edited";
            return RedirectToAction(nameof(Index));
        }

        // GET: Tweeter/Remove/5
        public async Task<IActionResult> Remove(string tweeterId, string userName, [FromHeader] string referer)
        {
            if (userName == null) //this happens only when Add tweeter to favorites and immediately try to remove it (without refresh)
                userName = userManager.GetUserName(HttpContext.User);

            ViewData["returnUrl"] = referer;

            if (string.IsNullOrWhiteSpace(userName))
            {
                TempData["Result"] = "UserName cannot be null or whitespace.";
                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrWhiteSpace(tweeterId))
            {
                TempData["Result"] = "No tweeter selected!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["IsAdmin"] = CurrentUserIsAdmin();

            var currentUser = await userManager.GetUserAsync(HttpContext.User);


            if (ModelState.IsValid)
            {
                var userId = string.Empty;

                try
                {
                    userId = CurrentUserIsAdmin() ? userService.FindUserIdByUserName(userName) : currentUser.Id;
                }

                catch (ArgumentException)
                {
                    TempData["Result"] = "You haven't saved such Tweeter. No tweeter removed.";
                    return RedirectToAction(nameof(Index));
                }

                var tweeterDto = tweeterService.GetTweeterForUser(userId, tweeterId);
                if (tweeterDto == null)
                {
                    TempData["Result"] = "Such tweeter is not found. Please check and try again!";
                    return RedirectToAction(nameof(Index));
                }

                var tweeter = mappingProvider.MapTo<TweeterViewModel>(tweeterDto);

                return PartialView(tweeter);
            }
            else
            {
                TempData["Result"] = "Something went wrong. No tweeter was removed";
                return RedirectToAction(nameof(Index));
            }

        }

        // POST: Tweeter/Remove/5
        [HttpPost, ActionName("Remove")]
        public async Task<IActionResult> RemoveConfirmed(string tweeterId, string userName, string screenName, [FromHeader] string referer, string returnUrl)
        {
            if (userName == null)
            {
                TempData["Result"] = "User cannot be null!";
                return RedirectToAction(nameof(Index));
            }

            if (tweeterId == null)
            {
                TempData["Result"] = "Such tweeter does not exist. ";
                return RedirectToAction(nameof(Index));
            }

            if (CurrentUserIsAuthorizedAsync(tweeterId) == false)
            {
                TempData["Result"] = "You do not have right to remove this tweeter!";
                return RedirectToAction(nameof(Index));
            }

            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = CurrentUserIsAdmin() ? userService.FindUserIdByUserName(userName) : currentUser.Id;
                    await tweeterService.RemoveSavedTweeterForUserAsync(userId, tweeterId);
                    //TempData["Result"] = "Tweeter was successfully removed";
                    //return RedirectToAction(nameof(Index));
                    return RedirectToLocal(returnUrl, "" /* "Tweeter was successfully removed from favorites"*/);
                }
                catch (ArgumentException)
                {
                    TempData["Result"] = "You haven't saved such Tweeter. No tweeter removed.";
                    return RedirectToAction(nameof(Index));
                }
            }

            TempData["Result"] = "Something went wrong. No tweeter was removed";
            //return RedirectToAction(nameof(Index));
            return RedirectToLocal(returnUrl, "Hello");

        }


        private bool CurrentUserIsAuthorizedAsync(string resourceId)
        {
            if (this.CurrentUserIsAdmin())
                return true;
            else
            {
                var currentUserId = userManager.GetUserId(HttpContext.User);
                var resource = tweeterService.GetTweeterForUser(currentUserId, resourceId);

                return resource != null;
            }
        }



        [HttpGet]
        public async Task<IActionResult> SearchResults(string searchString, [FromHeader] string referer)
        {
            var returnUrl = referer;
            var searchResult = await this.tweeterApiService.SearchTweetersAsync(searchString);

            var userId = this.userManager.GetUserId(HttpContext.User);

            var tweeterDtos = this.tweeterService.SearchFavoriteTweetersForUser(userId, searchString);

            if (searchResult == null && !tweeterDtos.Any())
            {
                var resultMessage = "No tweeters found. Please try again!";
                return RedirectToLocal(returnUrl, resultMessage);
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

            foreach (var tweeterViewModel in userFavouriteSet)
            {
                if (searchResultSet.Any(x => x.TweeterId == tweeterViewModel.TweeterId))
                {
                    var favoriteTweeter =
                        searchResultSet.SingleOrDefault(x => x.TweeterId == tweeterViewModel.TweeterId);
                    favoriteTweeter.IsLikedFromUser = true;
                    favoriteTweeter.User = tweeterViewModel.User;
                    favoriteTweeter.UserName = tweeterViewModel.UserName;
                }
                else
                {
                    tweeterViewModel.IsLikedFromUser = true;
                    searchResultSet.Add(tweeterViewModel);
                }
            }

            var orderedSearchResultSet = searchResultSet.Select(x => x).OrderByDescending(x => x.IsLikedFromUser);

            return this.View(orderedSearchResultSet);

        }

        [HttpPost]
        public async Task<IActionResult> AddTweeterToFavourite(string screenName, string tweeterId, [FromHeader] string referer)
        {
            if (this.ModelState.IsValid)
            {
                var tweeterDto = await this.tweeterApiService.GetTweeterByScreenNameAsync(screenName);

                if (tweeterDto == null)
                {
                    return this.Json("Invalid tweeter.");
                }

                var userId = this.userManager.GetUserId(HttpContext.User);

                try
                {
                    await this.tweeterService.AddFavoriteTweeterForUserAsync(userId, tweeterDto);
                }
                catch (Exception e)
                {
                    var error = e.Message;

                    return this.Json("Unable to add tweeter to favourite.");
                }
                //return RedirectToLocal(returnUrl, "Successfully added to favorite");
                return this.Json("success");
            }

            return this.Json("Tweeter shoud have screen name.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveTweeterFromFavourite(TweeterViewModel tweeterViewModel, [FromHeader] string referer)
        {
            var returnUrl = referer;
            if (this.ModelState.IsValid)
            {
                var tweeterDto = await this.tweeterApiService.GetTweeterByScreenNameAsync(tweeterViewModel.ScreenName);

                if (tweeterDto == null)
                {
                    return this.Json("Invalid tweeter.");
                }

                var userId = this.userManager.GetUserId(HttpContext.User);

                try
                {
                    await this.tweeterService.RemoveSavedTweeterForUserAsync(userId, tweeterDto.TweeterId);
                }
                catch (Exception)
                {
                    return this.Json("Unable to remove tweeter from favourite.");
                }

                return RedirectToLocal(returnUrl, "Successfully removed");
                //return this.Json("success");
            }

            return this.Json("Tweeter shoud have screen name.");


        }

        #region Helpers

        private bool CurrentUserIsAdmin()
        {
            return HttpContext.User.IsInRole("Admin");
        }

        private IActionResult RedirectToLocal(string returnUrl, string resultMessage)
        {
            TempData["Result"] = resultMessage;
            return Redirect(returnUrl);
        }

        #endregion

    }
}
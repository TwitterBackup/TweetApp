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
        private readonly IUserService userDbService;
        private readonly ITweeterService tweeterService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMappingProvider mappingProvider;

        public TweetersController(UserManager<ApplicationUser> userManager, IMappingProvider mappingProvider,
            ITweeterService tweeterService, IUserService userDbService, IUserService userService,
            ITweeterApiService tweeterApiService)
        {
            this.tweeterApiService = tweeterApiService ?? throw new ArgumentNullException(nameof(tweeterApiService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.userDbService = userDbService ?? throw new ArgumentNullException(nameof(userDbService));
            this.tweeterService = tweeterService ?? throw new ArgumentNullException(nameof(tweeterService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
        }

        private bool CurrentUserIsAdmin()
        {
            return HttpContext.User.IsInRole("Admin");
        }

        // GET: Tweeter
        public async Task<IActionResult> Index()
        {
            IEnumerable<TweeterDto> tweetersDto;

            if (CurrentUserIsAdmin())
            {
                tweetersDto = tweeterService.GetAllSavedTweetersForAdmin();
                ViewData["IsAdmin"] = true;
            }
            else
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                tweetersDto = tweeterService.GetUserFavouriteTweeters(currentUser.Id);
                ViewData["IsAdmin"] = false;
            }

            var tweeterViewModels = mappingProvider.ProjectTo<TweeterDto, TweeterViewModel>(tweetersDto);

            if (!tweeterViewModels.Any())
            {
                TempData["Result"] = "No saved Tweeters at this moment!";
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
                    userId = this.CurrentUserIsAdmin() ? userDbService.FindUserIdByUserName(name) : currentUser.Id;
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

                    var tweetDto = mappingProvider.MapTo<EditTweeterDto>(tweeterForEdit);
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

            TempData["Result"] = "Something went wrong. No tweet was edited";
            return RedirectToAction(nameof(Index));
        }

        // GET: Tweeter/Delete/5
        public async Task<IActionResult> Delete(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Result"] = "UserName cannot be null or whitespace.";
                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["Result"] = "No tweet selected!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["IsAdmin"] = CurrentUserIsAdmin();

            var currentUser = await userManager.GetUserAsync(HttpContext.User);


            if (ModelState.IsValid)
            {
                var userId = string.Empty;

                try
                {
                    userId = CurrentUserIsAdmin() ? userService.FindUserIdByUserName(name) : currentUser.Id;
                }

                catch (ArgumentException)
                {
                    TempData["Result"] = "You haven't saved such Tweeter. No tweeter deleted.";
                    return RedirectToAction(nameof(Index));
                }

                var tweeterDto = tweeterService.GetTweeterForUser(userId, id);
                if (tweeterDto == null)
                {
                    TempData["Result"] = "Such tweeter is not found. Please check and try again!";
                    return RedirectToAction(nameof(Index));
                }

                var tweeter = mappingProvider.MapTo<TweeterViewModel>(tweeterDto);
                tweeter.UserName = name;

                return PartialView(tweeter);
            }
            else
            {
                TempData["Result"] = "Something went wrong. No tweeter was deleted";
                return RedirectToAction(nameof(Index));
            }

        }

        // POST: Tweeter/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id, string UserName)
        {
            if (UserName == null)
            {
                TempData["Result"] = "User cannot be null!";
                return RedirectToAction(nameof(Index));
            }

            if (id == null)
            {
                TempData["Result"] = "Such tweeter does not exist. ";
                return RedirectToAction(nameof(Index));
            }

            if (CurrentUserIsAuthorizedAsync(id) == false)
            {
                TempData["Result"] = "You do not have right to edit this tweeter!";
                return RedirectToAction(nameof(Index));
            }

            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = CurrentUserIsAdmin() ? userService.FindUserIdByUserName(UserName) : currentUser.Id;
                    await tweeterService.RemoveSavedTweeterForUserAsync(userId, id);
                    TempData["Result"] = "Tweet was successfully deleted";
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException)
                {
                    TempData["Result"] = "You haven't saved such Tweet. No tweet deleted.";
                    return RedirectToAction(nameof(Index));
                }
            }

            TempData["Result"] = "Something went wrong. No tweet was deleted";
            return RedirectToAction(nameof(Index));

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





        //---------------------





        [HttpGet]
        public async Task<IActionResult> SearchResults(string searchString)
        {
            var searchResult = await this.tweeterApiService.SearchTweetersAsync(searchString);

            var userId = this.userManager.GetUserId(HttpContext.User);

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
                if(searchResultSet.Any(x => x.TweeterId == tweeterViewModel.TweeterId))
                    searchResultSet
                        .SingleOrDefault(x => x.TweeterId == tweeterViewModel.TweeterId)
                        .IsLikedFromUser = true;
                else
                {
                    tweeterViewModel.IsLikedFromUser = true;
                    searchResultSet.Add(tweeterViewModel);
                }
            }
            return this.View(searchResultSet);

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

                var userId = this.userManager.GetUserId(HttpContext.User);

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
    }
}
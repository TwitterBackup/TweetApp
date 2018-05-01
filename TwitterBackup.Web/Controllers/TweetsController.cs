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
            return HttpContext.User.IsInRole("Admin");
        }


        //Add: Tweets
        public async Task<IActionResult> Add(string tweetId)
        {
            if (ModelState.IsValid)
            {
                var apiTweetDto = await tweetApiService.GetTweetByIdAsync(tweetId);
                var userId = userManager.GetUserId(HttpContext.User);

                await tweetService.AddTweetForUserAsync(userId, apiTweetDto);

                TempData["Result"] = "Tweet saved successfully";

                return RedirectToAction(nameof(Index));
            }

            TempData["Result"] = "Something went wrong. Please try again!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Tweets
        public async Task<IActionResult> Index()
        {
            IEnumerable<TweetDto> tweetsDto;

            if (CurrentUserIsAdmin())
            {
                tweetsDto = tweetService.GetAllTweetsForAdmin();
                ViewData["IsAdmin"] = true;
            }
            else
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                tweetsDto = tweetService.GetAllTweetsForUser(currentUser.Id);
                ViewData["IsAdmin"] = false;
            }

            var tweetViewModels = mappingProvider.ProjectTo<TweetDto, TweetViewModel>(tweetsDto);

            if (!tweetViewModels.Any())
            {
                TempData["Result"] = "No saved Tweets at this moment!";
            }

            return View(tweetViewModels);
        }

        // GET: Tweets/Edit/5
        public async Task<IActionResult> Edit(string id, string name)
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
                string userId;

                try
                {
                    userId = this.CurrentUserIsAdmin() ? userService.FindUserIdByUserName(name) : currentUser.Id;
                }

                catch (ArgumentException)
                {
                    TempData["Result"] = "You haven't saved such Tweet. No tweet deleted.";
                    return RedirectToAction(nameof(Index));
                }

                var tweetDto = tweetService.GetTweetForUser(userId, id);
                if (tweetDto == null)
                {
                    TempData["Result"] = "Such tweet is not found. Please check and try again!";
                    return RedirectToAction(nameof(Index));
                }

                var tweet = mappingProvider.MapTo<EditTweetViewModel>(tweetDto);

                return PartialView(tweet);
            }
            else
            {
                TempData["Result"] = "Something went wrong. No tweet was deleted";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Tweets/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(EditTweetViewModel tweetForEdit)
        {
            if (tweetForEdit.TweetId == null)
            {
                ViewData["Result"] = "Such tweet is not found. Please check and try again!";
                return RedirectToAction(nameof(Index));
            }

            if (CurrentUserIsAuthorizedAsync(tweetForEdit.TweetId) == false)
            {
                ViewData["Result"] = "You do not have right to edit this tweet!";
                return RedirectToAction(nameof(Index));
            }

            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = CurrentUserIsAdmin() ? userService.FindUserIdByUserName(tweetForEdit.UserName) : currentUser.Id;

                    var tweetDto = mappingProvider.MapTo<EditTweeterDto>(tweetForEdit);
                    await tweetService.AddNoteToSavedTweetForUserAsync(userId, tweetForEdit.TweetId, tweetForEdit.TweetComments);
                    TempData["Result"] = "Tweet was successfully edited";
                    return Json("success");
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

        // GET: Tweets/Delete/5
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
                    TempData["Result"] = "You haven't saved such Tweet. No tweet deleted.";
                    return RedirectToAction(nameof(Index));
                }

                var tweetDto = tweetService.GetTweetForUser(userId, id);
                if (tweetDto == null)
                {
                    TempData["Result"] = "Such tweet is not found. Please check and try again!";
                    return RedirectToAction(nameof(Index));
                }

                var tweet = mappingProvider.MapTo<TweetViewModel>(tweetDto);
                tweet.UserName = name;

                return PartialView(tweet);
            }
            else
            {
                TempData["Result"] = "Something went wrong. No tweet was deleted";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Tweets/Delete/5
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
                TempData["Result"] = "Such tweet does not exist. ";
                return RedirectToAction(nameof(Index));
            }

            if (CurrentUserIsAuthorizedAsync(id) == false)
            {
                TempData["Result"] = "You do not have right to edit this tweet!";
                return RedirectToAction(nameof(Index));
            }

            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = CurrentUserIsAdmin() ? userService.FindUserIdByUserName(UserName) : currentUser.Id;
                    await tweetService.RemoveSavedTweetForUserAsync(userId, id);
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
                var resource = tweetService.GetTweetForUser(currentUserId, resourceId);

                return resource != null;
            }
        }
    }
}

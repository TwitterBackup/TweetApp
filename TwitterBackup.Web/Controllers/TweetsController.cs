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
using TwitterBackup.Web.Models.TweetViewModels;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class TweetsController : Controller
    {
        private readonly IUserDbService userDbService;
        private readonly IMappingProvider mappingProvider;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITweetDbService tweetService;

        public TweetsController(ITweetDbService tweetService, IMappingProvider mappingProvider, UserManager<ApplicationUser> userManager, IUserDbService userDbService)
        {
            this.userDbService = userDbService ?? throw new ArgumentNullException(nameof(userDbService));
            this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
            this.mappingProvider = mappingProvider ?? throw new ArgumentNullException(nameof(mappingProvider));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // GET: Tweets
        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            IList<TweetViewModel> tweetViewModelList = null;
            IEnumerable<TweetDto> tweetsDto = null;
            bool currentUserIsAdmin = false;

            if (CurrentUserIsAdmin())
            {
                tweetsDto = await tweetService.GetAllTweetsAsync();
                currentUserIsAdmin = true;
            }
            else
            {
                tweetsDto = await tweetService.GetTweetsByUserIdAsync(currentUser.Id);
            }

            tweetViewModelList = mappingProvider.ProjectTo<TweetDto, TweetViewModel>(tweetsDto).ToList();

            if (tweetViewModelList.Count == 0)
            {
                TempData["Result"] = "No saved Tweets at this moment!";
            }

            ViewData["IsAdmin"] = currentUserIsAdmin;
            return View(tweetViewModelList);
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
                var userId = string.Empty;

                try
                {
                    userId = CurrentUserIsAdmin() ? userDbService.FindUserIdByUserName(name) : currentUser.Id;
                }

                catch (ArgumentException)
                {
                    TempData["Result"] = "You haven't saved such Tweet. No tweet deleted.";
                    return RedirectToAction(nameof(Index));
                }

                var tweetDto = tweetService.TweetById(id, userId);
                if (tweetDto == null)
                {
                    TempData["Result"] = "Such tweet is not found. Please check and try again!";
                    return RedirectToAction(nameof(Index));
                }

                var tweet = mappingProvider.MapTo<EditTweetViewModel>(tweetDto);
                if (tweet.TweetComments == null)
                    tweet.TweetComments = "";
                tweet.UserName = name;

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
                    var userId = CurrentUserIsAdmin() ? userDbService.FindUserIdByUserName(tweetForEdit.UserName) : currentUser.Id;

                    var tweetDto = mappingProvider.MapTo<EditTweetDto>(tweetForEdit);
                    tweetService.UpdateTweet(tweetDto, userId);
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
                    userId = CurrentUserIsAdmin() ? userDbService.FindUserIdByUserName(name) : currentUser.Id;
                }

                catch (ArgumentException)
                {
                    TempData["Result"] = "You haven't saved such Tweet. No tweet deleted.";
                    return RedirectToAction(nameof(Index));
                }

                var tweetDto = tweetService.TweetById(id, userId);
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
                    var userId = CurrentUserIsAdmin() ? userDbService.FindUserIdByUserName(UserName) : currentUser.Id;
                    tweetService.SoftDeleteTweetByIdPerUserId(id, userId);
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
            var currentUser = HttpContext.User;
            if (currentUser.IsInRole("Admin"))
                return true;
            else
            {
                var currentUserId = userManager.GetUserId(HttpContext.User);
                var resource = tweetService.TweetById(resourceId, currentUserId);

                return resource != null;
            }
        }

        private bool CurrentUserIsAdmin()
        {
            return HttpContext.User.IsInRole("Admin");
        }
    }
}

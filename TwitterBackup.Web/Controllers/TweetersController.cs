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
using TwitterBackup.Web.Models.TweeterDbViewModel;
using TwitterBackup.Web.Models.TweetViewModels;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class TweetersController : Controller
    {
        private readonly IUserService userDbService;
        private readonly ITweeterService tweeterDbService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMappingProvider mappingProvider;

        public TweetersController(UserManager<ApplicationUser> userManager, IMappingProvider mappingProvider, ITweeterService tweeterDbService, IUserService userDbService)
        {
            this.userDbService = userDbService ?? throw new ArgumentNullException(nameof(userDbService));
            this.tweeterDbService = tweeterDbService ?? throw new ArgumentNullException(nameof(tweeterDbService));
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
                tweetersDto = tweeterDbService.GetAllSavedTweetersForAdmin();
                ViewData["IsAdmin"] = true;
            }
            else
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                tweetersDto = await tweeterDbService.GetUserFavouriteTweetersAsync(currentUser.Id);
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

                var tweeterDto = tweeterDbService.GetFavoriteTweeterForUser(userId, id);
                if (tweeterDto == null)
                {
                    TempData["Result"] = "Such tweet is not found. Please check and try again!";
                    return RedirectToAction(nameof(Index));
                }

                var editTweeterViewModel = mappingProvider.MapTo<EditTweeterViewModel>(tweeterDto);
                if (editTweeterViewModel.TweetComments == null)
                    editTweeterViewModel.TweetComments = "";
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
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tweeter/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tweeter/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
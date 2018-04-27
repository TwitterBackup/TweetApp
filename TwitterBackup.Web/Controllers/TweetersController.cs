using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;
using TwitterBackup.Web.Models.TweeterDbViewModel;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class TweetersController : Controller
    {
        private readonly ITweetDbService tweetDbService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMappingProvider mapper;
        private readonly ITweeterDbService tweeterDbService;

        public TweetersController(ITweetDbService tweetDbService, UserManager<ApplicationUser> userManager, IMappingProvider mapper, ITweeterDbService tweeterDbService)
        {
            this.tweetDbService = tweetDbService ?? throw new ArgumentNullException(nameof(tweetDbService));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.tweeterDbService = tweeterDbService ?? throw new ArgumentNullException(nameof(tweeterDbService));
        }


        // GET: Tweeter
        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            var resultDto = await tweeterDbService.GetUserFavouriteTweetersAsync(currentUser.Id);

            if (!resultDto.Any())
            {
                ViewData["Result"] = "You do not have favourite tweeters at the moment!";
                return View();
            }
            else
            {
                ViewData["Result"] = "Your favourite tweeters follow below:";
                var result = mapper.ProjectTo<TweeterDto, DetailsViewModel>(resultDto);
                return View(result);
            }
        }

        // GET: Tweeter/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tweeterDto = tweeterDbService.GetById(id);
            if (tweeterDto == null)
            {
                return NotFound();
            }
            var result = mapper.MapTo<DetailsViewModel>(tweeterDto);

            return View(result);
        }

        // GET: Tweeter/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
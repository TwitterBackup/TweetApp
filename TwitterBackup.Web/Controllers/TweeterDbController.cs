using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;

namespace TwitterBackup.Web.Controllers
{
    public class TweeterDbController : Controller
    {
        private readonly ITweetDbService tweetDbService;
        private readonly UserManager<ApplicationUser> user;
        private readonly IMappingProvider mapper;

        public TweeterDbController(ITweetDbService tweetDbService, UserManager<ApplicationUser> user, IMappingProvider mapper)
        {
            this.tweetDbService = tweetDbService;
            this.user = user;
            this.mapper = mapper;
        }


        // GET: TweeterDb
        public ActionResult Index()
        {
            return View();
        }

        // GET: TweeterDb/Details/5
        public ActionResult Details(string id)
        {
            //find id of given tweeter

            var tweetsDto = tweetDbService.GetTweetsByTweeterIdAsync(id);
            //var tweetsVM = mapper.ProjectTo<TweeterDbViewModel>(tweetsDto);


            return View();
        }

        // GET: TweeterDb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TweeterDb/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TweeterDb/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TweeterDb/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: TweeterDb/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TweeterDb/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
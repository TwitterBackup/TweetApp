using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Models;
using TwitterBackup.Services.Data.Contracts;

namespace TwitterBackup.Web.Controllers
{
    public class TweetController : Controller
    {
        private readonly ITweetDbService tweetDbService;
        private readonly UserManager<ApplicationUser> user;
        private readonly IMappingProvider mapper;

        public TweetController(ITweetDbService tweetDbService, UserManager<ApplicationUser> user, IMappingProvider mapper)
        {
            this.tweetDbService = tweetDbService;
            this.user = user;
            this.mapper = mapper;
        }


        // GET: TweetDb
        public IActionResult Index()
        {
            
            return View();
        }

        // GET: TweetDb/Details/5
        public ActionResult Details(string id)
        {

            return View();
        }

        // GET: TweetDb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TweetDb/Create
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

        // GET: TweetDb/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TweetDb/Edit/5
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

        // GET: TweetDb/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TweetDb/Delete/5
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
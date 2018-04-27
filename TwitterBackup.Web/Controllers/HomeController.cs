using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using TwitterBackup.Web.Models;

namespace TwitterBackup.Web.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; set; }

        public HomeController(IConfiguration config)
        {
            Configuration = config;
        }

        public IActionResult Index()
        {
            //ViewData["ConsumerKey"] = Configuration["Authentication:Twitter:ConsumerKey"];
            //ViewData["ConsumerSecret"] = Configuration["Authentication:Twitter:ConsumerSecret"];
            
            return this.View();
        }

        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

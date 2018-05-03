using Microsoft.AspNetCore.Mvc;

namespace TwitterBackup.Web.Areas.Admin.Controllers
{
    public class StatisticsController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
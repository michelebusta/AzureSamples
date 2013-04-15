using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MovieDb.Shared.Smtp;
using QueueComparison.Web.Models;

namespace QueueComparison.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        [HttpPost]
        public ActionResult Index(EmailModel model)
        {
            if (!ModelState.IsValid)
            {
                // todo: validation...
            }

            // send to queue(s)
            Task.Factory.StartNew(() => EmailUtility.SendSearchNotificationToQueue(model.EmailAddress));

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Done()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }
    }
}

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContextAwareAndRealTimeTimeTable.Web.Models;
using HL= ContextAwareAndRealTimeTimeTable.Helpers;
using System.Configuration;


namespace ContextAwareAndRealTimeTimeTable.Web.Controllers
{

    public class HomeController : Controller
    {
        private static string senderId = ConfigurationManager.AppSettings["SenderId"];
        private static string apiKey = ConfigurationManager.AppSettings["ApiKey"];
        private string baseUrl = "http://api.kayesms.com/api/v1/sms/send/?apiKey=" + apiKey;

        public HomeController()
        {
            ViewData["RootNav"] = "/";
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tomorrow()
        {
            return View();
        }

        public ActionResult Today()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
           
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Contact(MobileTest model)
        {
            if (ModelState.IsValid)
            {
                HL.SmsHelper.SendSmsToOneContact(baseUrl, model.Mobile, model.Message, senderId);
            }
            return View("MessageSent");
        }

        public ActionResult MessageSent()
        {
           
            return View();
        }
    }
}
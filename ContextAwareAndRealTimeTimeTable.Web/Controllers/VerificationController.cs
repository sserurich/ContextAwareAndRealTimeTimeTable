using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContextAwareAndRealTimeTimeTable.Web.Models;
using ContextAwareAndRealTimeTimeTable.EF.Models;
using Microsoft.AspNet.Identity;

namespace ContextAwareAndRealTimeTimeTable.Web.Controllers
{
    public class VerificationController : Controller
    {
        ContextAwareAndRealTimeTimeTableEntities context = new ContextAwareAndRealTimeTimeTableEntities();
        // GET: Verification
        public ActionResult Index()
        {
            return View();
        }

        //GET
        public ActionResult Verify(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Verify(VerificationViewModel verificationModel){
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                Verification v = new Verification(){
                    RequestedBy = userId,
                    Message = verificationModel.Message,
                    CreatedOn = DateTime.Now
                };
                context.Verifications.Add(v);
                context.SaveChanges();
            }
            return View("Success");
        }
    }
}
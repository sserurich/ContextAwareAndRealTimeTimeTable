using ContextAwareAndRealTimeTimeTable.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using ContextAwareAndRealTimeTimeTable.EF.Models;
using HL = ContextAwareAndRealTimeTimeTable.Helpers;

namespace ContextAwareAndRealTimeTimeTable.Web.Controllers
{
    //[Authorize(Roles="Admin,Lecturer")]
    public class UsersAdminController : Controller
    {
        private static string senderId = ConfigurationManager.AppSettings["SenderId"];
        private static string apiKey = ConfigurationManager.AppSettings["ApiKey"];
        private string baseUrl = "http://api.kayesms.com/api/v1/sms/send/?apiKey=" + apiKey;
        private string verificationMessage = "You are verified as a lecturer";
        public UsersAdminController()
        {
            ViewData["RootNav"] = "/";
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Users/
        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        //Get:/Users/Verification
        public async Task<ActionResult> UsersToVerifyAsLecturers()
        {
            var dbContext = new ContextAwareAndRealTimeTimeTableEntities();

            var lecturersToBeVerified = dbContext.Verifications.Where(a=>a.UpdatedOn== null);
            List<ApplicationUser> list = new List<ApplicationUser>();
            foreach(var x in lecturersToBeVerified){
                var found = await UserManager.FindByIdAsync(x.RequestedBy);
              //var found = UserManager.Users.Where(a=> a.Id==x.RequestedBy).FirstOrDefault();
                if(found != null){
                    list.Add(found);
                }  
            }
            
            return View(list);
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = userViewModel.Email, FirstName = userViewModel.FirstName, LastName = userViewModel.LastName };
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);
            
            var userToEdit = new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                
                //RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                //{
                //    Selected = userRoles.Contains(x.Name),
                //    Text = x.Name,
                //    Value = x.Name
                //})
            };

            if (RoleManager.Roles.ToList().Count > 0)
            {
                userToEdit.RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                });
            }

            return View(userToEdit);
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id,LastName,FirstName")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                bool isLecturer = false;
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                             
                user.Email = editUser.Email;
                user.FirstName = editUser.FirstName;
                user.LastName = editUser.LastName;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());
                if (selectedRole.Count() > 0)
                {
                    foreach (string x in selectedRole)
                    {
                        if (x.Contains("Lecturer"))
                        {
                            isLecturer = true;
                        }
                    }
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                if (isLecturer == true)
                {
                    var dbContext = new ContextAwareAndRealTimeTimeTableEntities();

                    var lecturersToBeVerified = dbContext.Verifications.Where(a => a.UpdatedOn == null && a.RequestedBy== user.Id).FirstOrDefault();
                    if (lecturersToBeVerified != null)
                    {
                        lecturersToBeVerified.UpdatedOn = DateTime.Now;
                        dbContext.SaveChanges();

                         HL.SmsHelper.SendSmsToOneContact(baseUrl,user.Mobile, verificationMessage, senderId);
                       
                    }
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    
    }
}
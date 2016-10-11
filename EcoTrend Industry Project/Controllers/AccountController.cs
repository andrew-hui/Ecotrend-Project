using EcoTrend_Industry_Project.BusinessLogic;
using EcoTrend_Industry_Project.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EcoTrend_Industry_Project.Controllers
{
    public class AccountController : BaseController
    {
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisteredUser newUser, string role, string branch, string repName, string email, string supplierName)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            manager.UserLockoutEnabledByDefault = true;
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var identityUser = new IdentityUser()
            {
                UserName = newUser.UserName,
                Email = newUser.Email
            };
            IdentityResult result = manager.Create(identityUser, newUser.Password);

            /** add user to role **/
            EcotrendEntities context = new EcotrendEntities();
            AspNetUser user = context.AspNetUsers
                             .Where(u => u.UserName == newUser.UserName).FirstOrDefault();
            AspNetRole userrole = context.AspNetRoles
                             .Where(r => r.Name == role).FirstOrDefault();

            user.AspNetRoles.Add(userrole);
            /** **/
            /** sales rep **/
            if (role == "salesrep")
            {
                SalesRep newRep = new SalesRep();
                newRep.salesRepID = Convert.ToInt32(newUser.UserName);
                newRep.branch = branch;
                newRep.name = repName;
                newRep.email = email;
                context.SalesReps.Add(newRep);
            }
            if (role == "supplier")
            {
                Supplier newSupplier = new Supplier();
                newSupplier.email = email;
                newSupplier.name = supplierName;
                context.Suppliers.Add(newSupplier);
                context.SaveChanges();
            }
            /** **/
            context.SaveChanges();
            if (result.Succeeded)
            {
                var authenticationManager
                                  = HttpContext.Request.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(identityUser,
                                           DefaultAuthenticationTypes.ApplicationCookie);
                ViewBag.Success = "Account successfully created";
                return View();
            }
            ViewBag.Error = "Uh oh... Unable to create an account.";
            return View();
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
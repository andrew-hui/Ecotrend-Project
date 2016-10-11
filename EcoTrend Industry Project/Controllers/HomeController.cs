using EcoTrend_Industry_Project.BusinessLogic;
using EcoTrend_Industry_Project.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EcoTrend_Industry_Project.Repositories;
using PagedList;

namespace EcoTrend_Industry_Project.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login login)
        {
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);
            try
            {
                IdentityUser identityUser = manager.Find(login.UserName,
                                                             login.Password);
                if (ModelState.IsValid)
                {
                    if (identityUser != null)
                    {
                        IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                        authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                        var identity = new ClaimsIdentity(new[] {
                                            new Claim(ClaimTypes.Name, login.UserName),
                                            },
                                            DefaultAuthenticationTypes.ApplicationCookie,
                                            ClaimTypes.Name, ClaimTypes.Role);
                        // SignIn() accepts ClaimsIdentity and issues logged in cookie. 

                        var userSearched = manager.FindByName(login.UserName);

                        if (manager.IsLockedOut(userSearched.Id))
                        {
                            return View();
                        }
                        else
                        {

                            authenticationManager.SignIn(new AuthenticationProperties
                            {
                                IsPersistent = false
                            }, identity);

                            EcotrendEntities context = new EcotrendEntities();
                            var user = context.AspNetUsers.Where(u => u.UserName == login.UserName).FirstOrDefault();
                            var role = manager.GetRoles(user.Id);

                            if (role[0] == "salesrep")
                            {
                                return RedirectToAction("ContactManagement", "Home");
                            }
                            else if (role[0] == "admin")
                            {
                                return RedirectToAction("ListSalesReps", "Admin");
                            }
                            else if (role[0] == "supplier")
                            {
                                return RedirectToAction("Index", "Supplier");
                            }

                        }

                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin, salesrep")]
        public ActionResult ContactManagement(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int salesRepID = 0;
            if (User.Identity.Name != "admin")
            {
                salesRepID = int.Parse(User.Identity.Name);
            }
            ContactRepo contactRepo = new ContactRepo();
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            IEnumerable<ContactVM> contacts = contactRepo.GetContactsBySalesRepID(searchString, sortOrder, salesRepID);

            // Store current search and sort filter parameters.
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentSort = sortOrder;

            // Provide toggle option for name sort.
            if (String.IsNullOrEmpty(sortOrder))
                ViewBag.NameSortParm = ContactRepo.LASTNAME;
            else
                ViewBag.NameSortParm = "";

            //// Provide toggle  optionfor date sort.
            //if (sortOrder == StudentRepository.DATE)
            //    ViewBag.DateSortParm = StudentRepository.DATE_DESC;
            //else
            //    ViewBag.DateSortParm = StudentRepository.DATE;
            const int PAGE_SIZE = 3;
            int pageNumber = (page ?? 1);

            // Truncate results to fit in the view provided.
            contacts = contacts.ToPagedList(pageNumber, PAGE_SIZE);

            return View(contacts);


        }

        [HttpGet]
        public ActionResult SendEmail()
        {

            // EmailMessageVM e                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            M = new EmailMessageVM();
            //   return View(emailMessageVM);
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string sender, string subject, string body)
        {
            ContactRepo contactRepo = new ContactRepo();
            /* List<SampleContact> contactList = contactRepo.GetAll().ToList();

             string ErrorMessage = "";
             foreach (var item in contactList)
             {
                 MailHelper mailer = new MailHelper();
                 string response = mailer.EmailFromArvixe(
                                            new Message(sender, subject, body, item.email));
                 if (!response.Contains("success"))
                 {
                     ErrorMessage = ErrorMessage + item.email + " Failed \n";
                     return RedirectToAction("emailMessageList", "Home", new { ErrorMessage = ErrorMessage });
                 }
             }*/
            return View();
        }

        public ActionResult emailMessageList(string ErrorMessage)
        {
            ViewBag.ErrorMessage = ErrorMessage;
            return View();
        }


        public ActionResult Result(bool success, string result)
        {
            if (success == true)
            {
                ViewBag.TabTitle = "Success";
                ViewBag.Result = result;
            }
            else
            {
                ViewBag.TabTitle = "Error";
                ViewBag.Result = result;
            }
            return View();
        }

        public ActionResult Delete(int id)
        {

            ContactRepo contactRepo = new ContactRepo();
            if (contactRepo.Delete(id) == true)
            {
                return RedirectToAction("ContactManagement");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Test(string str)
        {
            try
            {
                EmailRepo emailRepo = new EmailRepo();
                List<string> emails = new List<string>();
                emails.Add("andrew.hui09@gmail.com");
                emails.Add("hui_andrew@hotmail.com");
                emailRepo.MassSend(emails, "sender", "somesubjcct", "abcBody");

                ViewBag.Success = "Emails sent.";
                return RedirectToAction("Result", "Home", new { success = true, result = ViewBag.Success });
            }
            catch
            {
                ViewBag.Error = "Emails were not sent.";
                return RedirectToAction("Result", "Home", new { success = false, result = ViewBag.Error });
            }
        }
        public void emailArray()
        {
            string[] emailaddresses = new string[] { "andrew.hui09@gmail.com", "hui_andrew@hotmail.com" };
            for (int i = 0; i < emailaddresses.Count(); i++)
            {
                MailHelper mailer = new MailHelper();
                string response = mailer.EmailFromArvixe(new Message("andrew.hui09@gmail.com", "someSubject", "someBody", emailaddresses[i]));
            }
        }



    }
}
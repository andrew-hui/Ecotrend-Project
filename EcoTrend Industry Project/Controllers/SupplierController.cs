using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EcoTrend_Industry_Project.Controllers
{
    [Authorize(Roles = "supplier")]
    public class SupplierController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string subject, string body)
        {
            EcotrendEntities context = new EcotrendEntities();
            var memberEmail = (from u in context.AspNetUsers
                              where u.UserName == User.Identity.Name
                              select new 
                              {
                                  email = u.Email
                              }).FirstOrDefault();
            var supplierId = (from s in context.Suppliers
                             where s.email == memberEmail.email
                             select new
                             {
                                 supplierID = s.supplierID
                             }).FirstOrDefault();
            Email email = new Email();
            email.subject = subject;
            email.body = body;
            email.supplierID = supplierId.supplierID;
            email.approved = false;
            email.dateCreated = DateTime.Now;

            try
            {
                context.Emails.Add(email);
                context.SaveChanges();
                ViewBag.Success = "Email successfully saved.";
            }
            catch
            {
                ViewBag.Error = "Cannot save email.";
            }
            return View();
        }
    }
}
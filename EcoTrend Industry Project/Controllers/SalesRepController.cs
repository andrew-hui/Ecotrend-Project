using EcoTrend_Industry_Project.Models;
using EcoTrend_Industry_Project.Repositories;
using EcoTrend_Industry_Project.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoTrend_Industry_Project.Controllers
{
    [Authorize(Roles = "admin, salesrep")]
    public class SalesRepController : BaseController
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddContact()
        {
            StoreRepo storeRepo = new StoreRepo();
            return View(storeRepo.GetStoresUnderContactVM());
        }
        [HttpPost]
        public ActionResult AddContact(ContactVM contactVM)
        {
            int salesRepID = Convert.ToInt32(User.Identity.Name);
            contactVM.SalesRepID = salesRepID;
            StoreRepo storeRepo = new StoreRepo();
            contactVM.StoreName = storeRepo.GetStoreNameByID(Convert.ToInt32(contactVM.StoreID));
            ContactRepo contactRepo = new ContactRepo();

            try
            {
                contactRepo.AddContact(contactVM);
                ViewBag.Success = "Contact successfully added.";
                return RedirectToAction("Result", "Home", new { success = true, result = ViewBag.Success });
            }
            catch
            {
                ViewBag.Error = "Failed to add contact.";
                return RedirectToAction("Result", "Home", new { success = false, result = ViewBag.Error });
            }



        }

        [HttpGet]
        public ActionResult EditContact(string id)
        {
            StoreRepo storeRepo = new StoreRepo();
            ContactRepo contactRepo = new ContactRepo();
            ContactVM contactVm = contactRepo.GetContactDetail(Convert.ToInt32(id));
            //queries stores and gets them from the database, and puts them in the edit viewmodel.
            contactVm.Stores = storeRepo.GetStoresUnderContactVM().Stores;
            return View(contactVm);
        }
        [HttpPost]
        public ActionResult EditContact(ContactVM contactVM)
        {
            contactVM.SalesRepID = Convert.ToInt32(User.Identity.Name);
            /*  if (department == "success")
              {
                  ViewBag.Success = "Contact successfully updated.";
                  return RedirectToAction("Result", "Home", new { success = true, result = ViewBag.Success });
              }
              else
              {
                  ViewBag.Error = "Contact was not updated.";
                  return RedirectToAction("Result", "Home", new { success = false, result = ViewBag.Error });
              } */
            ContactRepo contactRepo = new ContactRepo();
            ContactVM editedContact = contactRepo.LimitedUpdate(contactVM);

            return RedirectToAction("ContactManagement", "Home");

        }

        public ActionResult AddSalesRep()
        {
            return View();
        }
        public ActionResult ListSalesReps()
        {
            return View();
        }
        public ActionResult ContactNotes(int id, int contactId, string firstName, string lastName)
        {
            LogsRepo logsRepo = new LogsRepo();
            var logs = logsRepo.GetAllVM(id, contactId);
            ViewBag.FirstName = firstName;
            ViewBag.LastName = lastName;
            ViewBag.ContactId = contactId;
            return View(logsRepo.GetLogsBySalesIDContactID(id, contactId));
        }

        [HttpGet]
        public ActionResult AddNote(int contactId, string firstName, string lastName)
        {
            EcotrendEntities context = new EcotrendEntities();
            ViewBag.ContactId = contactId;
            ViewBag.FirstName = firstName;
            ViewBag.LastName = lastName;
            return View();
        }
        [HttpPost]
        public ActionResult AddNote(string title, string body, int contactID)
        {
            EcotrendEntities context = new EcotrendEntities();
            LogsRepo logsRepo = new LogsRepo();

            int parsedUserName;
            int.TryParse(User.Identity.Name, out parsedUserName);

            var salesRepName = logsRepo.GetSalesRepName(parsedUserName);

            Log log = new Log();
            log = logsRepo.AddLog(title, body, contactID, salesRepName);
            log.salesRepID = parsedUserName;

            try
            {
                context.Logs.Add(log);
                context.SaveChanges();
                ViewBag.Success = "Note added successfully";

                return RedirectToAction("Result", "Home", new { success = true, result = ViewBag.Success });
            }
            catch
            {
                ViewBag.Error = "Note was not added.";
                return RedirectToAction("Result", "Home", new { success = false, result = ViewBag.Error });
            }
        }
        [HttpGet]
        public ActionResult SendGroupEmail()
        {
            ContactRepo contactRepo = new ContactRepo();
            return View(contactRepo.GetAll());
        }
        [HttpPost]
        public ActionResult SendGroupEmail(List<string> emails, string title, string body)
        {
            EmailRepo emailRepo = new EmailRepo();
            ContactRepo contactRepo = new ContactRepo();
            try
            {
                emailRepo.MassSend(emails, "sender", title, body);
                ViewBag.Success = "Emails sent successfully";
                return View(contactRepo.GetAll());
            }
            catch
            {
                ViewBag.Error = "Uh oh... Something went wrong. Try again";
                return View(contactRepo.GetAll());
            }
        }


        public ActionResult ListContactsByCompany()
        {
            return View();
        }
    }
}
using EcoTrend_Industry_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcoTrend_Industry_Project.Controllers
{
    public class BaseController : Controller
    {
        public BaseViewModel model;

        public BaseController()
        {
            try
            {
                EcotrendEntities context = new EcotrendEntities();
                var query = from e in context.Emails
                            select new BaseViewModel
                            {
                                Approved = e.approved
                            };
                int count = query.Count(pending => pending.Approved == false);
                this.model = new BaseViewModel();
                this.model.Count = count;
                this.ViewBag.Model = this.model;
            }
            catch{
                this.ViewBag.Model = "";
            }
        }
    }
}
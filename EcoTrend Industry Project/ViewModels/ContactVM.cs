using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EcoTrend_Industry_Project.ViewModels
{
    public class ContactVM
    {
        [DisplayName("Customer ID")]
        public int ContactID { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Telephone")]
        public string Telephone { get; set; }
        [DisplayName("Branch")]
        public string Branch { get; set; }
        [DisplayName("Department")]
        public string Department { get; set; }
        [DisplayName("Store ID")]
        public Nullable<int> StoreID { get; set; }
        [DisplayName("Store Name")]
        public string StoreName { get; set; }
        public int SalesRepID { get; set; }
        public IEnumerable<Store> Stores {get; set; }
        public IEnumerable<SalesRep> SalesReps { get; set; }

    }
}
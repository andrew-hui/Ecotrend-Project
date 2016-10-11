using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.ViewModels
{
    public class SupplierDetailsVM
    {
        [DisplayName("Supplier ID")]
        public int SupplierID { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Status")]
        public DateTime? LockedOut { get; set; }
        [DisplayName("User Type")]
        public string UserRole { get; set; }
        public IEnumerable<EmailVM> EmailHistory {get; set; }
    }
}
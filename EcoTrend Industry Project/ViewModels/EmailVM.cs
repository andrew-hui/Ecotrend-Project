using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.ViewModels
{
    public class EmailVM
    {
        [DisplayName("Email ID")]
        public int EmailID { get; set; }
        [DisplayName("Subject")]
        public string Subject { get; set; }
        [DisplayName("Message Body")]
        public string Body { get; set; }
        [DisplayName("Approved")]
        public bool? Approved { get; set; }
        [DisplayName("Date Sent")]
        public DateTime? DateSent { get; set; }
        [DisplayName("Date Created")]
        public DateTime? DateCreated { get; set; }
        public int? SupplierID { get; set; }
    }
}
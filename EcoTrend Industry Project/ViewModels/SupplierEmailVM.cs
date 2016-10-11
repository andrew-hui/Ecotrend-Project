using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.ViewModels
{
    public class SupplierEmailVM
    {
        public int supplierID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public bool? approved { get; set; }
        public int emailID { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}
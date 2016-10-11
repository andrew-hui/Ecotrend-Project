using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.ViewModels
{
    public class LogContactVM
    {
        public int logID { get; set; }
        public Nullable<System.DateTime> logDate { get; set; }
        public Nullable<int> salesRepID { get; set; }
        public string salesRepName { get; set; }
        public Nullable<int> contactID { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string storeName { get; set; }
        public string contactName { get; set; }
    }
}
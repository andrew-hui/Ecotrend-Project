using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoTrend_Industry_Project.ViewModels
{
    public class SupplierAndSalesRepVM
    {
        public IEnumerable<PasswordResetVM> SalesRepsPasswordReset { get; set; }
        public IEnumerable<PasswordResetVM> SuppliersPasswordReset { get; set; }
        public IEnumerable<PasswordResetVM> SalesRepsLockout { get; set; }
        public IEnumerable<PasswordResetVM> SuppliersLockout { get; set; }

    }
}

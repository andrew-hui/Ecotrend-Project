using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.ViewModels
{
    public class SalesRepsLockoutVM
    {
        [DisplayName("Status")]
        public DateTime? Date { get; set; }

        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Branch")]
        public string Branch { get; set; }

    }
}
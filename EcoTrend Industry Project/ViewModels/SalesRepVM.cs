using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcoTrend_Industry_Project.ViewModels
{
    public class SalesRepVM
    {
        [Required]
        [DisplayName("ID Number")]
        public int SalesRepID { get; set; }

        [Required]
        [DisplayName("Branch")]
        public string Branch { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
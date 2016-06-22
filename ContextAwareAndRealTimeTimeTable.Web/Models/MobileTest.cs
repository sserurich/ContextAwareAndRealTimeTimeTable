using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ContextAwareAndRealTimeTimeTable.Web.Models
{
    public class MobileTest
    {
        [RegularExpression(@"^(2567|2563)\d{8}$", ErrorMessage = "Mobile number should start with 256")]
        [Required]
        [Display(Name="Mobile")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContextAwareAndRealTimeTimeTable.Web.Models
{
    public class VerificationViewModel
    {

        [Required]
        [Display(Name = "Verification Message")]
        public string Message { get; set; }
    }
}
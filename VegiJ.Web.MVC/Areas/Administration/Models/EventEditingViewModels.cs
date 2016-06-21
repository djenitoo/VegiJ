using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VegiJ.Web.MVC.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EventEntityViewModel
    {
        public Guid ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name="Event Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Place")]
        public string Place { get; set; }
        [Required]
        [Display(Name = "Date and time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Approved")]
        public bool IsApproved { get; set; }
        public AuthorViewModel Author { get; set; }
    }
}
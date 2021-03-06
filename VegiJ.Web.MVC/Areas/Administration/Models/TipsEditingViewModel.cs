﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VegiJ.Web.MVC.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using DataAccess;

    public class TipEntityViewModel
    {
        public Guid ID { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Title { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 20)]
        public string Content { get; set; }
        public bool IsApproved { get; set; }
        public AuthorViewModel Author { get; set; }

        //[Display(Name = "User List")]
        //public List<SelectListItem> UsersListItems { get; set; }
    }

    public class AuthorViewModel
    {
        public string UserName { get; set; }
        public Guid ID { get; set; }
    }
}
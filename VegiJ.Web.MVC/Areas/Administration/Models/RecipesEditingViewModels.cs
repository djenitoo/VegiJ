using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VegiJ.Web.MVC.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RecipeEntityViewModel
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
        public CategoryEntityViewModel Category { get; set; }
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string newCategoryName { get; set; }
        public List<TagEntityViewModel> Tags { get; set; }
    }

    public class CategoryEntityViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
    }

    public class TagEntityViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}
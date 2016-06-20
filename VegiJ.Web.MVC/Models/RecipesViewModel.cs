using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VegiJ.Web.MVC.Models
{
    using DataAccess;

    public class RecipesIndexViewModel
    {
        public List<Recipe> RecipesList { get; set; }
        public List<Category> Categories { get; set; }
    }

    public class RecipesCategoryViewModel
    {
        public Guid ID;
        public string Name;
        public string ParentName { get; set; }
    }

    public class RecipesParialViewModel
    {
        public string CategoryName { get; set; }
        public List<Recipe> CategoryRecipes { get; set; }
    }

    public class RecipeDetailViewModel
    {
        public Recipe Recipe { get; set; }
    }

    public class CategoryViewModel
    {
        public string CategoryName { get; set; }
        public Category ParentCategory { get; set; }
        public List<Recipe> CategoryRecipes { get; set; }
    }

    public class TagsViewModel
    {
        public string TagName { get; set; }
        public List<Recipe> TagRecipes { get; set; }
        public List<Tag> AllTags { get; set; }
    }
}
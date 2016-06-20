using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VegiJ.Web.MVC.Controllers
{
    using System.Collections;
    using DataAccess;
    using DataAccess.Contracts;
    using Models;

    public class RecipesController : Controller
    {
        public IRecipeManager RecipeManager { get; set; }
        public ICategoryManager CategoryManager { get; set; }
        public ITagManager TagManager { get; set; }

        private static List<Category> categories;

        public RecipesController(IRecipeManager recipeManager, ICategoryManager categoryManager, ITagManager tagManeger)
        {
            this.RecipeManager = recipeManager;
            this.CategoryManager = categoryManager;
            this.TagManager = tagManeger;
            categories = CategoryManager.GetAllCategories().ToList();
        }

        // GET: Recipes
        public ActionResult Index()
        {
            var model = new RecipesIndexViewModel();

            model.Categories = CategoryManager.GetAllCategories().ToList();
            model.RecipesList = RecipeManager.GetAllRecipes().ToList();

            return View(model);
        }

        //GET: Recipes/Details/5
        public ActionResult Details(string RecipeTitle)
        {
            var recipe = RecipeManager.GetAllRecipes().AsEnumerable().Where(r => r.Title.Equals(RecipeTitle)).FirstOrDefault();

            if (recipe != null)
            {
                var model = new RecipeDetailViewModel();
                model.Recipe = recipe;
                return View(model);
            }

            throw new HttpException(404, "The page you are looking for do not exist.");
        }

        public ActionResult Categories(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                var model = new CategoryViewModel();
                model.CategoryName = "Main Category";
                model.ParentCategory = null;
                model.CategoryRecipes = RecipeManager.GetAllRecipes().ToList();

                return View(model);
            }

            var cate = CategoryManager.GetAllCategories()
                    .AsEnumerable()
                    .Where(cat => cat.Name.Equals(category))
                    .FirstOrDefault();

            if (cate != null)
            {
                var model = new CategoryViewModel();
                model.CategoryName = cate.Name;
                model.ParentCategory = cate.ParentCategory ?? null;
                model.CategoryRecipes = cate.Recipes.ToList();

                return View(model);
            }

            throw new HttpException(404, "The category you are looking for do not exist.");
        }

        public ActionResult Tags(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                var model = new TagsViewModel();
                model.TagName = "All Tags";
                model.TagRecipes = null;
                model.AllTags = TagManager.GetAllTags().ToList();

                return View(model);
            }
            var tagItem = this.TagManager.GetAllTags().AsEnumerable().Where(t => t.Name.Equals(tag)).FirstOrDefault();

            if (tagItem != null)
            {
                var model = new TagsViewModel();
                model.TagName = tagItem.Name;
                model.TagRecipes = tagItem.Recipes.ToList();

                return View(model);
            }

            throw new HttpException(404, "The category you are looking for do not exist.");
        }

        public PartialViewResult RecipesByCategoryPartial(string ID, string Name)
        {
            var model = new RecipesParialViewModel();
            if (ID == "root")
            {
                model.CategoryName = Name;
                model.CategoryRecipes = RecipeManager.GetAllRecipes().ToList();
            }
            else
            {
                model.CategoryName = Name;
                model.CategoryRecipes =
                    RecipeManager.GetAllRecipes().AsEnumerable().Where(r => r.CategoryID.Equals(Guid.Parse(ID))).ToList();
            }
            return PartialView(model);
        }



        public JsonResult Category_Read()
        {
            var jsonVal = Json(GetCategories(), JsonRequestBehavior.AllowGet);
            return jsonVal;
        }

        private static IEnumerable<RecipesCategoryViewModel> GetCategories()
        {
            var result = categories.Select(cat => new RecipesCategoryViewModel()
            {
                ID = cat.ID,
                Name = cat.Name,
                ParentName = cat.ParentCategory == null ? "" : cat.ParentCategory.Name
            }).ToList();
            return result;
        }
    }
}

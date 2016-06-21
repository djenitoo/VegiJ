using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VegiJ.Web.MVC.Areas.Administration.Controllers
{
    using DataAccess;
    using DataAccess.Contracts;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models;

    public class RecipesEditingController : Controller
    {
        private RecipeServices recipeServices { get; set; }

        public RecipesEditingController(IUserManager uManager,
            ITagManager tagManager,
            ICategoryManager catManager,
            IRecipeManager recipeManager)
        {
            this.recipeServices = new RecipeServices(uManager,tagManager,catManager,recipeManager);
            ViewData["defaultAuthor"] = recipeServices.GetAuthors()[0];
        }
        // GET: Administration/RecipesEditing
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(recipeServices.Read().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, RecipeEntityViewModel product)
        {
            var results = new List<RecipeEntityViewModel>();

            if (product != null && ModelState.IsValid)
            {
                recipeServices.Create(product);
                results.Add(product);
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, RecipeEntityViewModel product)
        {
            if (product != null && ModelState.IsValid)
            {
                recipeServices.Update(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, RecipeEntityViewModel product)
        {
            if (product != null)
            {
                recipeServices.Destroy(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult Authors_Read()
        {
            var jsonVal = Json(recipeServices.GetAuthors(), JsonRequestBehavior.AllowGet);
            return jsonVal;
        }

        public JsonResult Category_Read()
        {
            var jsonVal = Json(recipeServices.GetCategories(), JsonRequestBehavior.AllowGet);
            return jsonVal;
        }
    }
}

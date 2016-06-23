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
    using Newtonsoft.Json;

    [Authorize(Roles = "admin")]
    public class TipsEditingController : Controller
    {
        private TipServices TipService { get; set; }

        public TipsEditingController(IUserManager uManager, ITipManager tipManager)
        {
            
            this.TipService = new TipServices(uManager, tipManager);
            ViewData["defaultAuthor"] = TipService.GetAuthors()[0];
        }

        // GET: Administration/TipsEditing
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(TipService.Read().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, TipEntityViewModel product)
        {
            var results = new List<TipEntityViewModel>();

            if (product != null && ModelState.IsValid)
            {
                TipService.Create(product);
                results.Add(product);
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, TipEntityViewModel product)
        {
            if (product != null && ModelState.IsValid)
            {
                TipService.Update(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, TipEntityViewModel product)
        {
            if (product != null)
            {
                TipService.Destroy(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult Authors_Read()
        {
            var jsonVal = Json(TipService.GetAuthors(), JsonRequestBehavior.AllowGet);
            return jsonVal;
        }
    }
}

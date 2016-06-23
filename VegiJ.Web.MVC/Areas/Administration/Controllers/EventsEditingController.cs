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

    [Authorize(Roles = "admin")]
    public class EventsEditingController : Controller
    {
        private EventServices eventService { get; set; }

        public EventsEditingController(IUserManager uManager, IEventManager evManager)
        {
            this.eventService = new EventServices(uManager, evManager);
            ViewData["defaultAuthor"] = eventService.GetAuthors()[0];
        }
        // GET: Administration/EventsEditing
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(eventService.Read().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, EventEntityViewModel product)
        {
            var results = new List<EventEntityViewModel>();

            if (product != null && ModelState.IsValid)
            {
                eventService.Create(product);
                results.Add(product);
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, EventEntityViewModel product)
        {
            if (product != null && ModelState.IsValid)
            {
                eventService.Update(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, EventEntityViewModel product)
        {
            if (product != null)
            {
                eventService.Destroy(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult Authors_Read()
        {
            var jsonVal = Json(eventService.GetAuthors(), JsonRequestBehavior.AllowGet);
            return jsonVal;
        }
    }
}

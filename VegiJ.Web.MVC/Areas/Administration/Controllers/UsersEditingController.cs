using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VegiJ.Web.MVC.Areas.Administration.Controllers
{
    using DataAccess;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models;

    
    public class UsersEditingController : Controller
    {
        private IUserManager UserManager { get; set; }
        private IRepository<VegiJ.DataAccess.Gender> GenderRepository { get; set; }
        public UserServices userService;
        
        public UsersEditingController(IUserManager uManager, IRepository<VegiJ.DataAccess.Gender> gRepository)
        {
            this.UserManager = uManager;
            this.userService = new UserServices(this.UserManager);
            GenderRepository = gRepository;
        }

        // GET: Administration/UsersEditing
        public ActionResult Index()
        {
            var model = new UserItemViewModel
            {
                ListItems = new SelectList((IEnumerable<Gender>)GenderRepository.Table, "ID", "Name").ToList()
            };
            return View(model);
        }
        
        public JsonResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(userService.Read().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, UserItemViewModel product)
        {
            var results = new List<UserItemViewModel>();

            if (product != null && ModelState.IsValid)
            {
                userService.Create(product);
                results.Add(product);
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, UserItemViewModel product)
        {
            if (product != null && ModelState.IsValid)
            {
                userService.Update(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, UserItemViewModel product)
        {
            if (product != null)
            {
                userService.Destroy(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult Gender_Read()
        {
            var jsonVal = Json(GetGenders(), JsonRequestBehavior.AllowGet);
            return jsonVal;
        }

        private List<SelectListItem> GetGenders()
        {
            var result = new SelectList((IEnumerable<Gender>) GenderRepository.Table, "ID", "Name").ToList();

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VegiJ.Web.MVC.Controllers
{
    using DataAccess;

    public class HomeController : Controller
    {
        //[Inject]
        public IUserManager UserManager { get; set; }

        //[Inject]
        public HomeController(IUserManager uManager)
        {
            this.UserManager = uManager;
        }
        public ActionResult Index()
        {
            var users = UserManager.GetUsers().ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
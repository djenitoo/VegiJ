namespace VegiJ.Web.MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Net;
    using DataAccess;
    using Logic;
    using Models;
    using Gender = DataAccess.Gender;

    [Authorize]
    public class AccountController : Controller
    {
        public IUserManager UserManager { get; set; }
        public IRepository<User> URepository { get; set; }
        public IRepository<VegiJ.DataAccess.Gender> GenderRepository { get; set; }

        public AccountController(IUserManager uManager, IRepository<User> userRepository, IRepository<VegiJ.DataAccess.Gender> gRepository)
        {
            UserManager = uManager;
            URepository = userRepository;
            GenderRepository = gRepository;
        }

        // GET: Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            SecurityManager.LoadUserRepository(URepository);
            var result = SecurityManager.LogIn(model.UserName, model.Password, model.RememberMe);

            if (result)
            {
                return RedirectToLocal(returnUrl);
            }
            // Error occured
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            var model = new RegisterViewModel()
            {
                ListItems = new SelectList((IEnumerable<Gender>)GenderRepository.Table, "ID", "Name").ToList()
            };

            //model.ListItems = new SelectList((IEnumerable<Gender>)ViewData["Genders"], "ID", "Name").ToList();
            //ViewData["Genders"] = GenderRepository.Table;
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            //model.ListItems = new SelectList((IEnumerable<Gender>)GenderRepository.Table, "ID", "Name").ToList();
            if (ModelState.IsValid)
            {
                var user = new User(model.UserName,
                                    model.Password,
                                    model.Email,
                                    model.BirthDate.ToString(),
                                    model.Gender.ID.ToString())
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                try
                {
                    this.UserManager.CreateUser(user);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
                SecurityManager.LoadUserRepository(this.URepository);
                if (SecurityManager.LogIn(model.UserName, model.Password))
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            SecurityManager.LoadUserRepository(this.URepository);
            System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.Abandon();
            System.Web.HttpContext.Current.User = null;
            SecurityManager.LogOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
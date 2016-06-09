using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VegiJ.Web.MVC.Areas.Users.Controllers
{
    using System.Net;
    using DataAccess;
    using Models;

    [Authorize]
    public class UserController : Controller
    {
        public IUserManager UserManager { get; set; }
        public IRepository<VegiJ.DataAccess.Gender> GenderRepository { get; set; }
        private static User pageUser { get; set; }

        public UserController(IUserManager uManager, IRepository<Gender> gRepository)
        {
            UserManager = uManager;
            GenderRepository = gRepository;
        }
        // GET: Users/User
        [AllowAnonymous]
        public ActionResult Index(string username)
        {
            var model = new IndexViewModel()
            {
                user =
                    UserManager.GetUsers()
                        .AsEnumerable()
                        .Where(u => u.UserName == username)
                        .FirstOrDefault()
            };
            return View(model);
        }
        
        public ActionResult Settings(string username)
        {
            if (username != User.Identity.Name && !User.IsInRole("admin"))
            {
                throw new HttpException(404, "The page you are looking for do not exist.");
            }
            //SettingsViewModel model = new SettingsViewModel();
            var user = UserManager.GetUsers()
                .AsEnumerable()
                .Where(u => u.UserName == username)
                .FirstOrDefault();

            if (user != null)
            {
                pageUser = user;
                var genders = new SelectList((IEnumerable<Gender>)GenderRepository.Table, "ID", "Name");
                if (user.GenderID.HasValue)
                {
                    var selected = genders.Where(x => x.Value == user.GenderID.ToString()).First();
                    selected.Selected = true;
                }
                var model = new SettingsViewModel()
                {
                    ID = user.ID,
                    BirthDate = user.BirthDate,
                    ConfirmPassword = "",
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    GenderID = user.GenderID,
                    Password = "",
                    UserName = user.UserName,
                    ListItems = genders.ToList()
                };

                //var model = new SettingsViewModel();
                //model.ID = user.ID;
                //model.BirthDate = user.BirthDate;
                //model.ConfirmPassword = "";
                //model.Email = user.Email;
                //model.FirstName = user.FirstName;
                //model.LastName = user.LastName;
                //model.Gender = user.Gender;
                //model.GenderID = user.GenderID;
                //model.Password = "";
                //model.UserName = user.UserName;
                //model.ListItems = genders.ToList();

                return View(model);
            }

            throw new HttpException(404, "The page you are looking for do not exist.");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.GetUsers()
                .AsEnumerable()
                .Where(u => u.UserName == model.UserName)
                .FirstOrDefault();

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.BirthDate = model.BirthDate;

                if (!string.IsNullOrEmpty(model.Password))
                {
                    user.Password = PasswordHash.EncryptPassword(model.Password, user.Salt);
                }

                UserManager.UpdateUser(user);

                if (user.GenderID.HasValue && model.GenderID.HasValue && user.GenderID != model.GenderID)
                {
                    user.Gender = null;
                    user.GenderID = model.GenderID;
                    UserManager.UpdateUser(user);
                }

                return RedirectToAction("Index");
            }

            var genders = new SelectList((IEnumerable<Gender>)GenderRepository.Table, "ID", "Name");
            if (model.GenderID.HasValue)
            {
                var selected = genders.Where(x => x.Value == model.GenderID.ToString()).First();
                selected.Selected = true;
            }
            model.ListItems = genders.ToList();
            return View(model);
        }
    }
}
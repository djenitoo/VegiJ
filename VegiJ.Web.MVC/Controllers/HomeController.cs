using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VegiJ.Web.MVC.Controllers
{
    using DataAccess;
    using DataAccess.Contracts;
    using Models;

    public class HomeController : Controller
    {
        //[Inject]
        private IUserManager UserManager { get; set; }
        private IRecipeManager RecipeManager { get; set; }
        private IEventManager EventManager { get; set; }
        private ITipManager TipManager { get; set; }

        //[Inject]
        public HomeController(IUserManager uManager, IRecipeManager rManager, IEventManager eManager, ITipManager tManager)
        {
            this.UserManager = uManager;
            this.RecipeManager = rManager;
            this.EventManager = eManager;
            this.TipManager = tManager;
        }
        public ActionResult Index()
        {
            var model = new HomeViewModel
            {
                LastRecipes = GetLastRecipes(),
                LastRegisteredUserName = GetLastRegisteredUsername(),
                TipOfTheDay = GetTipOfTheDay(),
                UpcomingEvents = GetUpcommingEvents()
            };


            return View(model);
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

        private List<Recipe> GetLastRecipes()
        {
            var recipeList = RecipeManager
                                .GetAllRecipes()
                                .AsEnumerable()
                                .Where(r => r.IsApproved)
                                .OrderByDescending(r => r.CreatedDate)
                                .Take(3)
                                .ToList();

            return recipeList;
        }

        private List<Event> GetUpcommingEvents()
        {
            var upcomingEventsList = EventManager
                                        .GetAllEvent()
                                        .AsEnumerable()
                                        .Where(e => e.IsApproved && e.StartTime > DateTime.Now)
                                        .Take(2)
                                        .ToList();
            return upcomingEventsList;
        }

        private string GetLastRegisteredUsername()
        {
            var lastUser = UserManager.GetUsers()
                    .AsEnumerable()
                    .OrderByDescending(u => u.CreatedDate)
                    .FirstOrDefault();

            return lastUser.UserName;
        }

        private TipOfTheDayViewModel GetTipOfTheDay()
        {
            var approvedTips = TipManager.GetAllTips().AsEnumerable().Where(t => t.IsApproved).ToList();
            int tipsCount = approvedTips.Count;
            int day = (int)((DateTime.Today - new DateTime(2000, 1, 1)).TotalDays);
            Random rnd = new Random(day);
            int id = rnd.Next(0, tipsCount - 1);

            Tip selectedTip = approvedTips[id];

            TipOfTheDayViewModel tipOfTheDayItem = new TipOfTheDayViewModel
            {
                Name = selectedTip.Title,
                Content = selectedTip.Content,
                AuthorUserName = selectedTip.Author.UserName
            };

            return tipOfTheDayItem;
        }
    }
}
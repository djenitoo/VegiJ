using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VegiJ.Web.MVC.Models
{
    using DataAccess;

    public class HomeViewModel
    {
        public List<Recipe> LastRecipes { get; set; }
        public List<Event> UpcomingEvents { get; set; }
        public TipOfTheDayViewModel TipOfTheDay { get; set; }
        public string LastRegisteredUserName { get; set; }
    }

    public class TipOfTheDayViewModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string AuthorUserName { get; set; }
    }
}
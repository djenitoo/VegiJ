using Ninject;
using Ninject.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using VegiJ.DataAccess;
using VegiJ.DataAccess.Contracts;

namespace VegiJ.Web
{
    public partial class ViewRecipe : PageBase
    {
        [Inject]
        public IRecipeManager RecipeManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IEnumerable<Recipe> GetRecipe([RouteData] string title)
        {
            IEnumerable<Recipe> items = null;

            if (!String.IsNullOrEmpty(title))
            {
                items = RecipeManager.GetAllRecipes().AsEnumerable().Where(
                    u =>
                        string.Equals((u.Title as string), title, StringComparison.InvariantCultureIgnoreCase));
            }

            return items;
        }
    }
}
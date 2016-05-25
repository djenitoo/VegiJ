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
    public partial class ViewCategory : PageBase
    {
        [Inject]
        public IRecipeManager RecipeManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (RouteData.Values["category"] == null)
            {
                Response.Redirect(GetRouteUrl("RecipeByNameRoute", new { title = "Vegetarian" }));
            }
        }

        public IEnumerable<Recipe> GetRecipes([RouteData] string category)
        {
            IEnumerable<Recipe> items = null;
            
            if (!String.IsNullOrEmpty(category))
            {
                items = RecipeManager.GetAllRecipes().AsEnumerable().Where(
                    u =>
                        string.Equals((u.Category.Name as string), category, StringComparison.InvariantCultureIgnoreCase) && u.IsApproved);
            }

            return items;
        }

        protected void RadListView1_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            RadListView2.DataSource = GetRecipes(RouteData.Values["category"].ToString());
        }

        protected void RadListView1_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RadListView2.Rebind();
            }
        }
    }
}
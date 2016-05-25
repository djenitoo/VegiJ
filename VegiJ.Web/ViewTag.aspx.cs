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
    public partial class ViewTag : PageBase
    {
        [Inject]
        public IRecipeManager RecipeManager { get; set; }
        [Inject]
        public ITagManager TagManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (RouteData.Values["tag"] == null)
            {
                Response.Redirect(GetRouteUrl("TagByNameRoute", new { tag = "vegetarian" }));
            }
        }

        public IEnumerable<Recipe> GetRecipes([RouteData] string tag)
        {
            IEnumerable<Recipe> items = null;
            var tagItem = TagManager.GetAllTags().AsEnumerable().Where(t =>
                                        string.Equals(t.Name as string, tag, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (tagItem != null)
            {
                items = tagItem.Recipes.ToList();                    
            }

            return items;
        }

        protected void RadListView1_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            RadListView2.DataSource = GetRecipes(RouteData.Values["tag"].ToString());
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
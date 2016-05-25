using Ninject;
using Ninject.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VegiJ.DataAccess.Contracts;

namespace VegiJ.Web
{
    public partial class Tags : PageBase
    {
        [Inject]
        public IRecipeManager RecipeManager { get; set; }
        [Inject]
        public ITagManager TagManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadListView1_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RadListView1.Rebind();
            }
        }

        protected void RadListView1_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            RadListView1.DataSource = this.TagManager.GetAllTags().Where(t => t.Recipes.Count > 0).ToList();
        }

        protected string TagCount(string name)
        {
            var tag = this.TagManager.GetAllTags().AsEnumerable().Where(t => t.Name.Equals(name)).FirstOrDefault();

            return tag.Recipes.Count().ToString();
        }
    }
}
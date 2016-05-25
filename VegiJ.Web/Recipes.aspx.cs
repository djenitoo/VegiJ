using Ninject;
using Ninject.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using VegiJ.DataAccess.Contracts;

namespace VegiJ.Web
{
    public partial class Recipes : PageBase
    {
        [Inject]
        public IRecipeManager RecipeManager { get; set; }
        [Inject]
        public ICategoryManager CategoryManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RadListView1_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            RadListView1.DataSource = this.RecipeManager.GetAllRecipes().ToList();
            var ddl = (sender as RadListView).FindControl("RadDropDownTree1") as RadDropDownTree;
            ddl.DataTextField = "Name";
            ddl.DataFieldID = "ID";
            ddl.DataFieldParentID = "ParentCategoryId";
            ddl.DataValueField = "ID";
            ddl.DataSource = this.CategoryManager.GetAllCategories().ToList();
            ddl.DataBind();
        }

        protected void RadListView1_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RadListView1.Rebind();
            }
        }

        protected void RadDropDownTree1_EntryAdded(object sender, DropDownTreeEntryEventArgs e)
        {
            RadListView1.FilterExpressions.Clear();
            var ddl = RadListView1.FindControl("RadDropDownTree1") as RadDropDownTree;
            RadListView1.FilterExpressions.BuildExpression().Contains("Category.Name", ddl.SelectedText).Build();
            RadListView1.Rebind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RadListView1.FilterExpressions.Clear();
            RadListView1.DataSource = this.RecipeManager.GetAllRecipes().ToList().OrderBy(r => r.CreatedDate);
            RadListView1.Rebind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            RadListView1.FilterExpressions.Clear();
            RadListView1.DataSource = this.RecipeManager.GetAllRecipes().ToList().OrderByDescending(r => r.CreatedDate);
            RadListView1.Rebind();
        }
    }
}
namespace VegiJ.Web
{
    using System;
    using System.Linq;
    using Ninject;
    using VegiJ.DataAccess;
    using VegiJ.DataAccess.Contracts;
    using VegiJ.Logic;
    using System.Collections.Generic;
    public partial class _Default : Ninject.Web.PageBase
    {
        [Inject]
        public IRecipeManager RecipeManager { get; set; }
        [Inject]
        public IUserManager UserManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                WelcomeBackMessage.Text = "Welcome back, " + User.Identity.Name + "!";
                AuthenticatedMessagePanel.Visible = true;
            }
        }
        public IEnumerable<Recipe> GetRecipes()
        {
            IEnumerable<Recipe> items = RecipeManager.GetAllRecipes().AsEnumerable().OrderBy(r => r.CreatedDate).Take(3).ToList();
            
            return items;
        }

        public User RecentUser()
        {
            User items = UserManager.GetUsers().AsEnumerable().OrderByDescending(r => r.CreatedDate).FirstOrDefault();

            return items;
        }

        protected void RadListView1_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            RadListView2.DataSource = GetRecipes();
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
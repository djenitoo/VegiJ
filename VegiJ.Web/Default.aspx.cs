using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Ninject;
using VegiJ.DataAccess;
using VegiJ.DataAccess.Contracts;
using VegiJ.Logic;

namespace VegiJ.Web
{
    public partial class _Default : Ninject.Web.PageBase
    {
        //[Inject]
        //IUserProvider userManager { get; set; }

        private IUserManager userManager;
        private IRecipeManager recipeManager;
        private ICategoryManager categoryManager;
        private IRepository<Tag> tagRepository;
        private User currentUser;
        [Inject]
        public void Setup(IDbContext context)
        {
            //IRepository<User> userRepository, IRepository<Recipe> recipeRepository
            // load every repo
            this.userManager = new UserManager(new Repository<User>(context));
            SecurityManager.LoadUserRepository(new Repository<User>(context));
            this.categoryManager = new CategoryManager(new Repository<Category>(context));
            this.tagRepository = new Repository<Tag>(context);
            this.recipeManager = new RecipeManager(new Repository<Recipe>(context));
            //Repository<Category> rep = new Repository<Category>(catRepo);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //User exmplUser = new User("arya", "012358", "noOne@abv.bg");
            //userManager.AddUser(exmplUser);
            usersGridView.DataSource = userManager.GetUsers().ToList();
            usersGridView.DataBind();
        }

        protected void logOutButton_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.User = null;
            SecurityManager.LogOut();
            LogInButton.Visible = true;
            logOutButton.Visible = false;
            Response.Redirect(Request.RawUrl);
        }

        protected void LogInButton_Click(object sender, EventArgs e)
        {
            if (SecurityManager.LogIn("jenny", "012358"))
            {
                currentUser = SecurityManager.GetCurrentUser();
                HttpContext.Current.User = currentUser;                
            }
            
            if (Request.IsAuthenticated)
            {
                WelcomeBackMessage.Text = "Welcome back, " + HttpContext.Current.User.Identity.Name + "!";
                AuthenticatedMessagePanel.Visible = true;
                LogInButton.Visible = false;
                logOutButton.Visible = true;              
            }
        }

        protected void CreateRecipeBtn_Click(object sender, EventArgs e)
        {

            //Category ctg = new Category("Vegetarian");
            //this.categoryManager.AddCategory(ctg);
            //Recipe vegiRecipe = new Recipe("Vegies with cheese", "Bla bla bla");
            //vegiRecipe.CategoryID = ctg.ID;
            //var tag = new Tag("vegetarian");
            //tagRepository.Create(tag);
            //vegiRecipe.Tags = new List<Tag> {tag};
            //vegiRecipe.AuthorId = userManager.GetUsers().Where(u => u.UserName.Equals("jenny")).FirstOrDefault().ID;
            //this.recipeManager.AddRecipe(vegiRecipe);
            var vegiRecipe = this.recipeManager.GetAllRecipes().FirstOrDefault();
            var res = vegiRecipe.Tags.Count;

            // TODO: Maybe put category/tag creation in RecipeManager if they do not exist?
        }
    }
}
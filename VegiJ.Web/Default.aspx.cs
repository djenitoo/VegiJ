namespace VegiJ.Web
{
    using System;
    using System.Linq;
    using Ninject;
    using VegiJ.DataAccess;
    using VegiJ.DataAccess.Contracts;
    using VegiJ.Logic;

    public partial class _Default : Ninject.Web.PageBase
    {
        //[Inject]
        //IUserProvider userManager { get; set; }

        private IUserManager userManager;
        //private IRecipeManager recipeManager;
        //private ICategoryManager categoryManager;
        //private IRepository<Tag> tagRepository;
        //private IRepository<Event> eventRepository;
        //private IRepository<Tip> tipRepository;
        //private User currentUser;

        [Inject]
        public void Setup(IDbContext context)
        {
            //IRepository<User> userRepository, IRepository<Recipe> recipeRepository
            // load every repo
            this.userManager = new UserManager(new Repository<User>(context));
            // TODO: Move load security repo to master?
            //SecurityManager.LoadUserRepository(new Repository<User>(context));
            //this.categoryManager = new CategoryManager(new Repository<Category>(context));
            //this.tagRepository = new Repository<Tag>(context);
            //this.eventRepository = new Repository<Event>(context);
            //this.tipRepository = new Repository<Tip>(context);
            //this.recipeManager = new RecipeManager(new Repository<Recipe>(context));
            //Repository<Category> rep = new Repository<Category>(catRepo);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //User exmplUser = new User("arya", "012358", "noOne@abv.bg");
            //userManager.CreateUser(exmplUser);
            usersGridView.DataSource = userManager.GetUsers().ToList();
            usersGridView.DataBind();
            //var frms = User.Identity.AuthenticationType;
            if (Request.IsAuthenticated)
            {
                WelcomeBackMessage.Text = "Welcome back, " + User.Identity.Name + "!";
                AuthenticatedMessagePanel.Visible = true;
            }
            //var jaqen = userManager.GetUsers().Where(u => u.UserName.Equals("jaqen")).FirstOrDefault();
            //jaqen.GenderID = Guid.Parse("131EE21F-7716-4BC8-8530-0429EF8D33CA");
            //userManager.UpdateUser(jaqen);
        }
        
        protected void CreateRecipeBtn_Click(object sender, EventArgs e)
        {

            //Category ctg = new Category("Vegetarian");
            //this.categoryManager.AddCategory(ctg);
            //Recipe vegiRecipe = new Recipe("Vegies with cheese", "Bla bla bla");
            //vegiRecipe.CategoryID = ctg.ID;
            //var tag = new Tag("vegetarian");
            //tagRepository.Create(tag);
            //vegiRecipe.Tags = new Collection<Tag> {tag};
            //vegiRecipe.AuthorId = userManager.GetUsers().Where(u => u.UserName.Equals("arya")).FirstOrDefault().ID;
            //this.recipeManager.AddRecipe(vegiRecipe);
            //var vegiEvent = new Event("Game of thrones: season 6 ep 1", "Home");
            //vegiEvent.StartTime = DateTime.Now.AddHours(8);
            //vegiEvent.AuthorId = vegiRecipe.AuthorId;
            //this.eventRepository.Create(vegiEvent);
            //var firstTip = new Tip("Eat rice with soy sauce", "For better flavor add soy sauce to your rice. It's so yummy!");
            //firstTip.AuthorId = vegiEvent.AuthorId;
            //this.tipRepository.Create(firstTip);
            //var vegiRecipe = this.recipeManager.GetAllRecipes().FirstOrDefault();
            //var res = vegiRecipe.Tags.Count;
            //var eventVegi = this.eventRepository.Table.FirstOrDefault();
            //var tipOfArya = this.tipRepository.Table.FirstOrDefault();
            // TODO: Maybe put category/tag creation in RecipeManager if they do not exist?
        }
    }
}
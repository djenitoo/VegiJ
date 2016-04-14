using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
        private IUserProvider userManager;
        [Inject]
        public void Setup(IRepository<User> userRepository)
        {
            // ?
            this.userManager = new UserManager(userRepository);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // userManager.GetUsers();
            // example:
            //customersGridView.DataSource = repository.GetAll();
            //customersGridView.DataBind();
            User jenny = new DataAccess.User("jenny", "012358", "djeni_1993@abv.bg");
            //userManager.AddUser(jenny);
            usersGridView.DataSource = userManager.GetUsers().ToList();
            usersGridView.DataBind();
        }
    }
}
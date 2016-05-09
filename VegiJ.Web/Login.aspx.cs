namespace VegiJ.Web
{
    using System;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DataAccess;
    using DataAccess.Contracts;
    using Helpers;
    using Logic;
    using Ninject;

    public partial class Login : Ninject.Web.PageBase
    {

        private IRepository<User> userRepository { get; set; }

        [Inject]
        public void Setup(IDbContext context)
        {
            this.userRepository = new Repository<User>(context);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.TxtboxUsername.Text = "";
            this.TxtBoxPassword.Text = "";
            this.CheckBoxRememberMe.Checked = false;

            ClearTextBoxes(Page);
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            
            //try
            //{
                SecurityManager.LoadUserRepository(userRepository);
                if (SecurityManager.LogIn(TxtboxUsername.Text, TxtBoxPassword.Text, CheckBoxRememberMe.Checked))
                {
                    var currentUser = SecurityManager.GetCurrentUser();
                    //string[] roles = { currentUser.IsAdmin ? "admin" : ""};
                    //var userIdentity = new GenericIdentity(currentUser.UserName, "Forms");
                    
                    //HttpContext.Current.User = currentUser;
                    //HttpContext.Current.User = currentUser;
                    
                    //FormsAuthentication.RedirectFromLoginPage(currentUser.UserName, CheckBoxRememberMe.Checked);
                    var url = Request.QueryString["ReturnUrl"] ?? "~/Default.aspx";
                    HttpContext.Current.Response.Redirect(url);

                }
            //}
            //catch (Exception ex)
            //{
            //    this.Page.Validators.Add(new ValidationError(ex.Message));
            //}
        }

        protected void ClearTextBoxes(Control p1)
        {
            foreach (Control ctrl in p1.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox t = ctrl as TextBox;

                    if (t != null)
                    {
                        t.Text = String.Empty;
                    }
                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                    {
                        ClearTextBoxes(ctrl);
                    }
                }
            }
        }

    }
}
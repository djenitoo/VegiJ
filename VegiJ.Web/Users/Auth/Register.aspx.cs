namespace VegiJ.Web.Users.Auth
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DataAccess;
    using Helpers;
    using Ninject;
    
    public partial class Register : Ninject.Web.PageBase
    {
        [Inject]
        public IUserManager UserManager { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Default.aspx");
            }

            this.PanelRegister.Visible = true;
            this.PanelSuccessfulRegister.Visible = false;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string username = TxtboxUsername.Text.Trim();
            string password = TxtboxPassword.Text.Trim();
            string email = TxtboxEmail.Text.Trim();
            string firstName = TxtboxFirstName.Text.Trim();
            string lastName = TxtboxLastName.Text.Trim();

            try
            {
                var userToRegister = new User(username, password, email)
                {
                    FirstName = firstName,
                    LastName = lastName
                };
                this.UserManager.CreateUser(userToRegister);
            }
            catch (Exception ex)
            {
                this.Page.Validators.Add(new ValidationError(ex.Message));
                return;
            }

            // Here successful continue after registration
            this.PanelRegister.Visible = false;
            this.PanelSuccessfulRegister.Visible = true;
        }
        
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(Page);
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
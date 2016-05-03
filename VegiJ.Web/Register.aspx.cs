

namespace VegiJ.Web
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DataAccess;
    using Helpers;
    using Logic;
    using Ninject;


    public partial class Register : Ninject.Web.PageBase
    {
        private IUserManager userManager { get; set; }

        [Inject]
        public void Setup(IRepository<User> repository)
        {
            this.userManager = new UserManager(repository);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.PanelRegister.Visible = true;
            this.PanelSuccessfulRegister.Visible = false;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string username = TxtboxUsername.Text.Trim();
            string password = TxtboxPassword.Text.Trim();
            string email = TxtboxEmail.Text.Trim();
            
            try
            {
                var userToRegister = new User(username, password, email);
                userToRegister.FirstName = TxtboxFirstName.Text.Trim();
                userToRegister.LastName = TxtboxLastName.Text.Trim();
                this.userManager.AddUser(userToRegister);
            }
            catch (Exception ex)
            {
                this.Page.Validators.Add(new ValidationError(ex.Message));
                return;
            }

            // TODO: Here successful continue after registration
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
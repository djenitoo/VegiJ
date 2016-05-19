
namespace VegiJ.Web.Users
{
    using System.Security.Claims;
    using System.Web.ModelBinding;
    using System.Web.UI.WebControls;
    using DataAccess;
    using Logic;
    using Ninject;
    using Ninject.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Data;
    using Telerik.Web.UI;
    public partial class Settings : PageBase
    {
        [Inject]
        public IUserManager UserManager { get; set; }
        [Inject]
        public IRepository<Gender> genderRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!RouteData.Values.ContainsKey("username") && !Request.QueryString.AllKeys.Contains("UserID"))
            {
                if (User.Identity.IsAuthenticated)
                {
                    Response.Redirect(GetRouteUrl("SettingsByUserNameRoute", new { username = User.Identity.Name }));
                }
                else
                {
                    Response.Redirect("Auth/Login.aspx");
                }
            }
            else
            {
                if (!IsOwnProfilePage() && !User.IsInRole("admin"))
                {
                    Response.Redirect(GetRouteUrl("SettingsByUserNameRoute", new { username = User.Identity.Name }));
                }
            }
            
            if (!IsPostBack)
            {
                Page.DataBind();
                this.LoadGenders();
                RadDatePicker datePicker = (RadDatePicker)userSettings2.FindControl("RadDatePicker2");
                datePicker.SelectedDate = datePicker.DbSelectedDate != null ? DateTime.Parse(datePicker.DbSelectedDate.ToString()) : datePicker.MaxDate;
            }

        }

        public IEnumerable<User> GetUser([QueryString("UserID")] Guid? userId,
                        [RouteData] string username)
        {
            IEnumerable<User> user = null;

            if (!String.IsNullOrEmpty(username))
            {
                user = UserManager.GetUsers().AsEnumerable().Where(
                    u =>
                        string.Equals((u.UserName as string), username, StringComparison.InvariantCultureIgnoreCase));
            }
            else if (userId.HasValue)
            {
                user = UserManager.GetUsers().AsEnumerable().Where(u => u.ID == userId.Value);
            }

            return user;
        }

        // TODO: if own profile show sensitive fields
        public bool IsOwnProfilePage()
        {
            if (RouteData.Values.ContainsKey("username"))
            {
                return string.Equals(User.Identity.Name, RouteData.Values["username"].ToString(),
                StringComparison.InvariantCultureIgnoreCase);
            }
            else if (Request.QueryString["UserID"] != null)
            {
                var userClaims = (ClaimsIdentity)User.Identity;
                return
                    Guid.Parse(userClaims.FindFirst(ClaimTypes.NameIdentifier).Value)
                        .Equals(Guid.Parse(Request.QueryString["UserID"]));
            }
            else
            {
                return false;
            }

        }

        private void LoadGenders()
        {
            try
            {
                DropDownList ddlGenders = (DropDownList)userSettings2.FindControl("DropDownGender");
                ddlGenders.DataSource = this.genderRepository.Table.ToList().OrderBy(g => g.Name);
                //DropDownGender.DataSource = this.genderRepository.Table.ToList();
                ddlGenders.DataTextField = "Name";
                ddlGenders.DataValueField = "ID";
                ddlGenders.DataBind();
                var genderID = (this.GetUser(null, (string)RouteData.Values["username"]).FirstOrDefault()).GenderID.ToString();
                if (!string.IsNullOrEmpty(genderID))
                {
                    ddlGenders.Items.FindByValue(genderID).Selected = true;
                }
                else
                {
                    ddlGenders.Items.FindByText("Other").Selected = true;
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Error occured while loading the gender list.");
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void userSettings2_UpdateItem(Guid ID)
        {
            User item = UserManager.GetUser(ID);
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", ID));
                return;
            }
            DropDownList ddlGenders = (DropDownList)userSettings2.FindControl("DropDownGender");
            item.Gender = null;
            item.GenderID = ddlGenders.SelectedValue != "" ? Guid.Parse(ddlGenders.SelectedValue) : item.GenderID;
            UserManager.UpdateUser(item);
            TextBox txtBoxFirstName = (TextBox)userSettings2.FindControl("TxtboxFirstName");
            item.FirstName = txtBoxFirstName.Text;
            TextBox txtBoxLastName = (TextBox)userSettings2.FindControl("TxtboxLastName");
            item.FirstName = txtBoxLastName.Text;
            TextBox txtEmail = (TextBox)userSettings2.FindControl("TxtboxEmail");
            item.FirstName = txtEmail.Text;
            RadDatePicker datePicker = (RadDatePicker)userSettings2.FindControl("RadDatePicker2");
            item.BirthDate = datePicker.SelectedDate;
            TextBox txtPassword = (TextBox)userSettings2.FindControl("TxtboxPassword");
            if (txtPassword.Text != "")
            {
                item.Password = PasswordHash.EncryptPassword(txtPassword.Text, item.Salt);
            }

            TryUpdateModel(item);
            //ModelState["ID"].Errors.Clear();
            //if (ModelState.IsValid)
            //{
                UserManager.UpdateUser(item);
                Response.Redirect(GetRouteUrl("UserByNameRoute", new { username = RouteData.Values["username"] }));
                // Save changes here, e.g. MyDataLayer.SaveChanges();

            //}
        }

        protected void BtnCancel_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(GetRouteUrl("UserByNameRoute", new { username = RouteData.Values["username"] }));
        }
    }
}
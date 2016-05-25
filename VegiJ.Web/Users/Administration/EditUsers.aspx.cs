namespace VegiJ.Web.Users.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Data.Entity.Infrastructure;
    using VegiJ.DataAccess;
    using Ninject;
    using Ninject.Web;
    using Telerik.Web.UI;
    using System.Data;
    public partial class EditUsers : PageBase
    {
        [Inject]
        public IUserManager UserManager { get; set; }
        [Inject]
        public IRepository<Gender> genderRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RadGrid1_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            var userId = Guid.Parse(editableItem.GetDataKeyValue("ID").ToString());
            var user = this.UserManager.GetUser(userId);
            if (user != null)
            {
                try
                {
                    this.UserManager.DeleteUser(user);
                }
                catch (Exception ex)
                {

                    ShowErrorMessage("Error occured while deleting the username. " + ex.Message);
                }
            }

        }

        private void ShowErrorMessage(string msg)
        {
            RadAjaxManager1.ResponseScripts.Add(string.Format(msg));
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            var userId = Guid.Parse(editableItem.GetDataKeyValue("ID").ToString());
            var user = UserManager.GetUser(userId);
            if (user != null)
            {
                TextBox newEmail = (TextBox)e.Item.FindControl("TextBox9");
                user.Email = newEmail.Text;
                TextBox fName = (TextBox)e.Item.FindControl("TextBox2");
                user.FirstName = fName.Text;
                TextBox lName = (TextBox)e.Item.FindControl("TextBox3");
                user.LastName = lName.Text;
                RadDatePicker newBDate = (RadDatePicker)e.Item.FindControl("BirthDatePicker");
                user.BirthDate = newBDate.SelectedDate;
                CheckBox newIsAdmin = (CheckBox)e.Item.FindControl("CheckBox1");
                user.IsAdmin = newIsAdmin.Checked;
                TextBox newPassword = (TextBox)e.Item.FindControl("TextBox5");
                if (newPassword.Text != "")
                {
                    user.Password = PasswordHash.EncryptPassword(newPassword.Text, user.Salt);
                }

                try
                {
                    UserManager.UpdateUser(user);
                }
                catch (System.Exception)
                {
                    ShowErrorMessage("Error occured while updating the username.");
                }

                DropDownList newGender = (DropDownList)e.Item.FindControl("ddlTOC");
                if (newGender.SelectedValue != "" && 
                    ((user.GenderID == null && newGender.SelectedValue != "" ) || 
                    !Guid.Equals(user.GenderID, Guid.Parse(newGender.SelectedValue))))
                {
                    user.Gender = null;
                    user.GenderID = Guid.Parse(newGender.SelectedValue);
                    try
                    {
                        UserManager.UpdateUser(user);
                    }
                    catch (System.Exception)
                    {
                        ShowErrorMessage("Error occured while updating the username gender.");
                    }
                }
            }
        }

        protected void RadGrid1_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //try
            //{
            TextBox uName = (TextBox)e.Item.FindControl("TextBox7");
            TextBox uEmail = (TextBox)e.Item.FindControl("TextBox9");
            TextBox fName = (TextBox)e.Item.FindControl("TextBox2");
            TextBox lName = (TextBox)e.Item.FindControl("TextBox3");
            RadDatePicker newBDate = (RadDatePicker)e.Item.FindControl("BirthDatePicker");
            CheckBox newIsAdmin = (CheckBox)e.Item.FindControl("CheckBox1");
            TextBox newPassword = (TextBox)e.Item.FindControl("TextBox5");
            DropDownList newGender = (DropDownList)e.Item.FindControl("ddlTOC");
            User item = new DataAccess.User(uName.Text, newPassword.Text, uEmail.Text, newBDate.SelectedDate.ToString(), newGender.SelectedValue)
            {
                FirstName = fName.Text,
                LastName = lName.Text,
                IsAdmin = newIsAdmin.Checked
            };
            UserManager.CreateUser(item);
            //}
            //catch (System.Exception ex)
            //{
            //    ShowErrorMessage("Error occured while creating the username. " + ex.Message);
            //}

        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RadGrid1.EditIndexes.Add(0);
                RadGrid1.Rebind();
            }
        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = this.UserManager.GetUsers().ToList();
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddlTOC");
                if (!(e.Item is GridEditFormInsertItem))
                {
                    ddl.Items.FindByValue(editedItem.GetDataKeyValue("GenderID").ToString()).Selected = true;
                }
                //GridEditManager editMan = editedItem.EditManager;
                //GridDropDownListColumnEditor editor = (GridDropDownListColumnEditor)(editMan.GetColumnEditor("ddl1"));
                //RadComboBox combo = editor.ComboBoxControl;
                //combo.AllowCustomText = true;
                //combo.MarkFirstMatch = true;
                //combo.DataSourse = //set the desired data source           
            }
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                HyperLink link = (HyperLink)item["ProfileLink"].Controls[0];
                link.NavigateUrl = GetRouteUrl("UserByNameRoute", new { username = item.GetDataKeyValue("UserName") });
                //you can now modify link.NavigateUrl
            }
        }
    }
}
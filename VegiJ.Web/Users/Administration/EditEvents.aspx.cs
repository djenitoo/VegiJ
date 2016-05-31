using Ninject;
using Ninject.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using VegiJ.DataAccess;
using VegiJ.DataAccess.Contracts;

namespace VegiJ.Web.Users.Administration
{
    public partial class EditEvents : PageBase
    {
        [Inject]
        public IUserManager UserManager { get; set; }
        [Inject]
        public IEventManager EventManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RadGrid1.Rebind();
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = this.EventManager.GetAllEvent().ToList();
        }
        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }
        protected void RadGrid1_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem insertedItem = e.Item as GridEditableItem;
            Hashtable newValues = new Hashtable();
            insertedItem.ExtractValues(newValues);
            try
            {
                var item = new Event(newValues["Name"].ToString(), newValues["Place"].ToString());
                item.IsApproved = bool.Parse(newValues["IsApproved"].ToString());
                item.StartTime = DateTime.Parse(newValues["StartTime"].ToString());

                RadComboBox recipeAuthor = e.Item.FindControl("RadComboBox1") as RadComboBox;
                if (recipeAuthor.SelectedValue != "")
                {
                    item.AuthorId = Guid.Parse(recipeAuthor.SelectedValue);
                }
                else
                {
                    item.AuthorId = Guid.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
                }

                // DB here            
                this.EventManager.AddEvent(item);
            }
            catch (Exception)
            {
                e.Canceled = true;
            }
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            var eventId = Guid.Parse(editableItem.GetDataKeyValue("ID").ToString());
            var item = EventManager.GetEvent(eventId);
            Hashtable newValues = new Hashtable();
            editableItem.ExtractValues(newValues);

            if (item != null)
            {
                item.Name = newValues["Name"].ToString();
                item.Place = newValues["Place"].ToString();
                item.IsApproved = bool.Parse(newValues["IsApproved"].ToString());
                item.StartTime = DateTime.Parse(newValues["StartTime"].ToString());

                RadComboBox recipeAuthor = e.Item.FindControl("RadComboBox1") as RadComboBox;
                if (recipeAuthor.SelectedValue != "")
                {
                    item.AuthorId = Guid.Parse(recipeAuthor.SelectedValue);
                }

                try
                {
                    EventManager.UpdateEvent(item);
                }
                catch (System.Exception)
                {
                }
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;

                RadComboBox authorBox = (RadComboBox)e.Item.FindControl("RadComboBox1");
                authorBox.DataTextField = "UserName";
                authorBox.DataValueField = "ID";
                authorBox.AllowCustomText = false;
                authorBox.MarkFirstMatch = true;
                authorBox.DataSource = this.UserManager.GetUsers().ToList();
                authorBox.DataBind();
                if (!(e.Item is GridEditFormInsertItem))
                {
                    authorBox.SelectedValue = editedItem.GetDataKeyValue("AuthorID").ToString();
                }
            }
        }

        protected void RadGrid1_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            var eventId = Guid.Parse(editableItem.GetDataKeyValue("ID").ToString());
            var vegiEvent = this.EventManager.GetEvent(eventId);
            if (vegiEvent != null)
            {
                try
                {
                    this.EventManager.DeleteEvent(vegiEvent);
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
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

    public partial class EditUsers : PageBase
    {
        [Inject]
        public IUserManager UserManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.DataBind();
            }
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<User> editUsersGrid_GetData()
        {
            return UserManager.GetUsers();
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void editUsersGrid_UpdateItem(Guid ID)
        {
            User item = UserManager.GetUser(ID);
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            if (item == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", ID));
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                // Save changes here, e.g. MyDataLayer.SaveChanges();

            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void editUsersGrid_DeleteItem(Guid id)
        {

        }
    }
}
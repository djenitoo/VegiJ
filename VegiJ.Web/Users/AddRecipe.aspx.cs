using Ninject;
using Ninject.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using VegiJ.DataAccess;
using VegiJ.DataAccess.Contracts;

namespace VegiJ.Web.Users
{
    public partial class AddRecipe : PageBase
    {
        [Inject]
        public IRecipeManager RecipeManager { get; set; }
        [Inject]
        public ICategoryManager CategoryManager { get; set; }
        [Inject]
        public ITagManager TagManager { get; set; }
        [Inject]
        public IUserManager UserManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadDataForm1_NeedDataSource(object sender, Telerik.Web.UI.RadDataFormNeedDataSourceEventArgs e)
        {
            RadDataForm1.DataSource = this.RecipeManager.GetAllRecipes().ToList();
        }

        protected void RadDataForm1_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                (sender as RadDataForm).IsItemInserted = true;

                (sender as RadDataForm).Rebind();
            }
        }

        protected void RadDataForm1_ItemCreated(object sender, RadDataFormItemEventArgs e)
        {
            if (e.Item is RadDataFormInsertItem)
            {
                RadDataFormInsertItem editedItem = e.Item as RadDataFormInsertItem;
                RadDropDownTree ddl = (RadDropDownTree)e.Item.FindControl("RadDropDownTree1");
                ddl.DataTextField = "Name";
                ddl.DataFieldID = "ID";
                ddl.DataFieldParentID = "ParentCategoryId";
                ddl.DataValueField = "ID";
                ddl.DataSource = this.CategoryManager.GetAllCategories().ToList();
                ddl.DataBind();

                RadAutoCompleteBox acBox = (RadAutoCompleteBox)e.Item.FindControl("RadAutoCompleteBox1");
                acBox.DataTextField = "Name";
                acBox.DataValueField = "ID";
                acBox.DataSource = this.TagManager.GetAllTags().ToList();
                acBox.DataBind();

                RadComboBox authorBox = (RadComboBox)e.Item.FindControl("RadComboBox1");
                authorBox.DataTextField = "UserName";
                authorBox.DataValueField = "ID";
                authorBox.AllowCustomText = false;
                authorBox.MarkFirstMatch = true;
                authorBox.DataSource = this.UserManager.GetUsers().ToList();
                authorBox.DataBind();
            }
        }

        protected void BtnNewCategory_Click(object sender, EventArgs e)
        {
            Button newCat = (sender as Button);
            newCat.Visible = false;
            Button cancelNewCat = newCat.Parent.FindControl("BtnCancelNewCategory") as Button;
            cancelNewCat.Visible = true;
            RadDropDownTree ddl = newCat.Parent.FindControl("RadDropDownTree1") as RadDropDownTree;
            ddl.DefaultMessage = "Select Parent Category";
            RadTreeNode node = new RadTreeNode() { Text = "Root", Value = "root" };
            ddl.EmbeddedTree.Nodes.Insert(0, node);
            RadTextBox newCatName = newCat.Parent.FindControl("NewCategoryTextBox") as RadTextBox;
            newCatName.Visible = true;
        }

        protected void BtnCancelNewCategory_Click(object sender, EventArgs e)
        {
            Button cancelNewCat = (sender as Button);
            cancelNewCat.Visible = false;
            Button newCat = cancelNewCat.Parent.FindControl("BtnNewCategory") as Button;
            RadDropDownTree ddl = newCat.Parent.FindControl("RadDropDownTree1") as RadDropDownTree;
            ddl.DefaultMessage = "Select Category";
            ddl.EmbeddedTree.Nodes.RemoveAt(0);
            newCat.Visible = true;
            RadTextBox newCatName = cancelNewCat.Parent.FindControl("NewCategoryTextBox") as RadTextBox;
            newCatName.Text = "";
            newCatName.Visible = false;
        }

        protected void RadDataForm1_ItemInserting(object sender, RadDataFormCommandEventArgs e)
        {
            RadDataFormEditableItem insertedItem = e.DataFormItem as RadDataFormEditableItem;
            Hashtable newValues = new Hashtable();
            insertedItem.ExtractValues(newValues);
            try
            {
                var item = new Recipe(newValues["Title"].ToString(), newValues["Content"].ToString());
                Category itemCat = null;
                item.IsApproved = bool.Parse(newValues["IsApproved"].ToString());
                RadComboBox recipeAuthor = e.DataFormItem.FindControl("RadComboBox1") as RadComboBox;

                if (recipeAuthor.SelectedValue != "")
                {
                    item.AuthorId = Guid.Parse(recipeAuthor.SelectedValue);
                }
                else
                {
                    item.AuthorId = Guid.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
                }
                RadDropDownTree catDD = e.DataFormItem.FindControl("RadDropDownTree1") as RadDropDownTree;


                RadAutoCompleteBox tagsBox = e.DataFormItem.FindControl("RadAutoCompleteBox1") as RadAutoCompleteBox;
                foreach (AutoCompleteBoxEntry entry in tagsBox.Entries)
                {
                    if (entry.Value == "")
                    {
                        var tag = new Tag(entry.Text);
                        this.TagManager.AddTag(tag);
                        item.Tags.Add(tag);
                    }
                    else
                    {
                        var tag = this.TagManager.GetTag(Guid.Parse(entry.Value));
                        item.Tags.Add(tag);
                    }
                }

                // DB here            
                // category managment
                RadTextBox newCatText = e.DataFormItem.FindControl("NewCategoryTextBox") as RadTextBox;
                if (!string.IsNullOrWhiteSpace(newCatText.Text) && catDD.SelectedValue != "")
                {
                    Guid? parentID = catDD.SelectedValue != "root" ? Guid.Parse(catDD.SelectedValue) : Guid.Empty;
                    itemCat = new Category(newCatText.Text)
                    {
                        ParentCategoryId = parentID == Guid.Empty ? null : parentID
                    };
                    item.CategoryID = itemCat.ID;
                    item.Category = itemCat;
                }
                if (string.IsNullOrWhiteSpace(newCatText.Text) && catDD.SelectedValue != "")
                {
                    item.CategoryID = Guid.Parse(catDD.SelectedValue);
                    item.Category = null;
                }
                if (itemCat != null)
                {
                    this.CategoryManager.AddCategory(itemCat);
                }

                this.RecipeManager.AddRecipe(item);
            }
            catch (Exception)
            {
                e.Canceled = true;
            }

            // Successful
            Response.Redirect(GetRouteUrl("RecipeByNameRoute", new { recipe = newValues["Title"].ToString() }));
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recipes.aspx");
        }
    }
}
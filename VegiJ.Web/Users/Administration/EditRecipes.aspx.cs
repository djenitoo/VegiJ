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
    public partial class EditRecipes : PageBase
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

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //RadGrid1.EditIndexes.Add(0);
                //RadAutoCompleteBox acBox = RadGrid1.FindControl("RadAutoCompleteBox1") as RadAutoCompleteBox;
                RadGrid1.Rebind();
            }
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = this.RecipeManager.GetAllRecipes().ToList();
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
                var item = new Recipe(newValues["Title"].ToString(), newValues["Content"].ToString());
                Category itemCat = null;
                item.IsApproved = bool.Parse(newValues["IsApproved"].ToString());

                RadComboBox recipeAuthor = e.Item.FindControl("RadComboBox1") as RadComboBox;
                if (recipeAuthor.SelectedValue != "")
                {
                    item.AuthorId = Guid.Parse(recipeAuthor.SelectedValue);
                }
                else
                {
                    item.AuthorId = Guid.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
                }
                
                RadAutoCompleteBox tagsBox = e.Item.FindControl("RadAutoCompleteBox1") as RadAutoCompleteBox;
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
                RadDropDownTree catDD = e.Item.FindControl("RadDropDownTree1") as RadDropDownTree;
                RadTextBox newCatText = e.Item.FindControl("NewCategoryTextBox") as RadTextBox;
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

        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var editableItem = ((GridEditableItem)e.Item);
            var recipeId = Guid.Parse(editableItem.GetDataKeyValue("ID").ToString());
            var recipe = RecipeManager.GetRecipe(recipeId);
            Hashtable newValues = new Hashtable();
            editableItem.ExtractValues(newValues);
            Category itemCat = null;
            if (recipe != null)
            {
                recipe.Title = newValues["Title"].ToString();
                recipe.Content = newValues["Content"].ToString();
                recipe.IsApproved = bool.Parse(newValues["IsApproved"].ToString());

                RadComboBox recipeAuthor = e.Item.FindControl("RadComboBox1") as RadComboBox;
                if (recipeAuthor.SelectedValue != "")
                {
                    recipe.AuthorId = Guid.Parse(recipeAuthor.SelectedValue);
                }

                RadAutoCompleteBox tagsBox = e.Item.FindControl("RadAutoCompleteBox1") as RadAutoCompleteBox;
                foreach (AutoCompleteBoxEntry entry in tagsBox.Entries)
                {
                    if (entry.Value == "")
                    {
                        var tag = new Tag(entry.Text);
                        this.TagManager.AddTag(tag);
                        recipe.Tags.Add(tag);
                    }
                    else
                    {
                        var tag = this.TagManager.GetTag(Guid.Parse(entry.Value));
                        if (!recipe.Tags.Contains(tag))
                        {
                            recipe.Tags.Add(tag);
                        }
                    }
                }

                try
                {
                    RecipeManager.UpdateRecipe(recipe);
                }
                catch (System.Exception)
                {
                }

                // Category
                RadDropDownTree catDD = e.Item.FindControl("RadDropDownTree1") as RadDropDownTree;
                RadTextBox newCatText = e.Item.FindControl("NewCategoryTextBox") as RadTextBox;
                if (!string.IsNullOrWhiteSpace(newCatText.Text) && catDD.SelectedValue != editableItem.GetDataKeyValue("CategoryID").ToString())
                {
                    Guid? parentID = catDD.SelectedValue != "root" ? Guid.Parse(catDD.SelectedValue) : Guid.Empty;
                    itemCat = new Category(newCatText.Text)
                    {
                        ParentCategoryId = parentID == Guid.Empty ? null : parentID
                    };
                    recipe.CategoryID = itemCat.ID;
                    recipe.Category = itemCat;
                }
                if (string.IsNullOrWhiteSpace(newCatText.Text) && catDD.SelectedValue != "")
                {
                    recipe.CategoryID = Guid.Parse(catDD.SelectedValue);
                    recipe.Category = null;
                }
                if (itemCat != null)
                {
                    this.CategoryManager.AddCategory(itemCat);
                }
                try
                {
                    RecipeManager.UpdateRecipe(recipe);
                }
                catch (System.Exception)
                {
                }
            }
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

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                RadDropDownTree ddl = (RadDropDownTree)e.Item.FindControl("RadDropDownTree1");
                ddl.DataTextField = "Name";
                ddl.DataFieldID = "ID";
                ddl.DataFieldParentID = "ParentCategoryId";
                ddl.DataValueField = "ID";
                ddl.DataSource = this.CategoryManager.GetAllCategories().ToList();
                ddl.DataBind();
                if (!(e.Item is GridEditFormInsertItem))
                {
                    ddl.SelectedValue = editedItem.GetDataKeyValue("CategoryID").ToString();
                }

                RadAutoCompleteBox acBox = (RadAutoCompleteBox)e.Item.FindControl("RadAutoCompleteBox1");
                acBox.DataTextField = "Name";
                acBox.DataValueField = "ID";
                //acBox.DataSource = this.TagManager.GetAllTags().ToList();
                acBox.DataBind();
                if (!(e.Item is GridEditFormInsertItem))
                {
                    foreach (var tag in this.TagManager.GetAllTags().ToList())
                    {
                        if (tag.Recipes.Any(r => r.ID.ToString() == editedItem.GetDataKeyValue("ID").ToString()))
                        {
                            acBox.Entries.Add(new AutoCompleteBoxEntry(tag.Name, tag.ID.ToString()));
                        }
                    }
                }
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
            var recipeId = Guid.Parse(editableItem.GetDataKeyValue("ID").ToString());
            var recipe = this.RecipeManager.GetRecipe(recipeId);
            if (recipe != null)
            {
                try
                {
                    this.RecipeManager.DeleteRecipe(recipe);
                }
                catch (Exception ex)
                {
                }
            }

        }

        public IEnumerable<Tag> GetTags()
        {
            IEnumerable<Tag> tags = this.TagManager.GetAllTags().AsEnumerable();
            return tags;
        }
    }
}
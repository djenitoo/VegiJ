using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VegiJ.Web.MVC.Areas.Administration.Models
{
    using System.Web.Mvc;
    using DataAccess;
    using DataAccess.Contracts;
    using Logic;
    using Ninject.Infrastructure.Language;

    public class RecipeServices
    {
        private IUserManager UserManager { get; set; }
        private ITagManager TagManager { get; set; }
        private ICategoryManager CategoryManager { get; set; }
        private IRecipeManager RecipeManager { get; set; }

        public RecipeServices(IUserManager uManager,
                              ITagManager tagManager,
                              ICategoryManager catManager,
                              IRecipeManager recipeManager)
        {
            this.UserManager = uManager;
            this.TagManager = tagManager;
            this.CategoryManager = catManager;
            this.RecipeManager = recipeManager;
        }

        public IEnumerable<RecipeEntityViewModel> Read()
        {
            return RecipeManager.GetAllRecipes().Select(t => new RecipeEntityViewModel()
            {
                ID = t.ID,
                Title = t.Title,
                Content = t.Content,
                IsApproved = t.IsApproved,
                Author = new AuthorViewModel
                {
                    UserName = t.Author.UserName,
                    ID = t.AuthorId
                },
                Category = new CategoryEntityViewModel
                {
                    ID = t.CategoryID,
                    Name = t.Category.Name,
                    ParentName = t.Category.ParentCategory == null ? "" : t.Category.ParentCategory.Name
                },
                Tags = t.Tags.Select(p => new TagEntityViewModel()
                {
                    ID = p.ID,
                    Name = p.Name
                }).ToList()

            }).ToEnumerable();
        }

        public void Create(RecipeEntityViewModel model)
        {
            //var entry = new Tip(model.Title, model.Content);
            //entry.AuthorId = model.Author.ID;
            //entry.IsApproved = model.IsApproved;
            //TipManager.AddTip(entry);
            //model.ID = entry.ID;
        }

        public void Update(RecipeEntityViewModel model)
        {
            //var entity = TipManager.GetTip(model.ID);
            //if (entity != null)
            //{
            //    entity.Title = model.Title;
            //    entity.Content = model.Content;
            //    entity.IsApproved = model.IsApproved;
            //    entity.Author = UserManager.GetUser(model.Author.ID);
            //    entity.AuthorId = model.Author.ID;

            //    try
            //    {
            //        TipManager.UpdateTip(entity);
            //        model.Author =
            //        new AuthorViewModel { ID = entity.AuthorId, UserName = entity.Author.UserName };
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new ArgumentException("There was problem updating the tip.");
            //    }
            //}
        }

        public void Destroy(RecipeEntityViewModel model)
        {
            var entity = RecipeManager.GetRecipe(model.ID);

            if (entity != null)
            {
                RecipeManager.DeleteRecipe(entity);
            }
        }

        public List<SelectListItem> GetAuthors()
        {
            var result = new SelectList((IEnumerable<User>)UserManager.GetUsers(), "ID", "UserName").ToList();

            return result;
        }

        public List<CategoryEntityViewModel> GetCategories()
        {
            var result = CategoryManager.GetAllCategories().Select(cat => new CategoryEntityViewModel()
            {
                ID = cat.ID,
                Name = cat.Name,
                ParentName = cat.ParentCategory == null ? "" : cat.ParentCategory.Name
            }).ToList();

            return result;
        }
    }
}
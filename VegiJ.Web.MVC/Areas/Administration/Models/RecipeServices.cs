﻿using System;
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
            var choosenCat =
                    CategoryManager.GetAllCategories().AsEnumerable().Where(c => c.Name.Equals(model.Category.Name)).FirstOrDefault();
            if (choosenCat == null)
            {
                var newCategory = new Category(model.Category.Name)
                {
                    ParentCategory = model.Category.ParentName == "Root" ? null :
                    CategoryManager.GetAllCategories().AsEnumerable()
                                   .Where(c => c.Name.Equals(model.Category.ParentName)).FirstOrDefault()
                };
                CategoryManager.AddCategory(newCategory);

            }

            var newTagList = new List<Tag>();
            foreach (var item in model.Tags)
            {
                Tag newTag;
                if (item.ID == Guid.Empty)
                {
                    newTag = new Tag(item.Name);
                    try
                    {
                        TagManager.AddTag(newTag);
                        newTagList.Add(newTag);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    newTag = TagManager.GetTag(item.ID);
                    newTagList.Add(newTag);
                }
            }

            var entry = new Recipe(model.Title, model.Content)
            {
                Author = UserManager.GetUser(model.Author.ID),
                AuthorId = model.Author.ID,
                IsApproved = model.IsApproved,
                Category = choosenCat,
                CategoryID = choosenCat.ID,
                Tags = newTagList
            };

            try
            {
                RecipeManager.AddRecipe(entry);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("There was problem creating the new recipe. " + ex.Message);
            }

            model.ID = entry.ID;
            model.Author = new AuthorViewModel
            {
                ID = entry.AuthorId,
                UserName = entry.Author.UserName
            };
            model.Category.ID = entry.CategoryID;
            model.Tags = entry.Tags.Select(t => new TagEntityViewModel
            {
                ID = t.ID,
                Name = t.Name
            }).ToList();
        }

        public void Update(RecipeEntityViewModel model)
        {
            var entity = RecipeManager.GetRecipe(model.ID);
            if (entity != null)
            {
                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.IsApproved = model.IsApproved;
                entity.Author = UserManager.GetUser(model.Author.ID);
                entity.AuthorId = model.Author.ID;

                // categories
                var choosenCat =
                    CategoryManager.GetAllCategories().AsEnumerable().Where(c => c.Name.Equals(model.Category.Name)).FirstOrDefault();
                if (choosenCat == null)
                {
                    var newCategory = new Category(model.Category.Name)
                    {
                        ParentCategory = model.Category.ParentName == "Root" ? null :
                        CategoryManager.GetAllCategories().AsEnumerable()
                                       .Where(c => c.Name.Equals(model.Category.ParentName)).FirstOrDefault()
                    };
                    CategoryManager.AddCategory(newCategory);

                }
                entity.Category = choosenCat;
                entity.CategoryID = choosenCat.ID;

                //tags
                var newTagList = new List<Tag>();
                foreach (var item in model.Tags)
                {
                    Tag newTag;
                    if (item.ID == Guid.Empty)
                    {
                        newTag = new Tag(item.Name);
                        try
                        {
                            TagManager.AddTag(newTag);
                            newTagList.Add(newTag);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        newTag = TagManager.GetTag(item.ID);
                        newTagList.Add(newTag);
                    }
                }
                entity.Tags.Clear();
                entity.Tags = newTagList;

                try
                {
                    RecipeManager.UpdateRecipe(entity);
                    model.Author =
                    new AuthorViewModel { ID = entity.AuthorId, UserName = entity.Author.UserName };
                    model.Category.ID = entity.CategoryID;
                    model.Tags = entity.Tags.Select(t => new TagEntityViewModel
                    {
                        ID = t.ID,
                        Name = t.Name
                    }).ToList();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("There was problem updating the recipe. " + ex.Message);
                }
            }
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

        public List<TagEntityViewModel> GetTags()
        {
            var result = TagManager.GetAllTags().Select(cat => new TagEntityViewModel()
            {
                ID = cat.ID,
                Name = cat.Name
            }).ToList();

            return result;
        }
    }
}
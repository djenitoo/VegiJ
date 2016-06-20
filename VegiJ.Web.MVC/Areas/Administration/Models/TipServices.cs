using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VegiJ.Web.MVC.Areas.Administration.Models
{
    using System.Web.Mvc;
    using DataAccess;
    using DataAccess.Contracts;
    using Ninject.Infrastructure.Language;

    public class TipServices
    {
        private IUserManager UserManager { get; set; }
        private ITipManager TipManager { get; set; }

        public TipServices(IUserManager uManager, ITipManager tipManager)
        {
            this.UserManager = uManager;
            this.TipManager = tipManager;
        }

        public IEnumerable<TipEntityViewModel> Read()
        {
            return TipManager.GetAllTips().Select(t => new TipEntityViewModel()
            {
                ID = t.ID,
                Title = t.Title,
                Content = t.Content,
                Author = new AuthorViewModel
                {
                    UserName = t.Author.UserName, ID = t.AuthorId
                },
                IsApproved = t.IsApproved
            }).ToEnumerable();
        }

        public void Create(TipEntityViewModel model)
        {
            var entry = new Tip(model.Title, model.Content);
            entry.AuthorId = model.Author.ID;
            entry.IsApproved = model.IsApproved;
            TipManager.AddTip(entry);
            model.ID = entry.ID;
        }

        public void Update(TipEntityViewModel model)
        {
            var entity = TipManager.GetTip(model.ID);
            if (entity != null)
            {
                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.IsApproved = model.IsApproved;
                entity.Author = UserManager.GetUser(model.Author.ID);
                entity.AuthorId = model.Author.ID;
                
                try
                {
                    TipManager.UpdateTip(entity);
                    model.Author =
                    new AuthorViewModel {ID = entity.AuthorId, UserName = entity.Author.UserName};
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("There was problem updating the tip.");
                }
            }

        }

        public void Destroy(TipEntityViewModel model)
        {
            var entity = TipManager.GetTip(model.ID);

            if (entity != null)
            {
                TipManager.DeleteTip(entity);
            }
        }

        public List<SelectListItem> GetAuthors()
        {
            var result = new SelectList((IEnumerable<User>)UserManager.GetUsers(), "ID", "UserName").ToList();

            return result;
        }
    }
}
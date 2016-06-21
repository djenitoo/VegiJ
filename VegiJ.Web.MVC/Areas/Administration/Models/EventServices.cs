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

    public class EventServices
    {
        private IUserManager UserManager { get; set; }
        private IEventManager eventManager { get; set; }

        public EventServices(IUserManager uManager, IEventManager evManager)
        {
            this.UserManager = uManager;
            this.eventManager = evManager;
        }

        public IEnumerable<EventEntityViewModel> Read()
        {
            return eventManager.GetAllEvent().Select(t => new EventEntityViewModel()
            {
                ID = t.ID,
                Name = t.Name,
                Place = t.Place,
                StartTime = t.StartTime,
                Author = new AuthorViewModel
                {
                    UserName = t.Author.UserName,
                    ID = t.AuthorId
                },
                IsApproved = t.IsApproved
            }).ToEnumerable();
        }

        public void Create(EventEntityViewModel model)
        {
            var entry = new Event(model.Name, model.Place);
            entry.StartTime = model.StartTime;
            entry.AuthorId = model.Author.ID;
            entry.IsApproved = model.IsApproved;
            eventManager.AddEvent(entry);
            model.ID = entry.ID;
        }

        public void Update(EventEntityViewModel model)
        {
            var entity = eventManager.GetEvent(model.ID);

            if (entity != null)
            {
                entity.Name = model.Name;
                entity.Place = model.Place;
                entity.IsApproved = model.IsApproved;
                entity.StartTime = model.StartTime;
                entity.Author = UserManager.GetUser(model.Author.ID);
                entity.AuthorId = model.Author.ID;

                try
                {
                    eventManager.UpdateEvent(entity);
                    model.Author =
                    new AuthorViewModel { ID = entity.AuthorId, UserName = entity.Author.UserName };
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("There was problem updating the event.\r\n" + ex.Message);
                }
            }

        }

        public void Destroy(EventEntityViewModel model)
        {
            var entity = eventManager.GetEvent(model.ID);

            if (entity != null)
            {
                eventManager.DeleteEvent(entity);
            }
        }

        public List<SelectListItem> GetAuthors()
        {
            var result = new SelectList((IEnumerable<User>)UserManager.GetUsers(), "ID", "UserName").ToList();

            return result;
        }
    }
}
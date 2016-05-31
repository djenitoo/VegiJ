using System;
using System.Linq;
using VegiJ.DataAccess;
using VegiJ.DataAccess.Contracts;

namespace VegiJ.Logic
{
    public class EventManager : IEventManager
    {
        private IRepository<Event> _eventRepository;

        public EventManager(IRepository<Event> repo)
        {
            this._eventRepository = repo;
        }

        public void AddEvent(Event vegiEvent)
        {
            if (this.EventNameExist(vegiEvent.Name))
            {
                throw new ArgumentException("Event name already exist!");
            }
            _eventRepository.Create(vegiEvent);
        }

        public void DeleteEvent(Event vegiEvent)
        {
            if (!this.EventNameExist(vegiEvent.Name))
            {
                throw new ArgumentException("Event do not exist!");
            }
            _eventRepository.Delete(vegiEvent.ID);
        }

        public IQueryable<Event> GetAllEvent()
        {
            return _eventRepository.Table;
        }

        public Event GetEvent(Guid Id)
        {
            return _eventRepository.GetById(Id);
        }

        public void UpdateEvent(Event vegiEvent)
        {
            if (!this.EventNameExist(vegiEvent.Name))
            {
                throw new ArgumentException("Event do not exist!");
            }
            _eventRepository.Update(vegiEvent);
        }

        private bool EventNameExist(string name)
        {
            return _eventRepository.Table.Any(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

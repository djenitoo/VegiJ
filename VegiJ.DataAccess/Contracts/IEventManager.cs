using System;
using System.Linq;

namespace VegiJ.DataAccess.Contracts
{
    public interface IEventManager
    {
        Event GetEvent(Guid Id);
        void AddEvent(Event vegiEvent);
        void UpdateEvent(Event vegiEvent);
        void DeleteEvent(Event vegiEvent);
        IQueryable<Event> GetAllEvent();
    }
}

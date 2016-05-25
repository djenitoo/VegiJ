using System;
using System.Linq;

namespace VegiJ.DataAccess.Contracts
{
    public interface ITipManager
    {
        Tip GetTag(Guid Id);
        void AddTag(Tip tip);
        void UpdateTag(Tip tip);
        void DeleteTag(Tip tip);
        IQueryable<Tip> GetAllTags();
    }
}

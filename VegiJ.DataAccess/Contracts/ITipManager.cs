using System;
using System.Linq;

namespace VegiJ.DataAccess.Contracts
{
    public interface ITipManager
    {
        Tip GetTip(Guid Id);
        void AddTip(Tip tip);
        void UpdateTip(Tip tip);
        void DeleteTip(Tip tip);
        IQueryable<Tip> GetAllTips();
    }
}

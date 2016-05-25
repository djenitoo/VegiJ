using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegiJ.DataAccess.Contracts
{
    public interface ITagManager
    {
        Tag GetTag(Guid Id);
        void AddTag(Tag category);
        void UpdateTag(Tag category);
        void DeleteTag(Tag category);
        IQueryable<Tag> GetAllTags();
    }
}

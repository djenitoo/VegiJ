using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegiJ.DataAccess;
using VegiJ.DataAccess.Contracts;

namespace VegiJ.Logic
{
    public class TagManager : ITagManager
    {
        private IRepository<Tag> _tagRepository;

        public TagManager(IRepository<Tag> repository)
        {
            _tagRepository = repository;
        }

        public void AddTag(Tag tag)
        {
            if (this.TagNameExist(tag.Name))
            {
                throw new ArgumentException("Tag already exist!");
            }
            _tagRepository.Create(tag);
        }

        public void DeleteTag(Tag tag)
        {
            if (!this.TagNameExist(tag.Name))
            {
                throw new ArgumentException("Tag do not exist!");
            }
            _tagRepository.Delete(tag.ID);
        }

        public IQueryable<Tag> GetAllTags()
        {
            return _tagRepository.Table;
        }

        public Tag GetTag(Guid Id)
        {
            return _tagRepository.GetById(Id);
        }

        public void UpdateTag(Tag tag)
        {
            if (!this.TagNameExist(tag.Name))
            {
                throw new ArgumentException("Tag do not exist!");
            }
            _tagRepository.Update(tag);
        }

        private bool TagNameExist(string name)
        {
            return _tagRepository.Table.Any(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

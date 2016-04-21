namespace VegiJ.Logic
{
    using System;
    using System.Linq;
    using VegiJ.DataAccess;
    using VegiJ.DataAccess.Contracts;

    public class CategoryManager : ICategoryManager
    {
        private IRepository<Category> _categoryRepository;

        public CategoryManager(IRepository<Category> repository)
        {
            this._categoryRepository = repository;
        }

        public void AddCategory(Category category)
        {
            this._categoryRepository.Create(category);
        }

        public void DeleteCategory(Category category)
        {
            this._categoryRepository.Delete(category.ID);
        }

        public IQueryable<Category> GetAllCategories()
        {
            return this._categoryRepository.Table;
        }

        public Category GetCategory(Guid Id)
        {
            return this._categoryRepository.GetById(Id);
        }

        public void UpdateCategory(Category category)
        {
            this._categoryRepository.Update(category);
        }
    }
}

namespace VegiJ.DataAccess.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface ICategoryManager
    {
        Category GetCategory(Guid Id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        IQueryable<Category> GetAllCategories();
    }
}

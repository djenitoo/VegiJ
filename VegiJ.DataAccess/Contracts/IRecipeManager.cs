namespace VegiJ.DataAccess.Contracts
{
    using System;
    using System.Linq;

    public interface IRecipeManager
    {
        Recipe GetRecipe(Guid Id);
        void AddRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void DeleteRecipe(Recipe recipe);
        IQueryable<Recipe> GetAllRecipes();
    }
}

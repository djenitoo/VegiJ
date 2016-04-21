namespace VegiJ.DataAccess
{
    using System;
    using System.Linq;
    using VegiJ.DataAccess.Contracts;

    public class RecipeManager : IRecipeManager
    {
        private IRepository<Recipe> _recipeRepository;

        public RecipeManager(IRepository<Recipe> recipeRepository)
        {
            this._recipeRepository = recipeRepository;
        }

        // TODO: Add recipe exist method
        public void AddRecipe(Recipe recipe)
        {
            this._recipeRepository.Create(recipe);
        }

        public void DeleteRecipe(Recipe recipe)
        {
            this._recipeRepository.Delete(recipe.ID);
        }

        public IQueryable<Recipe> GetAllRecipes()
        {
            return this._recipeRepository.Table;
        }

        public Recipe GetRecipe(Guid Id)
        {
            return this._recipeRepository.GetById(Id);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            this._recipeRepository.Update(recipe);
        }
    }
}

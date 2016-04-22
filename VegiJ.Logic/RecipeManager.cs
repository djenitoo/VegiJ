namespace VegiJ.DataAccess
{
    using System;
    using System.Linq;
    using VegiJ.DataAccess.Contracts;
    // TODO: Implement later reporting for dublicate recipes? or nah
    public class RecipeManager : IRecipeManager
    {
        private IRepository<Recipe> _recipeRepository;

        public RecipeManager(IRepository<Recipe> recipeRepository)
        {
            this._recipeRepository = recipeRepository;
        }

        // TODO: Improve recipe exist method (maybe with regex, etc.)
        public void AddRecipe(Recipe recipe)
        {
            if (CheckIfRecipeExist(recipe))
            {
                return;
            }

            this._recipeRepository.Create(recipe);
        }

        public void DeleteRecipe(Recipe recipe)
        {
            if (!CheckIfRecipeExist(recipe))
            {
                return;
            }

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
            if (CheckIfRecipeExist(recipe))
            {
                return;
            }

            this._recipeRepository.Update(recipe);
        }

        private bool CheckIfRecipeExist(Recipe recipe)
        {
            var thereIsSameTitle = this._recipeRepository.Table.Any(r => r.Title.Equals(recipe.Title, StringComparison.InvariantCultureIgnoreCase));
            var thereIsSameContent = this._recipeRepository.Table.Any(r => r.Content.Equals(recipe.Content, StringComparison.InvariantCultureIgnoreCase));

            return thereIsSameContent || thereIsSameTitle;
        }
    }
}

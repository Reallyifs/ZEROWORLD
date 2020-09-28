using System;
using Terraria;
using Terraria.ModLoader;

namespace ZEROWORLD.Items
{
    static class ZRecipes
    {
        public static void RecipeGroups()
        {
            void Come(string internalName, string displayName, int[] ingredientID, Action<RecipeGroup> action = null)
            {
                RecipeGroup recipeGroup = new RecipeGroup(() => internalName, ingredientID);
                action?.Invoke(recipeGroup);
                RecipeGroup.RegisterGroup(displayName, recipeGroup);
            }
        }
        public static void Recipes()
        {
            void Come(int[][] ingredientID, int[] tileID, int[] resultID, Action<ModRecipe> action = null)
            {
                ModRecipe modRecipe = new ModRecipe(ZEROWORLD.Instance);
                foreach (int[] IngredientID in ingredientID)
                    modRecipe.AddIngredient(IngredientID[0], IngredientID[1]);
                foreach (int TileID in tileID)
                    modRecipe.AddTile(TileID);
                modRecipe.SetResult(resultID[0], resultID[1]);
                action?.Invoke(modRecipe);
                modRecipe.AddRecipe();
            }
        }
    }
}
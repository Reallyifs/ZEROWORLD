using System;
using Terraria.Localization;
using Terraria.ModLoader;
using ZEROWORLD.Files;

namespace ZEROWORLD.Items
{
    public abstract class ZItem : ModItem
    {
        public override void AddRecipes()
        {
            ModRecipe modRecipe = new ModRecipe(mod);
            if (ZEROWORLD.DeveloperMode)
                OwnerRecipes(modRecipe);
            modRecipe.AddRecipe();
        }

        public override sealed void SetStaticDefaults()
        {
            ZItemCollection.AddItemCollection(OwnerListDefault);
            ZList.Cultures.ForEach(delegate (GameCulture culture)
            {
                string displayName = "Default";
                string displayTooltip = "";
                OwnerDisplay(culture, out bool support, ref displayName, ref displayTooltip);
                if (support)
                {
                    if (culture == GameCulture.English)
                    {
                        DisplayName.SetDefault(displayName);
                        Tooltip.SetDefault(displayTooltip);
                        return;
                    }
                    DisplayName.AddTranslation(culture, displayName);
                    Tooltip.AddTranslation(culture, displayTooltip);
                }
            });
        }

        protected abstract int OwnerListDefault(out float level, out Version version, out DateTime date);
        protected abstract void OwnerDisplay(GameCulture culture, out bool support, ref string displayName, ref string displayTooltip);

        protected virtual void OwnerRecipes(ModRecipe modRecipe)
        {
        }
    }
}
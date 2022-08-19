using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ZEROWORLD.Items.ExtendMode
{
    /// <summary>
    /// “小纸条”
    /// </summary>
    public sealed class ChisanaMemo : ZItem
    {
        protected override void OwnerDefaults()
        {
            item.rare = ExtendItemRare;
            item.maxStack = 1;
        }

        protected override void OwnerDisplay(GameCulture culture, ref bool support, ref string displayName, ref string displayTooltip)
        {
            if (culture == GameCulture.Chinese)
            {
                support = true;
                displayName = "“小纸条”";
            }
            else if (culture == GameCulture.English)
            {
                support = true;
                displayName = "\"Small NOTE\"";
            }
        }

        protected override int OwnerListDefault(out float level, out Version version, out DateTime date)
        {
            date = new DateTime(2020, 11, 6);
            level = 0f;
            version = new Version(0, 1, 0, 0);
            return ModContent.ItemType<ChisanaMemo>();
        }

        public override void OnCraft(Recipe recipe) => new RecipeEditor(recipe).DeleteRecipe();
    }
}
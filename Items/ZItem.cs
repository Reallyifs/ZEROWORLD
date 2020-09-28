using System;
using Terraria.Localization;
using Terraria.ModLoader;

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
            OwnerStaticDefault(out string DName, out string CName, out string DTooltip, out string CTooltip);
            DisplayName.SetDefault(DName);
            DisplayName.AddTranslation(GameCulture.Chinese, CName);
            Tooltip.SetDefault(DTooltip);
            Tooltip.AddTranslation(GameCulture.Chinese, CTooltip);
        }

        protected abstract void OwnerListDefault(out int type, out float level, out Version version, out DateTime date);
        protected virtual void OwnerRecipes(ModRecipe modRecipe)
        {
        }
        protected virtual void OwnerStaticDefault(out string DefaultName, out string ChineseName, out string DefaultTooltip,
            out string ChineseTooltip)
        {
            ChineseName = "默认名字";
            DefaultName = "Default";
            ChineseTooltip = DefaultTooltip = "";
        }
    }
}
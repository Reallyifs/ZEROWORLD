using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ZEROWORLD.Files;

namespace ZEROWORLD.Items
{
    public abstract class ZItem : ModItem
    {
        public const int ExtendItemRare = ItemRarityID.LightRed;

        protected int Crit;
        protected int Rare;
        protected int Damage;
        protected int MaxStack;
        protected int[] UseTime;
        protected bool CanEquipped;
        protected bool[] Expert;
        protected float Scale;
        protected float KnockBack;
        protected ZItemClass[] Class;

        internal Dictionary<int, double> Crits;
        internal Dictionary<int, double> Damages;
        internal Dictionary<int, double> KnockBacks;

        public override void AddRecipes()
        {
            ModRecipe modRecipe = new ModRecipe(mod);
            modRecipe.SetResult(this);
            if (ZEROWORLD.DeveloperMode)
                OwnerRecipes(modRecipe);
            modRecipe.AddRecipe();
        }

        public override sealed void GetWeaponCrit(Player player, ref int crit)
        {
            double fixMultiply = 1;
            if (Main.hardMode && Crits.ContainsKey(1))
                fixMultiply *= Crits[1];
            if (Main.expertMode && Crits.ContainsKey(2))
                fixMultiply *= Crits[2];
            if (ZWorld.ExtendMode == 1 && Crits.ContainsKey(3))
                fixMultiply *= Crits[3];
            crit = (int)(crit * fixMultiply);
            OwnerExtraCritFixed(player, ref crit);
        }

        public override sealed void GetWeaponKnockback(Player player, ref float knockback)
        {
            double fixMultiply = 1;
            if (Main.hardMode && KnockBacks.ContainsKey(1))
                fixMultiply *= KnockBacks[1];
            if (Main.expertMode && KnockBacks.ContainsKey(2))
                fixMultiply *= KnockBacks[2];
            if (ZWorld.ExtendMode == 1 && KnockBacks.ContainsKey(3))
                fixMultiply *= KnockBacks[3];
            knockback = (float)(knockback * fixMultiply);
            OwnerExtraKnockBackFixed(player, ref knockback);
        }

        public override sealed void SetDefaults()
        {
            OwnerDefaults();
            item.crit = Crit;
            item.rare = Rare;
            item.scale = Scale;
            item.width = ModContent.GetTexture(Texture).Width;
            item.expert = Expert[0];
            item.damage = Damage;
            item.height = ModContent.GetTexture(Texture).Height;
            item.useTime = UseTime[0];
            item.maxStack = MaxStack;
            item.accessory = CanEquipped;
            item.knockBack = KnockBack;
            item.expertOnly = Expert.Length > 1 ? Expert[1] : Expert[0];
            item.useAnimation = UseTime.Length > 1 ? UseTime[1] : UseTime[0];
            for (int i = 0; i < Class.Length; i++)
            {
                switch (Class[i])
                {
                    case ZItemClass.Magic:
                        item.magic = true;
                        item.noMelee = true;
                        break;
                    case ZItemClass.Melee:
                        item.melee = true;
                        break;
                    case ZItemClass.Ranged:
                        item.ranged = true;
                        item.noMelee = true;
                        break;
                    case ZItemClass.Summon:
                        item.summon = true;
                        item.noMelee = true;
                        break;
                    case ZItemClass.Thrown:
                        item.thrown = true;
                        break;
                }
            }
            CritsMark();
            KnockBacksMark();
        }

        public override sealed void SetStaticDefaults()
        {
            ZItemCollection.AddItemCollection(OwnerListDefault);
            bool support = false;
            string displayName = "Default";
            string displayTooltip = "";
            OwnerDisplay(null, ref support, ref displayName, ref displayTooltip);
            ZList.Cultures.ForEach(delegate (GameCulture culture)
            {
                support = false;
                displayName = "Default";
                displayTooltip = "";
                OwnerDisplay(culture, ref support, ref displayName, ref displayTooltip);
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

        private void CritsMark()
        {
            double hardModeFixed = 1, expertModeFixed = 1, zeroModeFixed = 1;
            OwnerFixedCrit(ref hardModeFixed, ref expertModeFixed, ref zeroModeFixed);
            Crits[1] = hardModeFixed;
            Crits[2] = expertModeFixed;
            Crits[3] = zeroModeFixed;
        }

        private void KnockBacksMark()
        {
            double hardModeFixed = 1, expertModeFixed = 1, zeroModeFixed = 1;
            OwnerFixedKnockBack(ref hardModeFixed, ref expertModeFixed, ref zeroModeFixed);
            KnockBacks[1] = hardModeFixed;
            KnockBacks[2] = expertModeFixed;
            KnockBacks[3] = zeroModeFixed;
        }

        protected abstract void OwnerDisplay(GameCulture culture, ref bool support, ref string displayName, ref string displayTooltip);
        protected abstract int OwnerListDefault(out float level, out Version version, out DateTime date);

        protected virtual void OwnerDefaults()
        {
        }
        protected virtual void OwnerExtraCritFixed(Player player, ref int crit)
        {
        }
        protected virtual void OwnerExtraKnockBackFixed(Player player, ref float knockBack)
        {
        }
        protected virtual void OwnerFixedCrit(ref double hardMode, ref double expertMode, ref double zeroMode)
        {
        }
        protected virtual void OwnerFixedKnockBack(ref double hardMode, ref double expertMode, ref double zeroMode)
        {
        }
        protected virtual void OwnerRecipes(ModRecipe modRecipe)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using ZEROWORLD.Files;
using Terraria.ID;

namespace ZEROWORLD.Items
{
    public abstract class ZItem : ModItem
    {
        public const int ExtendItemRare = 12;

        internal Dictionary<int, double> Crits;
        internal Dictionary<int, double> Damages;
        internal Dictionary<int, double> KnockBacks;

        public override bool Autoload(ref string name) => false;

        public override sealed void GetWeaponCrit(Player player, ref int crit)
        {
            double fixMultiply = 1;
            if (Main.hardMode)
                fixMultiply *= Crits[1];
            if (Main.expertMode)
                fixMultiply *= Crits[2];
            if (ZWorld.ZeroMode)
                fixMultiply *= Crits[3];
            crit = (int)(crit * fixMultiply);
            OwnerExtraCritFixed(player, ref crit);
        }

        public override sealed void GetWeaponKnockback(Player player, ref float knockback)
        {
            double fixMultiply = 1;
            if (Main.hardMode)
                fixMultiply *= KnockBacks[1];
            if (Main.expertMode)
                fixMultiply *= KnockBacks[2];
            if (ZWorld.ZeroMode)
                fixMultiply *= KnockBacks[3];
            knockback = (float)(knockback * fixMultiply);
            OwnerExtraKnockBackFixed(player, ref knockback);
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            double fixAdd = 0, fixMult = 1, fixFlat = 0;
            if (Main.hardMode)
            {
                fixAdd += Damages[0];
                fixMult += Damages[3];
                fixFlat += Damages[6];
            }
            if (Main.expertMode)
            {
                fixAdd += Damages[1];
                fixMult += Damages[4];
                fixFlat += Damages[7];
            }
            if (ZWorld.ZeroMode)
            {
                fixAdd += Damages[2];
                fixMult += Damages[5];
                fixFlat += Damages[8];
            }
            add = (float)(add + fixAdd);
            mult = (float)(mult * fixMult);
            flat = (float)(flat + fixFlat);
            OwnerExtraDamageFixed(player, ref add, ref mult, ref flat);
        }

        public override sealed void SetDefaults()
        {
            OwnerDefaults();
            if (item.rare >= 12)
            {
                item.OwnerItem().ownerRare = item.rare;
                item.rare = ItemRarityID.White;
            }
            CritsMark();
            DamagesMark();
            KnockBacksMark();
        }

        public override sealed void SetStaticDefaults()
        {
            ZItemCollection.AddItemCollection(OwnerListDefault);
            bool support = false;
            string displayName = "[Default Name]";
            string displayTooltip = "";
            OwnerDisplay(null, ref support, ref displayName, ref displayTooltip);
            ZList.cultures.ForEach(delegate (GameCulture culture)
            {
                support = false;
                displayName = "[Default Name]";
                displayTooltip = "";
                OwnerDisplay(culture, ref support, ref displayName, ref displayTooltip);
                if (support)
                {
                    string nameExtra = "", tooltipExtra = "";
                    OwnerDisplayExtra(culture, ref nameExtra, ref tooltipExtra);
                    if (!string.IsNullOrWhiteSpace(nameExtra))
                        displayName += "\n" + nameExtra;
                    if (!string.IsNullOrWhiteSpace(tooltipExtra))
                        displayTooltip += "\n" + tooltipExtra;
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
            Crits = new Dictionary<int, double>();
            double hardModeFixed = 1, expertModeFixed = 1, zeroModeFixed = 1;
            OwnerFixedCrit(ref hardModeFixed, ref expertModeFixed, ref zeroModeFixed);
            Crits[1] = hardModeFixed;
            Crits[2] = expertModeFixed;
            Crits[3] = zeroModeFixed;
        }

        private void DamagesMark()
        {
            Damages = new Dictionary<int, double>();
            double hardModeFixed, expertModeFixed, zeroModeFixed;
            for (int i = 0; i < 9; i += 3)
            {
                hardModeFixed = expertModeFixed = zeroModeFixed = 0;
                bool? currentState;
                switch (i)
                {
                    case 0:
                        currentState = true;
                        break;
                    case 6:
                        currentState = false;
                        break;
                    default:
                        hardModeFixed = expertModeFixed = zeroModeFixed = 1;
                        currentState = null;
                        break;
                }
                OwnerFixedDamage(currentState, ref hardModeFixed, ref expertModeFixed, ref zeroModeFixed);
                Damages[i] = hardModeFixed;
                Damages[i + 1] = expertModeFixed;
                Damages[i + 2] = zeroModeFixed;
            }
        }

        private void KnockBacksMark()
        {
            KnockBacks = new Dictionary<int, double>();
            double hardModeFixed = 1, expertModeFixed = 1, zeroModeFixed = 1;
            OwnerFixedKnockBack(ref hardModeFixed, ref expertModeFixed, ref zeroModeFixed);
            KnockBacks[1] = hardModeFixed;
            KnockBacks[2] = expertModeFixed;
            KnockBacks[3] = zeroModeFixed;
        }

        protected abstract void OwnerDisplay(GameCulture culture, ref bool support, ref string displayName, ref string displayTooltip);
        protected abstract int OwnerListDefault(out float level, out Version version, out DateTime date);

        /// <summary>
        /// <para>建议书写顺序：</para>
        /// <para>宽高，职业（特征属性），伤害，暴击，击退，抛射物，抛射物速度，使用方式，使用时间，使用音效，堆叠数，稀有度，价值，杂项属性</para>
        /// </summary>
        protected virtual void OwnerDefaults()
        {
        }

        protected virtual void OwnerDisplayExtra(GameCulture culture, ref string nameExtra, ref string tooltipExtra)
        {
        }

        protected virtual void OwnerExtraCritFixed(Player player, ref int crit)
        {
        }

        protected virtual void OwnerExtraDamageFixed(Player player, ref float add, ref float mult, ref float flat)
        {
        }

        protected virtual void OwnerExtraKnockBackFixed(Player player, ref float knockBack)
        {
        }

        protected virtual void OwnerFixedCrit(ref double hardMode, ref double expertMode, ref double zeroMode)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentState">True：add时 | Null：mult时 | False：flat时</param>
        /// <param name="hardMode"></param>
        /// <param name="expertMode"></param>
        /// <param name="zeroMode"></param>
        protected virtual void OwnerFixedDamage(bool? currentState, ref double hardMode, ref double expertMode, ref double zeroMode)
        {
        }

        protected virtual void OwnerFixedKnockBack(ref double hardMode, ref double expertMode, ref double zeroMode)
        {
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using ZEROWORLD.Files;
using ZEROWORLD.Files.Interfaces;

namespace ZEROWORLD.Items.Card
{
    public abstract class ZCardItem : ZItem, ILanguageBase
    {
        public void LanguageLoad(List<(string, string, string[])> Texts)
        {
            Texts.Add(("Card.DamageWord", "Card.伤害词", new string[]
            {
                "card",
                "卡牌"
            }));
            Texts.AddRange(new (string, string, string[])[]
            {
                ("Card.DamageWord.Null", "Card.伤害词.无", new string[]
                {
                    "Null",
                    "无"
                }),
                ("Card.DamageWord.Magic", "Card.伤害词.魔法", new string[]
                {
                    "Magic",
                    "魔法"
                }),
                ("Card.DamageWord.Melee", "Card.伤害词.近战", new string[]
                {
                    "Melee",
                    "近战"
                }),
                ("Card.DamageWord.Ranged", "Card.伤害词.远程", new string[]
                {
                    "Ranged",
                    "远程"
                }),
                ("Card.DamageWord.Summon", "Card.伤害词.召唤", new string[]
                {
                    "Summon",
                    "召唤"
                }),
                ("Card.DamageWord.Thrown", "Card.伤害词.投掷", new string[]
                {
                    "Thrown",
                    "投掷"
                })
            });
        }

        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {

        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine currentLine = tooltips.FirstOrDefault((TooltipLine lineT) => lineT.Name == "Damage" && lineT.mod == "Terraria");
            if (currentLine != null)
            {
                string cardPoss = "[";
                ZItemClass[] array = ZFunctions.ToZItemClass(item).ToArray();
                if (array.Length > 0)
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        cardPoss += ZLanguage.Get($"Card.DamageWord.{array[i].GetType().Name}");
                        if (i != array.Length - 1)
                            cardPoss += " | ";
                    }
                }
                else
                    cardPoss += ZLanguage.Get("Card.DamageWord.Null");
                cardPoss += "]";
                string[] splitText = currentLine.text.Split(' ');
                currentLine.text = splitText.First() + $" {ZLanguage.Get("Card.伤害词")} {cardPoss} " + splitText.Last();
            }
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += player.OwnerPlayer().cardDamage[0];
            mult *= player.OwnerPlayer().cardDamage[1];
            flat += player.OwnerPlayer().cardDamage[2];
        }

        protected override sealed void OwnerDefaults()
        {
            OwnerSetDefaultCard();
            item.OwnerItem().cardClass = ZFunctions.ToZItemClass(item);
            item.QuickClassSet(ZItemClass.Magic | ZItemClass.Melee | ZItemClass.Ranged | ZItemClass.Summon | ZItemClass.Thrown, false);
        }

        protected virtual void OwnerSetDefaultCard()
        {
        }
    }
}
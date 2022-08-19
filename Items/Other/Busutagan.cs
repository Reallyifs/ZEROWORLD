using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ZEROWORLD.Buffs.Other;
using ZEROWORLD.Files;
using ZEROWORLD.Files.Interfaces;
using ZEROWORLD.Projectiles.Other;

namespace ZEROWORLD.Items.Other
{
    /// <summary>
    /// 增幅枪
    /// </summary>
    public sealed class Busutagan : ZItem, ILanguageBase
    {
        private int boosterTrueTime;
        private string boosterTime;
        private int[] boosterDamage;
        private int[] boosterDamageTime;

        private Player targetPlayer;
        private NPC targetNPC;

        protected override void OwnerDefaults()
        {
            item.QuickClassSet(ZItemClass.Magic | ZItemClass.Melee | ZItemClass.Ranged | ZItemClass.Summon | ZItemClass.Thrown, false);
            item.damage = 0;
            item.crit = 0;
            item.knockBack = 0f;
            item.shoot = ModContent.ProjectileType<ProjectileBusutadanFurendori>();
            item.shootSpeed = 30f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseTimeAndAnimation(10);
            item.maxStack = 1;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(1);
            item.useTurn = false;
        }

        protected override void OwnerDisplay(GameCulture culture, ref bool support, ref string displayName, ref string displayTooltip)
        {
            if (culture == GameCulture.Chinese)
            {
                support = true;
                displayName = "增幅枪";
                displayTooltip = "你真的想使用它吗？\n" +
                    "——————————\n" +
                    "左键：向附近寻找一名最近的玩家，以{0}的 [魔法] 和 [近战] 伤害向其射击 [{1}] 次，并对其施加 [无风咒焰] buff\n" +
                    "右键：向附近寻找5个最近的敌人，以{0}的 [卡牌] 伤害向其射击 [{1}] 次，并对其施加 [世回之辉] buff\n" +
                    "每次射击间隔 [{0}]";
            }
            else if (culture == GameCulture.English)
            {
                support = true;
                displayName = "Booster gun";
                displayTooltip = "Do you really want to use it?\n" +
                    "----------\n" +
                    "Left: Find the nearest player nearby, shoot at it with {0} [Magic] and [Melee] damage [{1}] times, and apply [Windless Curseflame] buff to it" +
                    "Right: Find the 5 nearest enemies nearby, shoot at them with {0} [Card] damage [{1}] times, and apply [B'acking Darklight] buff to them\n" +
                    "Interval between shots [{0}]";
            }
        }

        protected override int OwnerListDefault(out float level, out Version version, out DateTime date)
        {
            level = 8.7f;
            version = new Version(0, 1, 0, 0);
            date = new DateTime(2021, 1, 2);
            return ModContent.ItemType<Busutagan>();
        }

        public void LanguageLoad(List<(string, string, string[])> Texts)
        {
            Texts.Add(("Items.BoosterGun.Locked", "增幅枪效果未解锁", new string[]
            {
                $"[ Still Locked - Defeat {Language.GetTextValue("NPCName.WallofFlesh")} to unlock! ]",
                $"[ 效果未解锁 - 打败 {Language.GetTextValue("NPCName.WallofFlesh")} 以解锁！ ]"
            }));
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (boosterTrueTime == 0)
            {
                if (player.altFunctionUse != 2)
                {
                    targetPlayer = player.Center.FindNearestPlayer(300);
                    return targetPlayer != null;
                }
                else
                {
                    targetNPC = player.Center.FindNearestNPC(300);
                    return targetNPC != null;
                }
            }
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            new List<string>()
            {
                "Tooltip2",
                "Tooltip3",
                "Tooltip4"
            }.ForEach(delegate (string s)
            {
                TooltipLine currentLine = tooltips.FirstOrDefault((TooltipLine lineThis) => lineThis.Name == s && lineThis.mod == "Terraria");
                if (currentLine != null)
                {
                    switch (s)
                    {
                        case "Tooltip2":
                            currentLine.text = string.Format(currentLine.text, boosterDamage[0], boosterDamageTime[0]);
                            break;
                        case "Tooltip3":
                            if (Main.hardMode)
                                currentLine.text = string.Format(currentLine.text, boosterDamage[1], boosterDamageTime[1]);
                            else
                                currentLine.text = ZLanguage.Get("增幅枪效果未解锁", true);
                            break;
                        case "Tooltip4":
                            currentLine.text = string.Format(currentLine.text, boosterTime);
                            break;
                    }
                }
            });
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                Vector2 tVEC = Vector2.Normalize(targetPlayer.Center - player.Center) * 20;
                for (int i = 0; i < boosterDamageTime[0]; i++)
                {
                    tVEC.RotatedByRandom(0.01);
                    Projectile.NewProjectile(player.Center, tVEC, ModContent.ProjectileType<ProjectileBusutadanTekitaitekiKinsetsuKogeki>(),
                        boosterDamage[0], 0f, player.whoAmI, targetPlayer.whoAmI);
                    Projectile.NewProjectile(player.Center, tVEC, ModContent.ProjectileType<ProjectileBusutadanTekitaitekiMajikku>(),
                        boosterDamage[0], 0f, player.whoAmI, targetPlayer.whoAmI);
                }
                targetPlayer.AddBuff(ModContent.BuffType<BuffMufuNoNoroi>(), boosterDamage[0] * 100);
            }
            else if(Main.hardMode)
            {
                Vector2 tVEC = Vector2.Normalize(targetNPC.Center - player.Center) * 40;
                for (int i = 0; i < boosterDamageTime[1]; i++)
                {
                    tVEC.RotatedByRandom(0.05);
                    Projectile.NewProjectile(player.Center, tVEC, ModContent.ProjectileType<ProjectileBusutadanFurendori>(),
                        boosterDamage[1], 0f, player.whoAmI);
                }
                targetNPC.AddBuff(ModContent.BuffType<BuffMufuNoNoroi>(), boosterDamage[1] * 400);
            }
            AddBoosterTrueTime();
            return false;
        }

        public override void UpdateInventory(Player player)
        {
            boosterTime = BoosterTimeGet();
            boosterDamage = BoosterDamageGet();
            boosterDamageTime = BoosterDamageTimeGet();
            if (boosterTrueTime > 0)
                boosterTrueTime--;
            int[] BoosterDamageTimeGet()
            {
                if (Main.hardMode)
                {
                    if (NPC.downedPlantBoss)
                    {
                        if (NPC.downedMoonlord)
                        {
                            return new int[]
                            {
                                5,
                                60
                            };
                        }
                        return new int[]
                        {
                            4,
                            35
                        };
                    }
                    return new int[]
                    {
                        3,
                        20
                    };
                }
                return new int[]
                {
                    2,
                    0
                };
            }
            int[] BoosterDamageGet()
            {
                if (Main.hardMode)
                {
                    if (NPC.downedPlantBoss)
                    {
                        if (NPC.downedMoonlord)
                        {
                            return new int[]
                            {
                                250,
                                300
                            };
                        }
                        return new int[]
                        {
                            200,
                            100
                        };
                    }
                    return new int[]
                    {
                        100,
                        10
                    };
                }
                return new int[]
                {
                    50,
                    0
                };
            }
            string BoosterTimeGet()
            {
                if (Main.hardMode)
                {
                    if (NPC.downedPlantBoss)
                    {
                        if (NPC.downedMoonlord)
                            return "4 min";
                        return "5 min";
                    }
                    return "7 min";
                }
                return "10 min";
            }
        }

        private void AddBoosterTrueTime() => boosterTrueTime = int.Parse(boosterTime[0].ToString());
    }
}
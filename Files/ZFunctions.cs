using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ZEROWORLD.Items;
using ZEROWORLD.Players;

namespace ZEROWORLD.Files
{
    public static class ZFunctions
    {
        public static Player OwnerPlayer(this Projectile projectile) => Main.player[projectile.owner];

        public static bool HasBuff<T>(this Player player) where T : ModBuff => player.HasBuff(ModContent.BuffType<T>());

        public static NPC FindNearestNPC(this Vector2 position, float distanceMax = 500, Func<NPC, bool> funcBool = null)
        {
            NPC result = null;
            foreach (NPC target in Main.npc)
            {
                if (target.active && ((funcBool != null && funcBool(target)) || funcBool == null))
                {
                    float distance = Vector2.Distance(position, target.Center);
                    if (distance <= distanceMax)
                    {
                        result = target;
                        distanceMax = distance;
                    }
                }
            }
            return result;
        }

        public static Player FindNearestPlayer(this Vector2 position, float distanceMax = 500, Func<Player, bool> funcBool = null)
        {
            Player player = null;
            foreach (Player target in Main.player)
            {
                if (target.active && !target.immune && ((funcBool != null && funcBool(player)) || funcBool == null))
                {
                    float distance = Vector2.Distance(position, target.Center);
                    if (distance <= distanceMax)
                    {
                        player = target;
                        distanceMax = distance;
                    }
                }
            }
            return player;
        }

        public static ZPlayer OwnerPlayer(this Player player) => player.GetModPlayer<ZPlayer>();

        public static ZGlobalItem OwnerItem(this Item item) => item.GetGlobalItem<ZGlobalItem>();

        public static void QuickClassSet(this Item item, ZItemClass allClasses, bool setTo)
        {
            ZList.itemClasses.ForEach(delegate (ZItemClass checkClass)
            {
                if (allClasses.HasFlag(checkClass))
                {
                    switch (checkClass)
                    {
                        case ZItemClass.Magic:
                            item.magic = setTo;
                            break;
                        case ZItemClass.Melee:
                            item.melee = setTo;
                            break;
                        case ZItemClass.Ranged:
                            item.ranged = setTo;
                            break;
                        case ZItemClass.Summon:
                            item.summon = setTo;
                            break;
                        case ZItemClass.Thrown:
                            item.thrown = setTo;
                            break;
                    }
                }
            });
        }

        public static ZItemClass[] ToArray(this ZItemClass allClasses)
        {
            List<ZItemClass> itemClasses = new List<ZItemClass>();
            ZList.itemClasses.ForEach(delegate (ZItemClass checkClass)
            {
                if (checkClass != ZItemClass.Default && allClasses.HasFlag(checkClass))
                    itemClasses.Add(checkClass);
            });
            return SortReturn(itemClasses);
            T[] SortReturn<T>(List<T> result)
            {
                result.Sort();
                return result.ToArray();
            }
        }

        public static string TypeName(this object obj) => obj.GetType().Name;

        public static bool DuringNewYear()
        {
            return (DateTime.Now.Month == 1 && DateTime.Now.Day.Between(1, 10)) ||
                (DateTime.Now.Month == 12 && DateTime.Now.Day.Between(21, 31));
        }

        /// <exception cref="ArgumentException">"pre" cannot be greater than "post"</exception>
        public static bool Between(this int number, int pre = int.MinValue, int post = int.MaxValue)
        {
            if (pre > post)
                throw new ArgumentException("\"pre\" cannot be greater than \"post\"");
            return post >= number && number >= pre;
        }

        public static void HealLife(this Player player, int lifeAmount, bool broadcast = true)
        {
            player.statLife += lifeAmount;
            if (lifeAmount != 0)
                player.HealEffect(lifeAmount, broadcast);
        }

        public static void HealMana(this Player player, int manaAmount)
        {
            player.statLife += manaAmount;
            if (manaAmount != 0)
                player.ManaEffect(manaAmount);
        }

        public static void Heal(this Player player, int life = 0, int mana = 0)
        {
            player.HealLife(life);
            player.HealMana(mana);
        }

        public static void UseTimeAndAnimation(this Item item, int number)
        {
            item.useTime = number;
            item.useAnimation = number;
        }

        public static void TryAction(this Action tryAction, Action<Exception> exceptionAction = null, Action finallyAction = null)
        {
            try
            {
                tryAction?.Invoke();
            }
            catch (Exception exception)
            {
                exceptionAction?.Invoke(exception);
            }
            finally
            {
                finallyAction?.Invoke();
            }
        }

        public static ZItemInfo ZItemInfoSetDefaults(int type, float level, Version version, DateTime date, bool noMatCheck = false)
        {
            ZItemInfo info = new ZItemInfo(type, level, version, date);
            info.SetDefaultByType(noMatCheck);
            return info;
        }

        public static Type GetInterface<T>(this Type type, bool ignoreCase) => type.GetInterface(typeof(T).Name, ignoreCase);

        public static Type GetInterface<T>(this Type type) => type.GetInterface(typeof(T).Name);

        public static bool Contains<T>(this T[] array, T item) => array.ToList().Contains(item);

        public static void NewText(this string text, byte R = 255, byte G = 255, byte B = 255)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                Main.NewText(text, R, G, B);
        }

        public static void NewText(this string text, Color textColor)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                Main.NewText(text, textColor);
        }

        public static Item ItemSetDefaults(int type, bool noMatCheck = false)
        {
            Item item = new Item();
            item.SetDefaults(type, noMatCheck);
            return item;
        }

        public static ZItemClass ToZItemClass(Item item)
        {
            ZItemClass itemClasses = ZItemClass.Default;
            if (item.magic)
                itemClasses |= ZItemClass.Magic;
            if (item.melee)
                itemClasses |= ZItemClass.Melee;
            if (item.ranged)
                itemClasses |= ZItemClass.Ranged;
            if (item.summon)
                itemClasses |= ZItemClass.Summon;
            if (item.thrown)
                itemClasses |= ZItemClass.Thrown;
            return itemClasses;
        }

        public static void ContainsThenInsert<T>(this List<T> list, T findItem, T insertItem, int indexAdd = 0)
        {
            int index = list.IndexOf(findItem);
            if (index != 1)
                list.Insert(index + indexAdd, insertItem);
        }

        public static void ForEach<T>(this T[] array, Action<T> action, bool sort = false, T[] pre = null, T[] post = null)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (action == null)
                throw new ArgumentNullException("action");
            List<T> newList = array.ToList();
            if (sort)
            {
                newList.Sort(Comparer<T>.Create(delegate (T item1, T item2)
                {
                    if (item1 == null || item2 == null)
                    {
                        if (item2 == null)
                            return -1;
                        if (item1 == null)
                            return 1;
                        return 0;
                    }
                    return item1.TypeName().CompareTo(item2.TypeName());
                }));
            }
            if (pre != null)
            {
                for (int i = pre.Length - 1; i > -1; i--)
                {
                    if (newList.Contains(pre[i]))
                    {
                        newList.Remove(pre[i]);
                        newList.Insert(0, pre[i]);
                    }
                }
            }
            if (post != null)
            {
                for (int i = post.Length - 1; i > -1; i--)
                {
                    if (newList.Contains(post[i]))
                    {
                        newList.Remove(post[i]);
                        newList.Add(post[i]);
                    }
                }
            }
            newList.ForEach(action);
        }

        public static T CreateDelegate<T>(this MethodInfo info) where T : Delegate => (T)info.CreateDelegate(typeof(T), null);

        public static bool IsSubclassOf<T>(this Type type) => type.IsSubclassOf(typeof(T));

        public static bool Is(this string text, params string[] array)
        {
            foreach (string line in array)
            {
                if (text == line)
                    return true;
            }
            return false;
        }
    }
}
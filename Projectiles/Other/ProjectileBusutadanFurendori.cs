using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ZEROWORLD.Projectiles.Other
{
    /// <summary>
    /// 增幅子弹 - 友方
    /// </summary>
    public class ProjectileBusutadanFurendori : ModProjectile
    {
        private byte tReduce;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Booster Bullet");
            DisplayName.AddTranslation(GameCulture.Chinese, "增幅子弹");
        }

        public override void SetDefaults()
        {
            projectile.aiStyle = -1;
            projectile.magic = true;
            projectile.scale = 1.5f;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.maxPenetrate = 1;
            projectile.ignoreWater = false;
            projectile.tileCollide = false;
            tReduce = 30;
        }

        public override void AI()
        {
            NPC target = Main.npc[(int)projectile.ai[0]];
            if (target.active)
            {
                Vector2 tVEC = Vector2.Normalize(target.Center - projectile.Center) * 40;
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, DustID.t_Crystal, (tVEC / -41).X, (tVEC / -41).Y);
                if (tReduce > 0)
                    tReduce--;
                projectile.velocity = (projectile.velocity * tReduce + tVEC) / (tReduce + 1);
            }
            else
                projectile.timeLeft = 1;
        }
    }
}
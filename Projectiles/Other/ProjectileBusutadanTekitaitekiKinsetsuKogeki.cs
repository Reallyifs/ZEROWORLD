using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using ZEROWORLD.Files;

namespace ZEROWORLD.Projectiles.Other
{
    /// <summary>
    /// 增幅子弹 - 敌对（近战）
    /// </summary>
    public class ProjectileBusutadanTekitaitekiKinsetsuKogeki : ModProjectile
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
            projectile.melee = true;
            projectile.scale = 1.5f;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.maxPenetrate = 1;
            projectile.ignoreWater = false;
            projectile.tileCollide = false;
            tReduce = 30;
        }

        public override void AI()
        {
            Player player = projectile.OwnerPlayer();
            if (projectile.getRect().Intersects(player.getRect()))
                player.immune = true;
            player = Main.player[(int)projectile.ai[0]];
            if (player.active)
            {
                Vector2 tVEC = Vector2.Normalize(player.Center - projectile.Center) * 20;
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, 174, (tVEC / -21).X, (tVEC / -21).Y);
                if (tReduce > 0)
                    tReduce--;
                projectile.velocity = (projectile.velocity * tReduce + tVEC) / (tReduce + 1);
            }
            else
                projectile.timeLeft = 1;
        }
    }
}
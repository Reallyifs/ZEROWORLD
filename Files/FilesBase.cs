using Microsoft.Xna.Framework.Graphics;

namespace ZEROWORLD.Files
{
    public class FilesBase
    {
        public virtual void Load()
        {
        }

        public virtual void Unload()
        {
        }

        public virtual void TickDraw(SpriteBatch spriteBatch)
        {
        }

        public virtual void PostDraw(SpriteBatch spriteBatch)
        {
        }
    }
}
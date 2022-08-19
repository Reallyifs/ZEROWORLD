using Terraria.ModLoader;

namespace ZEROWORLD.Players
{
    public partial class ZPlayer : ModPlayer
    {
        public int[] cardDamage;
        public int[] cardCrit;
        public float[] cardKnockBack;

        private void CardPlayerReset()
        {
            cardDamage = new int[]
            {
                0,
                1
            };
            cardCrit = new int[]
            {
                0,
                1
            };
            cardKnockBack = new float[]
            {
                0f,
                1f
            };
        }
    }
}
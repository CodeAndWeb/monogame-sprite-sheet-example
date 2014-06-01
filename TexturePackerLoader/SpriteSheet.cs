namespace TexturePackerLoader
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework.Graphics;

    public class SpriteSheet
    {
        private readonly IDictionary<string, Sprite> spriteList;

        public SpriteSheet(Texture2D texture, IDictionary<string, Sprite> spriteList)
        {
            this.spriteList = spriteList;
            this.Texture = texture;
        }

        public Texture2D Texture { get; private set; }

        public Sprite Sprite(string sprite)
        {
            return this.spriteList[sprite];
        }
    }
}
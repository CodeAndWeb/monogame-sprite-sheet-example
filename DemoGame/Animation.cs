namespace TexturePackerMonoGameDemoCommon
{
    using System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Animation
    {
        public Animation(Vector2 characterVelocity, TimeSpan timePerFrame, SpriteEffects effect, string[] sprites)
        {
            this.Sprites = sprites;
            this.TimePerFrame = timePerFrame;
            this.Effect = effect;
            this.CharacterVelocity = characterVelocity;
        }

        /// <summary>
        /// Measured in pixels per second
        /// </summary>
        public Vector2 CharacterVelocity { get; private set; }

        public SpriteEffects Effect { get; private set; }

        public TimeSpan TimePerFrame { get; private set; }

        public string[] Sprites { get; private set; }
    }
}

using TexturePackerLoader;

namespace TexturePackerMonoGameDemoCommon
{
    using System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class AnimationManager
    {
        private readonly Animation[] animations;

        private TimeSpan previousFrameChangeTime = TimeSpan.Zero;

        private TimeSpan previousMovementTime = TimeSpan.Zero;

        private SpriteSheet spriteSheet;

        private Vector2 currentPosition;

        public AnimationManager(SpriteSheet spriteSheet, Vector2 initialPosition, Animation[] animations)
        {
            this.spriteSheet = spriteSheet;
            this.animations = animations;
            this.currentPosition = initialPosition;
        }

        public SpriteFrame CurrentSprite { get; private set; }

        public SpriteEffects CurrentSpriteEffects { get; private set; }

        public int CurrentFrame { get; private set; }

        public int CurrentAnimation { get; private set; }

        public Vector2 CurrentPosition 
        { 
            get { return this.currentPosition; } 
        }

        public void Update(GameTime gameTime) 
        {
            var nowTime = gameTime.TotalGameTime;
            var dtFrame = nowTime - this.previousFrameChangeTime;
            var dtPosition = nowTime - this.previousMovementTime;

            var animation = this.animations[this.CurrentAnimation];

            if (dtFrame >= animation.TimePerFrame)
            {
                this.previousFrameChangeTime = nowTime;
                this.CurrentFrame++;

                if (this.CurrentFrame >= animation.Sprites.Length)
                {
                    this.CurrentFrame = 0;
                    if (++this.CurrentAnimation >= this.animations.Length)
                    {
                        this.CurrentAnimation = 0;
                    }

                    animation = this.animations[this.CurrentAnimation];
                }

                this.CurrentSpriteEffects = animation.Effect;
            }

            this.CurrentSprite = this.spriteSheet.Sprite(animation.Sprites[this.CurrentFrame]);

            this.currentPosition.X += animation.CharacterVelocity.X * dtPosition.Ticks / TimeSpan.TicksPerSecond;
            this.currentPosition.Y += animation.CharacterVelocity.Y * dtPosition.Ticks / TimeSpan.TicksPerSecond;
            this.previousMovementTime = nowTime;        
        }
    }
}

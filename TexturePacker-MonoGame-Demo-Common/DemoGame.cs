namespace TexturePackerMonoGameDemoCommon
{
    using System;
    using System.Collections.Generic;

    using TexturePackerLoader;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DemoGame : Game
    {
        private readonly TimeSpan timePerFrame = TimeSpan.FromSeconds(1f/30f);

        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        private SpriteSheet spriteSheet;

        private SpriteRender spriteRender;

        private Sprite backgroundSprite;

        private Vector2 centreScreen;

        private AnimationManager characterAnimationManager;

        public DemoGame()
        {
            this.graphics = new GraphicsDeviceManager(this);
#if NETFX_CORE
            this.graphics.PreferredBackBufferWidth = 1024;
            this.graphics.PreferredBackBufferHeight = 768;
#endif
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.spriteRender = new SpriteRender(this.spriteBatch);

            var spriteSheetLoader = new SpriteSheetLoader(this.Content);
            this.spriteSheet = spriteSheetLoader.Load("CapGuyDemo.png");
            this.backgroundSprite = this.spriteSheet.Sprite(TexturePackerMonoGameDefinitions.CapGuyDemo.Background);
            this.centreScreen = new Vector2 (this.GraphicsDevice.Viewport.Width / 2, this.GraphicsDevice.Viewport.Height / 2);

            this.InitialiseAnimationManager();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            this.characterAnimationManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();

            // Draw the background
            this.spriteRender.Draw(this.spriteSheet.Texture, this.backgroundSprite, this.centreScreen);

            // Draw character on screen
            this.spriteRender.Draw(
                this.spriteSheet.Texture, 
                this.characterAnimationManager.CurrentSprite, 
                this.characterAnimationManager.CurrentPosition, 
                Color.White, 
                this.characterAnimationManager.CurrentSpriteEffects);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        private void InitialiseAnimationManager()
        {
            #if __IOS__
            var scale = MonoTouch.UIKit.UIScreen.MainScreen.Scale;
            var characterStartPosition = new Vector2(250 * scale, 530 * scale);
            var characterVelocityPixelsPerSecond = 125 * (int)scale;
            #else
            var characterStartPosition = new Vector2(250, 530);
            var characterVelocityPixelsPerSecond = 125;
            #endif

            var turnSprites = new [] {
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0001,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0002,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0003,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0004,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0005,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0006,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0007,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0008,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0009,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0010,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0011,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0012
            };

            var walkSprites = new [] {
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0001,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0002,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0003,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0004,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0005,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0006,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0007,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0008,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0009,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0010,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0011,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0012,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0013,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0014,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0015,
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_walk_0016,
            };
                
            var animationWalkRight = new Animation(new Vector2(characterVelocityPixelsPerSecond, 0), this.timePerFrame, SpriteEffects.None, walkSprites);
            var animationWalkLeft = new Animation(new Vector2(-characterVelocityPixelsPerSecond, 0), this.timePerFrame, SpriteEffects.FlipHorizontally, walkSprites);
            var animationTurnRightToLeft = new Animation(Vector2.Zero, this.timePerFrame, SpriteEffects.None, turnSprites);
            var animationTurnLeftToRight = new Animation(Vector2.Zero, this.timePerFrame, SpriteEffects.FlipHorizontally, turnSprites);

            var animations = new[] 
            { 
               animationWalkRight, animationWalkRight, animationWalkRight, animationWalkRight, animationWalkRight, animationWalkRight,
               animationTurnRightToLeft,
               animationWalkLeft, animationWalkLeft, animationWalkLeft, animationWalkLeft, animationWalkLeft, animationWalkLeft, 
               animationTurnLeftToRight
            };

            this.characterAnimationManager = new AnimationManager (this.spriteSheet, characterStartPosition, animations);
        }
    }
}

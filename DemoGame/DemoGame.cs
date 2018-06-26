using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TexturePackerLoader;

namespace TexturePackerMonoGameDemoCommon
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DemoGame : Game
    {
        private readonly TimeSpan timePerFrame = TimeSpan.FromSeconds(1f / 30f);

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SpriteSheet spriteSheet;
        private SpriteRender spriteRender;
        private SpriteFrame backgroundSprite;
        private Vector2 centreScreen;
        private AnimationManager characterAnimationManager;

        public DemoGame()
        {
            graphics = new GraphicsDeviceManager(this);
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteRender = new SpriteRender(spriteBatch);

            var spriteSheetLoader = new SpriteSheetLoader(Content, GraphicsDevice);
            spriteSheet = spriteSheetLoader.Load("CapGuyDemo.png");
            backgroundSprite = spriteSheet.Sprite(TexturePackerMonoGameDefinitions.CapGuyDemo.Background);
            centreScreen = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            InitialiseAnimationManager();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            characterAnimationManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            // Draw the background
            spriteRender.Draw(backgroundSprite, centreScreen);

            // Draw character on screen
            spriteRender.Draw(
                characterAnimationManager.CurrentSprite,
                characterAnimationManager.CurrentPosition,
                Color.White, 0, 1,
                characterAnimationManager.CurrentSpriteEffects);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void InitialiseAnimationManager()
        {
            var characterStartPosition = new Vector2(GraphicsDevice.Viewport.Width / 10, GraphicsDevice.Viewport.Height * 0.8f);
            var characterVelocityPixelsPerSecond = GraphicsDevice.Viewport.Width / 4;

            var turnSprites = new[] {
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

            var walkSprites = new[] {
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

            var animationWalkRight = new Animation(new Vector2(characterVelocityPixelsPerSecond, 0), timePerFrame, SpriteEffects.None, walkSprites);
            var animationWalkLeft = new Animation(new Vector2(-characterVelocityPixelsPerSecond, 0), timePerFrame, SpriteEffects.FlipHorizontally, walkSprites);
            var animationTurnRightToLeft = new Animation(Vector2.Zero, timePerFrame, SpriteEffects.None, turnSprites);
            var animationTurnLeftToRight = new Animation(Vector2.Zero, timePerFrame, SpriteEffects.FlipHorizontally, turnSprites);

            var animations = new[]
            {
               animationWalkRight, animationWalkRight, animationWalkRight, animationWalkRight, animationWalkRight, animationWalkRight,
               animationTurnRightToLeft,
               animationWalkLeft, animationWalkLeft, animationWalkLeft, animationWalkLeft, animationWalkLeft, animationWalkLeft,
               animationTurnLeftToRight
            };

            characterAnimationManager = new AnimationManager(spriteSheet, characterStartPosition, animations);
        }
    }
}

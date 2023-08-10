using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TexturePackerLoader;

namespace TexturePacker_MonoGame_Demo
{
    public class DemoGame : Game
    {
        private readonly TimeSpan timePerFrame = TimeSpan.FromSeconds(1f / 20f);

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SpriteSheet spriteSheet;
        private SpriteRender spriteRender;
        private SpriteFrame backgroundSprite;
        private Vector2 centreScreen;
        private AnimationManager characterAnimationManager;
        Matrix globalTransformation;

        public DemoGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteRender = new SpriteRender(spriteBatch);

            var spriteSheetLoader = new SpriteSheetLoader(Content, GraphicsDevice);
            spriteSheet = spriteSheetLoader.Load(GraphicsDevice.Viewport.Width > 1024 ? "CapGuyDemo@2x.png"
                                                                                      : "CapGuyDemo.png");
            backgroundSprite = spriteSheet.Sprite(TexturePackerMonoGameDefinitions.CapGuyDemo.Background);
            centreScreen = new Vector2(backgroundSprite.Size.X / 2, backgroundSprite.Size.Y / 2);

            globalTransformation = Matrix.CreateScale(GraphicsDevice.Viewport.Width / backgroundSprite.Size.X);
            InitialiseAnimationManager();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            characterAnimationManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(blendState: BlendState.NonPremultiplied, transformMatrix: globalTransformation);

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
            var characterStartPosition = new Vector2(backgroundSprite.Size.X / 4, backgroundSprite.Size.Y * 0.7f);
            var characterVelocityPixelsPerSecond = backgroundSprite.Size.X / 9;

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
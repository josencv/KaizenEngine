using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KaizenEngine.GameInput;
using KaizenEngine.Sprites;

namespace KaizenEngine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TestGame : Game
    {
        private GraphicsDeviceManager graphics;
        private GameInputManager inputManager;
        private SpriteBatch spriteBatch;
        private SpriteSheetLoader spriteSheetLoader;
        private SpriteRenderer spriteRenderer;

        private Texture2D texture;
        private Vector2 position;
        private float speed;
        private SpriteSheet sheet;

        public TestGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            inputManager = new GameInputManager();
            spriteSheetLoader = new SpriteSheetLoader(Content);

            position = Vector2.Zero;
            speed = 5.0f;
        }

        private void Move(float x, float y) {
            float speedModule = (float)Math.Sqrt(x * x + y * y);
            if (speedModule > 0)
            {
                position.X += x * speed / speedModule;
                position.Y -= y * speed / speedModule;    // Y axis is inverted in XNA
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            inputManager.InitializeGameInputs();
            GameInput.GameInput controller = inputManager.GetGameInput(PlayerInputNumber.Player1);
            controller.PlayerMoveSignal += Move;

            texture = new Texture2D(this.GraphicsDevice, 100, 100);
            Color[] colorData = new Color[100 * 100];
            for (int i = 0; i < colorData.Length; i++)
            {
                colorData[i] = Color.Red;
            }
            texture.SetData<Color>(colorData);
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
            spriteRenderer = new SpriteRenderer(spriteBatch);

            sheet = spriteSheetLoader.Load(@"Sprites\BlackRabite");
            // TODO: use this.Content to load your game content here
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (position.X > this.GraphicsDevice.Viewport.Width)
                position.X = -1 * texture.Width;

            if (position.X < -1 * texture.Width)
                position.X = this.GraphicsDevice.Viewport.Width;

            if (position.Y > this.GraphicsDevice.Viewport.Height)
                position.Y = -1 * texture.Height;

            if (position.Y < -1 * texture.Height)
                position.Y = this.GraphicsDevice.Viewport.Height;

            inputManager.ProcessGameInputs();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteFrame sprite = sheet.GetSprite("rabite_01");

            spriteBatch.Begin();
            spriteRenderer.Draw(sprite, position, scale: 3);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

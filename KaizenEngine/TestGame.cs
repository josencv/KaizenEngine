using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KaizenEngine.GameInput;
using KaizenEngine.Sprites;
using KaizenEngine.Animations;
using KaizenEngine.States;

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

        private Vector2 position;
        private float speed;
        private SpriteSheet sheet;
        private DrawableStateMachine stateMachine;

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
                stateMachine.SetFloatField("movementX", x);
                stateMachine.SetFloatField("movementY", y);
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
            GameInput.GameInput controller = inputManager.GetGameInput(GameInput.PlayerIndex.Player1);
            controller.PlayerMoveSignal += Move;
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

            stateMachine = new DrawableStateMachine(spriteRenderer);
            stateMachine.RegisterBoolField("attack");
            stateMachine.RegisterFloatField("movementX");
            stateMachine.RegisterFloatField("movementY");

            TransitionCondition condition1 = new TransitionCondition(StateMachineFieldType.Bool, ConditionOperator.True, "attack", 1);
            TransitionCondition condition2 = new TransitionCondition(StateMachineFieldType.Bool, ConditionOperator.False, "attack", 0);

            sheet = spriteSheetLoader.Load(@"Sprites\BlackRabite");

            List<SpriteFrame> spriteList = new List<SpriteFrame>();
            spriteList.Add(sheet.GetSprite(TexturePackerMonoGameDefinitions.Test.Rabite_03));
            spriteList.Add(sheet.GetSprite(TexturePackerMonoGameDefinitions.Test.Rabite_04));
            spriteList.Add(sheet.GetSprite(TexturePackerMonoGameDefinitions.Test.Rabite_05));
            spriteList.Add(sheet.GetSprite(TexturePackerMonoGameDefinitions.Test.Rabite_06));
            Animation2D moveDown = new Animation2D(spriteList, 1.0f);

            spriteList = new List<SpriteFrame>();
            spriteList.Add(sheet.GetSprite(TexturePackerMonoGameDefinitions.Test.Rabite_01));
            spriteList.Add(sheet.GetSprite(TexturePackerMonoGameDefinitions.Test.Rabite_10));
            spriteList.Add(sheet.GetSprite(TexturePackerMonoGameDefinitions.Test.Rabite_11));
            spriteList.Add(sheet.GetSprite(TexturePackerMonoGameDefinitions.Test.Rabite_12));
            Animation2D moveLeft = new Animation2D(spriteList, 1.0f);

            spriteList = new List<SpriteFrame>();
            spriteList.Add(sheet.GetSprite(TexturePackerMonoGameDefinitions.Test.Rabite_02));
            spriteList.Add(sheet.GetSprite(TexturePackerMonoGameDefinitions.Test.Rabite_07));
            spriteList.Add(sheet.GetSprite(TexturePackerMonoGameDefinitions.Test.Rabite_08));
            spriteList.Add(sheet.GetSprite(TexturePackerMonoGameDefinitions.Test.Rabite_09));
            Animation2D moveUp = new Animation2D(spriteList, 1.0f);

            DrawableState moving = new DrawableState(AnimationSelectionType.Directional);
            moving.AddAnimation(moveLeft);
            moving.AddAnimation(moveUp);
            moving.AddAnimation(moveLeft);
            moving.AddAnimation(moveDown);
            moving.AddAnimationSelectionField(stateMachine.Fields["movementX"]);
            moving.AddAnimationSelectionField(stateMachine.Fields["movementY"]);
            DrawableState state2 = new DrawableState(moveLeft);
            StateTransition transition1 = new StateTransition(moving, state2);
            StateTransition transition2 = new StateTransition(state2, moving);
            
            transition1.AddCondition(condition1);
            transition2.AddCondition(condition2);
            moving.AddTransition(transition1);
            state2.AddTransition(transition2);

            stateMachine.AddState(moving);
            stateMachine.AddState(state2);
            stateMachine.SetEntryPoint(moving);
            stateMachine.Start();

            //animator = new Animator(spriteRenderer);
            //animator.Play(animation, true);
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

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                stateMachine.SetBoolField("attack", true);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                stateMachine.SetBoolField("attack", false);
            }

            //if (position.X > this.GraphicsDevice.Viewport.Width)
            //    position.X = -1 * texture.Width;

            //if (position.X < -1 * texture.Width)
            //    position.X = this.GraphicsDevice.Viewport.Width;

            //if (position.Y > this.GraphicsDevice.Viewport.Height)
            //    position.Y = -1 * texture.Height;

            //if (position.Y < -1 * texture.Height)
            //    position.Y = this.GraphicsDevice.Viewport.Height;

            inputManager.ProcessGameInputs();
            stateMachine.Update(gameTime, position);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            stateMachine.Draw(position);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

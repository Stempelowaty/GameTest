using Burucki.Utils;
using Burucki.Menu;   
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Burucki
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameStateMachine _stateMachine;

        private static Game1 _instance;
        public static Game1 Instance => _instance;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _instance = this;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _stateMachine = new GameStateMachine();
            base.Initialize();
        }

        protected override void LoadContent()
        {
     
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load all global resources once
            GlobalResources.LoadContent(this);

            // Initialize state machine
            _stateMachine = new GameStateMachine();
            _stateMachine.ChangeState(new MenuState(_stateMachine));
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _stateMachine.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _stateMachine.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void UpdateResolution(int width, int height)
        {
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;

            _graphics.ApplyChanges();

            GlobalResources.ScreenWidth = width;
            GlobalResources.ScreenHeight = height;
        }

        public void ToggleFullscreen(bool isFullscreen)
        {
            _graphics.IsFullScreen = isFullscreen;
            _graphics.ApplyChanges();
            GlobalResources.IsFullScreen = isFullscreen;
        }
    }
}

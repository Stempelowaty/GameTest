using Burucki.Utils;
using Burucki.Menu;   
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Burucki
{
    public class GameInstance : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameStateMachine _stateMachine;

        private static GameInstance _instance;
        public static GameInstance Instance => _instance;

        private RenderTarget2D _renderTarget;
        private int baseWidth = 1280; 
        private int baseHeight = 720;
        public float _scaleFactorX;
        public float _scaleFactorY;


        public GameInstance()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _instance = this;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _stateMachine = new GameStateMachine();
            UpdateResolution(baseWidth, baseHeight);
            base.Initialize();
        }

        protected override void LoadContent()
        {
     
            _renderTarget = new RenderTarget2D(GraphicsDevice, baseWidth, baseHeight);
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load all global resources once
            GlobalResources.LoadContent(this);
            GlobalResources.UpdateScreenSize(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

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
 
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.Green);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // Prevents blurring
            _stateMachine.Draw(_spriteBatch);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.End();
        }

        public void UpdateResolution(int width, int height)
        {
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;

            _graphics.ApplyChanges();
            GlobalResources.UpdateScreenSize(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
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

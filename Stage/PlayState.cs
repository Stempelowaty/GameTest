using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burucki.Menu;
using Burucki.Utils;

namespace Burucki.Stage
{
    public class PlayState : IGameState
    {
        private GameStateMachine _stateMachine;
        private Entity _player;

        public PlayState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            /*
            Texture2D playerTexture = new Texture2D(GlobalResources.GraphicsDevice, 50, 50);
            Color[] dataPlayer = new Color[50 * 50];
            for (int i = 0; i < dataPlayer.Length; i++) dataPlayer[i] = Color.Blue;
            playerTexture.SetData(dataPlayer);
            */            
            
            _player = new Entity(GlobalResources.entityTexture, new Vector2(200, 200), new Vector2(50, 50));

        }

        public void Exit()
        {
            Console.WriteLine("Exited Play State");
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _stateMachine.ChangeState(new MenuState(_stateMachine));
            }
            if (InputManager.IsKeyDown(Keys.W)) _player.Move(0, -2);
            if (InputManager.IsKeyDown(Keys.S)) _player.Move(0, 2);
            if (InputManager.IsKeyDown(Keys.A)) _player.Move(-2, 0);
            if (InputManager.IsKeyDown(Keys.D)) _player.Move(2, 0);

            _player.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _player.Draw(spriteBatch);
        }
    }

}

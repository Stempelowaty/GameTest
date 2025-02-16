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

        private RotatableRectangle _rotRect;
        private Texture2D _rectTexture;

        public PlayState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            Console.WriteLine("Entered Play State");
            _rotRect = new RotatableRectangle(new Vector2(300, 100), new Vector2(100, 50), MathHelper.ToRadians(45));

            _rectTexture = new Texture2D(GlobalResources.GraphicsDevice, 100, 50);
            Color[] data = new Color[100 * 50];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Red;
            _rectTexture.SetData(data);
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
            _rotRect.SetRotation(_rotRect.Rotation + 0.01f); // Rotate continuously
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _rotRect.Draw(spriteBatch, _rectTexture);
            spriteBatch.DrawString(GlobalResources.Font, "HOLY FUCKING SHIT", new Vector2(220, 230), Color.White);
            spriteBatch.DrawString(GlobalResources.Font, "A SPINNING RECTANGLE", new Vector2(220, 260), Color.White);
        }
    }

}

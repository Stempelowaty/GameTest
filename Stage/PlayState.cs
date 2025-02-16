﻿using Microsoft.Xna.Framework.Graphics;
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
        private Texture2D _rectTexture;

        public PlayState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            Texture2D playerTexture = new Texture2D(GlobalResources.GraphicsDevice, 50, 50);
            Color[] data = new Color[50 * 50];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Blue;
            playerTexture.SetData(data);

            _player = new Entity(playerTexture, new Vector2(200, 200), new Vector2(50, 50));
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
            if (Keyboard.GetState().IsKeyDown(Keys.W)) _player.Move(0, -2);
            if (Keyboard.GetState().IsKeyDown(Keys.S)) _player.Move(0, 2);
            if (Keyboard.GetState().IsKeyDown(Keys.A)) _player.Move(-2, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.D)) _player.Move(2, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.T)) _player.Teleport(400, 300);
            if (Keyboard.GetState().IsKeyDown(Keys.X)) _player.Push(4, 0);

            _player.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _player.Draw(spriteBatch);
            spriteBatch.DrawString(GlobalResources.Font, "HOLY MOLY", new Vector2(220, 230), Color.White);
            spriteBatch.DrawString(GlobalResources.Font, "A WALKING RECTANGLE", new Vector2(220, 260), Color.White);
        }
    }

}

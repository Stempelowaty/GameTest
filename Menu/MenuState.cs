using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Burucki.Components;
using Burucki.Utils;
using Burucki.Stage;

namespace Burucki.Menu
{
    public class MenuState : IGameState
    {
        private GameStateMachine _stateMachine;
        private Button _playButton;
        private Dropdown _dropdown;
        private Checkbox _fullscreenCheckbox;

        public MenuState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {

            // Create button texture
            Texture2D buttonTexture = new Texture2D(GlobalResources.GraphicsDevice, 120, 50);
            Color[] buttonData = new Color[120 * 50];
            for (int i = 0; i < buttonData.Length; i++) buttonData[i] = Color.White;
            buttonTexture.SetData(buttonData);

            // Create play button
            _playButton = new Button(buttonTexture, new Vector2(500, 300), "Play", GlobalResources.Font);
            _playButton.OnClick += () => _stateMachine.ChangeState(new PlayState(_stateMachine));

            // Create dropdown texture
            Texture2D dropdownTexture = new Texture2D(GlobalResources.GraphicsDevice, 120, 50);
            Color[] dropdownData = new Color[120 * 50];
            for (int i = 0; i < dropdownData.Length; i++) dropdownData[i] = Color.LightGray;
            dropdownTexture.SetData(dropdownData);

            // Create dropdown
            _dropdown = new Dropdown(dropdownTexture, dropdownTexture, new Vector2(200, 200), new List<string> {  "1280x720", "1600x900", "1920x1080" }, GlobalResources.Font);
            _dropdown.OnSelectionChanged += (selection) =>
            {
                string[] dimensions = selection.Split('x');
                if (dimensions.Length == 2 &&
                    int.TryParse(dimensions[0], out int width) &&
                    int.TryParse(dimensions[1], out int height))
                {
                    GameInstance.Instance.UpdateResolution(width, height);
                }
            };

            // Load checkbox textures
            Texture2D uncheckedTexture = new Texture2D(GlobalResources.GraphicsDevice, 20, 20);
            Texture2D checkedTexture = new Texture2D(GlobalResources.GraphicsDevice, 20, 20);

            Color[] data = new Color[20 * 20];
            for (int i = 0; i < data.Length; i++) data[i] = Color.White;
            uncheckedTexture.SetData(data);
            for (int i = 0; i < data.Length; i++) data[i] = Color.Green;
            checkedTexture.SetData(data);

            _fullscreenCheckbox = new Checkbox(uncheckedTexture, checkedTexture, new Vector2(200, 100), "Fullscreen", GlobalResources.Font);
            _fullscreenCheckbox.OnCheckedChanged += (isChecked) =>
            {
                GameInstance.Instance.ToggleFullscreen(isChecked);
            };
        }

        public void Exit()
        {
            Console.WriteLine("Exited Menu State");
        }

        public void Update(GameTime gameTime)
        {
            _playButton.Update();
            _dropdown.Update();
            _fullscreenCheckbox.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GlobalResources.Font, "Menu", new Vector2(220, 230), Color.White);
            _playButton.Draw(spriteBatch);
            _dropdown.Draw(spriteBatch);
            _fullscreenCheckbox.Draw(spriteBatch);
        }
    }
}

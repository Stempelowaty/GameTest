﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Burucki.Components
{
    public class Dropdown
    {
        private Texture2D _buttonTexture;
        private Texture2D _optionTexture;
        private Vector2 _position;
        private List<string> _options;
        private SpriteFont _font;
        private bool _isOpen;
        private int _selectedIndex;
        private Rectangle _buttonBounds;
        private List<Rectangle> _optionBounds;
        private bool _isClicked;

        public event Action<string> OnSelectionChanged;

        public Dropdown(Texture2D buttonTexture, Texture2D optionTexture, Vector2 position, List<string> options, SpriteFont font)
        {
            _buttonTexture = buttonTexture;
            _optionTexture = optionTexture;
            _position = position;
            _options = options;
            _font = font;
            _isOpen = false;
            _selectedIndex = 0;
            _buttonBounds = new Rectangle((int)position.X, (int)position.Y, buttonTexture.Width, buttonTexture.Height);
            _optionBounds = new List<Rectangle>();

            for (int i = 0; i < options.Count; i++)
            {
                _optionBounds.Add(new Rectangle((int)position.X, (int)position.Y + buttonTexture.Height * (i + 1), buttonTexture.Width, buttonTexture.Height));
            }
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            bool isHoveringButton = _buttonBounds.Contains(mouse.Position);

            if (isHoveringButton && mouse.LeftButton == ButtonState.Pressed && !_isClicked)
            {
                _isOpen = !_isOpen;
                _isClicked = true;
            }
            else if (mouse.LeftButton == ButtonState.Released)
            {
                _isClicked = false;
            }

            if (_isOpen)
            {
                for (int i = 0; i < _options.Count; i++)
                {
                    if (_optionBounds[i].Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
                    {
                        _selectedIndex = i;
                        _isOpen = false;
                        OnSelectionChanged?.Invoke(_options[_selectedIndex]);
                        break;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw dropdown button
            spriteBatch.Draw(_buttonTexture, _position, Color.White);
            spriteBatch.DrawString(_font, _options[_selectedIndex], _position + new Vector2(10, 10), Color.Black);

            // Draw dropdown options
            if (_isOpen)
            {
                for (int i = 0; i < _options.Count; i++)
                {
                    spriteBatch.Draw(_optionTexture, _optionBounds[i].Location.ToVector2(), Color.White);
                    spriteBatch.DrawString(_font, _options[i], _optionBounds[i].Location.ToVector2() + new Vector2(10, 10), Color.Black);
                }
            }
        }
    }
}

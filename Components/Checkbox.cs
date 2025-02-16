using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace Burucki.Components
{
    public class Checkbox
    {
        private Texture2D _uncheckedTexture;
        private Texture2D _checkedTexture;
        private Vector2 _position;
        private bool _isChecked;
        private SpriteFont _font;
        private string _label;
        private Rectangle _bounds;

        public event Action<bool> OnCheckedChanged;

        public Checkbox(Texture2D uncheckedTexture, Texture2D checkedTexture, Vector2 position, string label, SpriteFont font)
        {
            _uncheckedTexture = uncheckedTexture;
            _checkedTexture = checkedTexture;
            _position = position;
            _label = label;
            _font = font;

            _bounds = new Rectangle((int)position.X, (int)position.Y, _uncheckedTexture.Width, _uncheckedTexture.Height);
        }

        public void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && _bounds.Contains(Mouse.GetState().Position))
            {
                _isChecked = !_isChecked;
                OnCheckedChanged?.Invoke(_isChecked);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_isChecked ? _checkedTexture : _uncheckedTexture, _position, Color.White);
            spriteBatch.DrawString(_font, _label, new Vector2(_position.X + 30, _position.Y), Color.White);
        }
    }
}

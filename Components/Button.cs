using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Burucki.Components
{
    public class Button
    {
        private Texture2D _texture;
        private Vector2 _position;
        private string _text;
        private SpriteFont _font;
        private Color _color;
        private Rectangle _bounds;
        private bool _isHovered;
        private bool _isClicked;

        public delegate void ButtonClick();
        public event ButtonClick OnClick;

        public Button(Texture2D texture, Vector2 position, string text, SpriteFont font)
        {
            _texture = texture;
            _position = position;
            _text = text;
            _font = font;
            _color = Color.White;
            _bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            _isHovered = _bounds.Contains(mouse.Position);

            if (_isHovered)
            {
                _color = Color.Gray;

                if (mouse.LeftButton == ButtonState.Pressed && !_isClicked)
                {
                    _isClicked = true;
                    OnClick?.Invoke();
                }
                else if (mouse.LeftButton == ButtonState.Released)
                {
                    _isClicked = false;
                }
            }
            else
            {
                _color = Color.White;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _color);
            Vector2 textSize = _font.MeasureString(_text);
            Vector2 textPosition = _position + new Vector2((_texture.Width - textSize.X) / 2, (_texture.Height - textSize.Y) / 2);
            spriteBatch.DrawString(_font, _text, textPosition, Color.Black);
        }
    }
}

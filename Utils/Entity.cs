using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Burucki.Utils
{
    public class Entity
    {
        public RotatableRectangle Hitbox { get; private set; }
        public Texture2D Texture { get; private set; }

        private Vector2 _velocity;
        private float _friction = 0.9f; // Slightly lower friction
        private float _minVelocity = 0.05f; // Stops movement if speed is too low

        public Entity(Texture2D texture, Vector2 position, Vector2 size, float rotation = 0f)
        {
            Texture = texture;
            Hitbox = new RotatableRectangle(position, size, rotation);
            _velocity = Vector2.Zero;
        }

        public void Move(float x, float y)
        {
            Vector2 newPosition = Hitbox.Position + new Vector2(x, y);
            Hitbox = new RotatableRectangle(newPosition, Hitbox.Size, Hitbox.Rotation);
        }

        public void Teleport(float x, float y)
        {
            Vector2 newPosition = new Vector2(x, y);
            Hitbox = new RotatableRectangle(newPosition, Hitbox.Size, Hitbox.Rotation);
            _velocity = Vector2.Zero; // Reset movement on teleport
        }

        public void Push(float x, float y)
        {
            _velocity += new Vector2(x, y);
            Console.WriteLine($"Push applied: {_velocity}"); // Debug
        }

        public void Update()
        {
            if (_velocity.LengthSquared() > _minVelocity * _minVelocity)
            {
                Move(_velocity.X, _velocity.Y);
                Console.WriteLine($"Moving: {_velocity}"); // Debug movement
                _velocity *= _friction; // Apply friction
            }
            else
            {
                _velocity = Vector2.Zero; // Stop movement
            }
        }

  
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Hitbox.Position, null, Color.White, Hitbox.Rotation, Hitbox.Size / 2, 1f, SpriteEffects.None, 0);
        }
    }
}

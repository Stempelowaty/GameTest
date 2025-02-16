using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burucki.Utils
{
    public class RotatableRectangle
    {
        public Vector2 Position { get; private set; }
        public Vector2 Size { get; private set; }
        public float Rotation { get; private set; }

        private Vector2 _origin;
        private VertexPositionColor[] _vertices;

        public RotatableRectangle(Vector2 position, Vector2 size, float rotation)
        {
            Position = position;
            Size = size;
            Rotation = rotation;
            _origin = size / 2;
            UpdateVertices();
        }

        private void UpdateVertices()
        {
            Vector2 halfSize = Size / 2;

            Vector2[] corners =
            {
            new Vector2(-halfSize.X, -halfSize.Y),
            new Vector2(halfSize.X, -halfSize.Y),
            new Vector2(halfSize.X, halfSize.Y),
            new Vector2(-halfSize.X, halfSize.Y)
        };

            Matrix rotationMatrix = Matrix.CreateRotationZ(Rotation);
            for (int i = 0; i < corners.Length; i++)
            {
                corners[i] = Vector2.Transform(corners[i], rotationMatrix) + Position;
            }

            _vertices = new VertexPositionColor[]
            {
            new VertexPositionColor(new Vector3(corners[0], 0), Color.White),
            new VertexPositionColor(new Vector3(corners[1], 0), Color.White),
            new VertexPositionColor(new Vector3(corners[2], 0), Color.White),
            new VertexPositionColor(new Vector3(corners[3], 0), Color.White)
            };
        }

        public void SetRotation(float rotation)
        {
            Rotation = rotation;
            UpdateVertices();
        }

        public bool ContainsPoint(Vector2 point)
        {
            Vector2 relativePoint = point - Position;
            Matrix inverseRotation = Matrix.CreateRotationZ(-Rotation);
            relativePoint = Vector2.Transform(relativePoint, inverseRotation);
            Vector2 halfSize = Size / 2;
            return Math.Abs(relativePoint.X) <= halfSize.X && Math.Abs(relativePoint.Y) <= halfSize.Y;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, Rotation, _origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}

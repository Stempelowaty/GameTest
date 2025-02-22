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

        public RotatableRectangle(Vector2 position, Vector2 size, float rotation)
        {
            Position = position;
            Size = size;
            Rotation = rotation;
            _origin = size / 2;
        }

   

        public void SetRotation(float rotation)
        {
            Rotation = rotation;
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
        

        /*=================================HITBOX DETECTION=============================================================================================*/

        public bool Intersects(RotatableRectangle other)
        {
            Vector2[] axes = GetSeparatingAxes(other);

            foreach (var axis in axes)
            {
                if (!OverlapsOnAxis(axis, this, other))
                {
                    return false; // No overlap on one axis means no intersection
                }
            }

            return true; // Overlapping on all axes means intersection
        }

        private Vector2[] GetSeparatingAxes(RotatableRectangle other)
        {
            Vector2[] cornersA = GetCorners();
            Vector2[] cornersB = other.GetCorners();

            return new Vector2[]
            {
                GetEdgeNormal(cornersA[0], cornersA[1]),
                GetEdgeNormal(cornersA[1], cornersA[2]),
                GetEdgeNormal(cornersB[0], cornersB[1]),
                GetEdgeNormal(cornersB[1], cornersB[2])
            };
        }

        private Vector2 GetEdgeNormal(Vector2 p1, Vector2 p2)
        {
            Vector2 edge = p2 - p1;
            Vector2 normal = new Vector2(-edge.Y, edge.X); // Perpendicular normal
            return Vector2.Normalize(normal); // Normalize ensures accurate SAT projection
        }

        private bool OverlapsOnAxis(Vector2 axis, RotatableRectangle a, RotatableRectangle b)
        {
            (float minA, float maxA) = ProjectOntoAxis(axis, a);
            (float minB, float maxB) = ProjectOntoAxis(axis, b);

            return !(maxA < minB || maxB < minA); // Check for overlap
        }

        private (float min, float max) ProjectOntoAxis(Vector2 axis, RotatableRectangle rect)
        {
            Vector2[] corners = rect.GetCorners();
            float min = Vector2.Dot(axis, corners[0]);
            float max = min;

            for (int i = 1; i < corners.Length; i++)
            {
                float projection = Vector2.Dot(axis, corners[i]);
                if (projection < min) min = projection;
                if (projection > max) max = projection;
            }

            return (min, max);
        }

        private Vector2[] GetCorners()
        {
            Vector2 halfSize = Size / 2;

            Vector2[] localCorners =
            {
                new Vector2(-halfSize.X, -halfSize.Y),
                new Vector2(halfSize.X, -halfSize.Y),
                new Vector2(halfSize.X, halfSize.Y),
                new Vector2(-halfSize.X, halfSize.Y)
            };

            Matrix rotationMatrix = Matrix.CreateRotationZ(Rotation);
            Vector2[] worldCorners = new Vector2[4];

            for (int i = 0; i < 4; i++)
            {
                worldCorners[i] = Vector2.Transform(localCorners[i], rotationMatrix) + Position;
            }

            return worldCorners;
        }
    }
}

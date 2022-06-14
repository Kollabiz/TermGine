using TermGine.Core;
using TermGine;

namespace TermGine.Collision
{
    class CollisionShape2D
    {
        protected Vector2 position;

        public CollisionShape2D() {}

        public Vector2 GetPosition()
        {
            return position;
        }

        public void CollisionShape2DInit(Vector2 _pos)
        {
            position = _pos;
        }
    }

    class SphereCollider2D: CollisionShape2D
    {
        private float radius;

        public SphereCollider2D(Vector2 _pos, float _radius)
        {
            CollisionShape2DInit(_pos);
            radius = _radius;
        }

        public float GetRadius()
        {
            return radius;
        }

        public bool Intersects(SphereCollider2D other)
        {
            float dist = Math.Abs(this.position.X - other.GetPosition().X) + Math.Abs(this.position.Y - other.GetPosition().Y);   // heuristic distance from this sphere center to other

            if(dist < this.GetRadius() + other.GetRadius())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class BoxCollider2D: CollisionShape2D
    {
        protected float width;
        protected float height;

        public BoxCollider2D(float _width, float _height, Vector2 _pos)
        {
            CollisionShape2DInit(_pos);
            width = _width;
            height = _height;
        }

        public bool Intersects(BoxCollider2D other)
        {
            float dist = Math.Abs(this.GetPosition().X - other.GetPosition().X) + Math.Abs(this.GetPosition().Y - other.GetPosition().Y);
            Vector2 a = new Vector2(this.GetPosition().X + this.width, this.GetPosition().Y + this.height);
            Vector2 b = new Vector2(other.GetPosition().X + other.width, other.GetPosition().Y + other.height);

            float Ol = dist - (a / 2).Dot(other.GetPosition()) - (b / 2).Dot(this.GetPosition());

            return (Ol > 0);
        }
    }
}
using TermGine.Core;

namespace TermGine
{
    class AmbientLight
    {
        public Vector2 position;
        public Vector3 direction;

        public AmbientLight(Vector2 pos, Vector3 dir)
        {
            position = pos;
            direction = dir;
        }
    }
}
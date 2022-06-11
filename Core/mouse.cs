using System.Runtime.InteropServices;

namespace TermGine.Core
{
    class Mouse
    {
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Vector2 GetPos()
        {
            POINT point;
            if(GetCursorPos(out point))
            {
                return new Vector2(point.X, point.Y);
            }
            return Vector2.ZERO;
        }
    }
}
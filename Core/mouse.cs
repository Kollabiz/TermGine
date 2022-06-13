using System.Runtime.InteropServices;

namespace TermGine.Core
{
    class Mouse
    {
        ///<summary>
        ///Static method <c>GetCursorPos</c> is utility
        ///method that is used in <c>GetPos</c> method,
        ///and not recommend to be used
        ///</summary>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        ///<summary>
        ///Static method <c>GetPos</c> returns <c>Vector2</c>
        ///that represents current mouse position in pixels
        ///from left-upper corner of screen
        ///</summary>
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
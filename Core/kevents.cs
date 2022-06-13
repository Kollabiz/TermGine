namespace TermGine
{
    class Keyboard
    {

        ///<summary>
        ///Returns state of given key
        ///</summary>
        public static bool IsPressed(int k)
        {
            return Core.Keyboard.KeyboardNative.IsKeyPressed(k);
        }
    }
}
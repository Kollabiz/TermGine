using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace TermGine.Core.Keyboard
{
    class KeyboardNative
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetKeayboardState(byte[] lpKeyState);

        private static byte[] keys = new byte[256];

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int key);

        private const int KEY_TOGGLED = 1;
        private const int KEY_PRESSED = 8000;

        ///<summary>
        ///Returns state of given key
        ///</summary>
        public static bool IsKeyPressed(int k)
        {
            short status = GetKeyState(k);
            return (status == KEY_PRESSED);
        }

        ///<summary>
        ///Returns true if any key pressed, else false
        ///</summary>
        public static bool IsAnyKeyPressed()
        {
            GetKeyState(0);

            if(!GetKeayboardState(keys))
            {
                int err = Marshal.GetLastWin32Error();
                throw new Win32Exception(err);
            }
            
            for(int i = 0; i < 256; i++)
            {
                if(i < 7)
                {
                    keys[i] = 0;
                }
                else
                {
                    keys[i] = (byte)(keys[i] & 0x80);
                }
            }
            return keys.Any(k => k != 0);
        }

        ///<summary>
        ///Waits until any key getting pressed
        ///</summary>
        public static void WaitForKeyDown()
        {
            bool AnyKeyPressed;
            do
            {
                AnyKeyPressed = IsAnyKeyPressed();
            } while(!AnyKeyPressed);
        }

        ///<summary>
        ///Waits until any key getting released
        ///</summary>
        public static void WaitForKeyUp()
        {
            bool AnyKeyPressed;
            do
            {
                AnyKeyPressed = IsAnyKeyPressed();
            } while(AnyKeyPressed);
        }

        ///<summary>
        ///Reads all characters from input stream
        ///<summary>
        public static void FlushInputStream()
        {
            while(Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}
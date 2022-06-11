namespace TermGine
{
    class Keyboard
    {
        public static readonly string[] keys = new string[]
        {
            "SPACE",
            "PGUP",
            "PGDOWN",
            "END",
            "HOME",
            "LEFT",
            "UP",
            "RIGHT",
            "DOWN",
            "SELECT",
            "PRINT",
            "EXECUTE",
            "PRTSCRN",
            "INS",
            "DEL",
            "HELP",
            "ZERO",
            "ONE",
            "TWO",
            "THREE",
            "FOUR",
            "FIVE",
            "SIX",
            "SEVEN",
            "EIGHT",
            "NINE",
            "",
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "CONTEXT",
            "SLEEP",
            "NUMP0",
            "NUMP1",
            "NUMP2",
            "NUMP3",
            "NUMP4",
            "NUMP5",
            "NUMP6",
            "NUMP7",
            "NUMP8",
            "MUMP9",
            "NUMP_STAR",
            "NUMP_PLUS",
            "SEP",
            "NUMP_MINUS",
        };

        public static bool IsPressed(string k)
        {
            for(int i = 0; i < keys.Length; i++)
            {
                if(keys[i] == k)
                {
                    return Core.Keyboard.KeyboardNative.IsKeyPressed(i);
                }
            }
            return false;
        }
    }
}
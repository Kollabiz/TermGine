using System.Drawing;

namespace TermGine.Core
{
    class Utils 
    {   
        
        ///<summary>
        ///Static method <c>MixColor</C> return Color
        ///that is a mixed color1 and color2
        ///</summary>
        public static Color MixColor(Color color1, Color color2)
        {
            byte normalizedA = (byte)(color2.A / 255);
            byte mixedA = (byte)Math.Clamp(color2.A + color1.A, 0, 255);
            byte mixedR = (byte)Math.Clamp((color2.R * normalizedA + color1.R), 0, 255);
            byte mixedG = (byte)Math.Clamp((color2.G * normalizedA + color1.G), 0, 255);
            byte mixedB = (byte)Math.Clamp((color2.B * normalizedA + color1.B), 0, 255);

            return Color.FromArgb(mixedA, mixedR, mixedG, mixedB);
        }

        public static double UnixNow()
        {
            return DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
using System.Drawing;
using TermGine.Rendering;

namespace TermGine.Core
{
    class Utils 
    {   
        
        ///<summary>
        ///Static method <c>MixColor</C> return Color
        ///that is a mixed color1 and color2
        ///</summary>
        public static Color MixColor(Color color1, Color color2, byte blendMode = MixModes.ALPHA_BLEND)
        {
            switch(blendMode)
            {
                case 0:     //OPAQUE
                    return color2;
                case 1:     //ALPHA_BLEND
                    byte normalizedA = (byte)(color2.A / 255);
                    byte mixedA = (byte)Math.Clamp(color2.A + color1.A, 0, 255);
                    byte mixedR = (byte)Math.Clamp((color2.R * normalizedA + color1.R), 0, 255);
                    byte mixedG = (byte)Math.Clamp((color2.G * normalizedA + color1.G), 0, 255);
                    byte mixedB = (byte)Math.Clamp((color2.B * normalizedA + color1.B), 0, 255);

                    return Color.FromArgb(mixedA, mixedR, mixedG, mixedB);
                case 2:     //ALPHA_CLIP
                    if(color2.A < 255)
                    {
                        return color1;
                    }
                    else
                    {
                        return color2;
                    }
                case 3:     //ALPHA_MAX
                    if(color1.A > color2.A)
                    {
                        return color1;
                    }
                    else
                    {
                        return color2;
                    }
                case 4:     //ALPHA_MIN
                    if(color1.A < color2.A)
                    {
                        return color1;
                    }
                    else
                    {
                        return color2;
                    }
                default:
                    return color2;
            }
        }

        ///<summary>
        ///Static method <c>UnixNow</c> returns
        ///time of its call in seconds of
        ///unix epoch
        ///</summary>
        public static double UnixNow()
        {
            return DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        ///<summary>
        ///Static method <c>NormalMapFromImage</c> creates
        ///array of <c>Vector3</c> from OpenGL normal map image
        ///</summary>
        public static Vector3[,] NormalMapFromImage(string path)
        {
            Bitmap img = (Bitmap)(Bitmap.FromFile(path));
            Vector3[,] normals = new Vector3[img.Height,img.Width];

            for(int y = 0; y < img.Height; y++)
            {
                for(int x = 0; x < img.Width; x++)
                {
                    Color pxColor = img.GetPixel(x, y);
                    Vector3 normal = new Vector3(pxColor.R, pxColor.G, pxColor.B).Normalized();
                    normals[y, x] = normal;
                }
            }
            img.Dispose();
            return normals;
        }
    }
}
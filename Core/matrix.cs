using System.Drawing;
using Pastel;

namespace TermGine.Core
{   
    ///<summary>
    ///Class <c>ColorMatrix</C> represents a
    ///matrix with different colors
    ///</summary>
    class ColorMatrix
    {
        private Color[,] matrix;
        public readonly int[] size;

        public ColorMatrix(int sizeX, int sizeY)
        {
            size = new int[] {sizeX, sizeY};        //Size of matrix
            matrix = new Color[sizeY, sizeX];       //Matrix itself
        }

        ///<summary>
        ///Static method <c>FromImage</c> returns <c>ColorMatrix</c>
        ///created from given image
        ///</summary>
        public static ColorMatrix FromImage(string path) {
            Bitmap img = new Bitmap(path);
            ColorMatrix _matrix = new ColorMatrix(img.Width, img.Height);

            for(int y = 0; y < img.Height; y++)
            {
                for(int x = 0; x < img.Width; x++)
                {
                    _matrix.SetPx(x, y, img.GetPixel(x, y));
                }
            }

            img.Dispose();
            return _matrix;
        }

        ///<summary>
        ///Method <c>SetPx</c> sets pixel at pos to given color
        ///</summary>
        public void SetPx(Vector2 pos, Color color)
        {
            matrix[pos.Y, pos.X] = color;
        }

        ///<summary>
        ///Method <c>SetPx</c> sets pixel at (x;y) to given color
        ///</summary>
        public void SetPx(int x, int y, Color color)
        {
            matrix[y, x] = color;
        }

        ///<summary>
        ///Method <c>GetPx</c> returns color from coordinates represented by pos
        ///</summary>
        public Color GetPx(Vector2 pos) 
        {
            return matrix[pos.Y, pos.X];
        }

        ///<summary>
        ///Method <c>GetPx</c> returns color from coordinates (x;y)
        ///</summary>
        public Color GetPx(int x, int y) 
        {
            return matrix[y, x];
        }

        ///<summary>
        ///Method <c>Copy</c> copies given matrix on current
        ///starting from origin
        ///</summary>
        public void Copy(Vector2 origin, ColorMatrix _matrix) 
        {
            for(int y = 0; y < _matrix.size[1]; y++) 
            {
                for(int x = 0; x < _matrix.size[0]; x++) 
                {
                    if(_matrix.GetPx(x, y).A == 0)
                    {
                        continue;
                    }
                    if(_matrix.GetPx(x, y).A == 255)
                    {
                        SetPx(x + origin.X, y + origin.Y, _matrix.GetPx(x, y));
                    } else
                    {
                        SetPx(x + origin.X, y + origin.Y, Utils.MixColor(_matrix.GetPx(x, y), matrix[y + origin.Y, x + origin.X]));
                    }
                }
            }
        }

        ///<summary>
        ///Method <c>Fill</c> fills matrix with given color
        ///</summary>
        public void Fill(Color color) {
            for(int y = 0; y < size[1]; y++)
            {
                for(int x = 0; x < size[0]; x++)
                {
                    matrix[y, x] = color;
                }
            }
        }

        ///<summary>
        ///Method <c>ToString</c> converts given matrix
        ///to string
        ///</summary>
        public override string ToString()
        {
            string converted = "";
            for(int y = 0; y < size[1]; y++)
            {
                for(int x = 0; x < size[0]; x++)
                {
                    converted += "  ".PastelBg(GetPx(x, y));
                }
                converted += "\n";
            }
            return converted;
        }

        ///<summary>
        ///Method <c>Print</c> prints matrix to output stream
        ///without converting whole matrix to string and storing it
        ///</summary>
        public void Print()
        {
            int i = 1;
            foreach(Color px in matrix)
            {
                if(i % size[0] == 0)
                {
                    Console.WriteLine("  ".PastelBg(px));
                } else
                {
                    Console.Write("  ".PastelBg(px));
                }
                i++;
            }
        }
    }
}
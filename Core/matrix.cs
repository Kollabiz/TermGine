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
        private byte clipMode = TermGine.Clipping.Modes.CLIP;

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

        public void SetClipMode(byte mode)
        {
            clipMode = mode;
        }

        public byte GetClipMode()
        {
            return clipMode;
        }

        ///<summary>
        ///Method <c>SetPx</c> sets pixel at pos to given color
        ///</summary>
        public void SetPx(Vector2 pos, Color color)
        {
            switch(clipMode)
            {
                case 0:
                    if(0 <= pos.X && pos.X < size[0] && 0 <= pos.Y && pos.Y < size[1])
                    {
                        matrix[(int)(pos.Y), (int)(pos.X)] = color;
                    }
                    break;
                
                case 1:
                    int _x = (int)(pos.X);
                    int _y = (int)(pos.Y);
                    if(pos.X < 0 || pos.X >= size[0])
                    {
                        _x = (int)(pos.X % size[0]);
                    }
                    if(pos.Y < 0 || pos.Y >= size[1])
                    {
                        _y = (int)(pos.Y % size[1]);
                    }
                    matrix[_y, _x] = color;
                    break;
                
                case 2:
                    if(pos.X < 0 || pos.X >= size[0] || pos.Y < 0 || pos.Y >= size[1])
                    {
                        throw new IndexOutOfRangeException($"invalid pixel position ({pos.X};{pos.Y})");
                    }
                    else
                    {
                        matrix[(int)(pos.X), (int)(pos.Y)] = color;
                    }
                    break;
            }
        }

        ///<summary>
        ///Method <c>SetPx</c> sets pixel at (x;y) to given color
        ///</summary>
        public void SetPx(int x, int y, Color color)
        {
            switch(clipMode)
            {
                case 0:
                    if(0 <= x && x < size[0] && 0 <= y && y < size[1])
                    {
                        matrix[(int)(y), (int)(x)] = color;
                    }
                    break;
                
                case 1:
                    int _x = (int)(x);
                    int _y = (int)(y);
                    if(x < 0 || x >= size[0])
                    {
                        _x = (int)(x % size[0]);
                    }
                    if(y < 0 || y >= size[1])
                    {
                        _y = (int)(y % size[1]);
                    }
                    matrix[_y, _x] = color;
                    break;
                
                case 2:
                    if(x < 0 || x >= size[0] || y < 0 || y >= size[1])
                    {
                        throw new IndexOutOfRangeException($"invalid pixel position ({x};{y})");
                    }
                    else
                    {
                        matrix[(int)(x), (int)(y)] = color;
                    }
                    break;
            }
        }

        ///<summary>
        ///Method <c>GetPx</c> returns color from coordinates represented by pos
        ///</summary>
        public Color GetPx(Vector2 pos) 
        {
            if(pos.X < 0 || pos.X >= size[0] || pos.Y < 0 || pos.Y >= size[1])
            {
                return Color.Black;
            }
            return matrix[(int)(pos.Y), (int)(pos.X)];
        }

        ///<summary>
        ///Method <c>GetPx</c> returns color from coordinates (x;y)
        ///</summary>
        public Color GetPx(int x, int y) 
        {
            if(x < 0 || x >= size[0] || y < 0 || y >= size[1])
            {
                return Color.Black;
            }
            return matrix[y, x];
        }

        ///<summary>
        ///Method <c>Copy</c> copies given matrix on current
        ///starting from origin
        ///</summary>
        public void Copy(Vector2 origin, ColorMatrix _matrix, byte blendMode = TermGine.Rendering.MixModes.ALPHA_BLEND) 
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
                        SetPx((int)(x + origin.X), (int)(y + origin.Y), _matrix.GetPx(x, y));
                    } else
                    {
                        SetPx((int)(x + origin.X), (int)(y + origin.Y), Utils.MixColor(_matrix.GetPx(x, y), matrix[(int)(y + origin.Y), (int)(x + origin.X)], blendMode));
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
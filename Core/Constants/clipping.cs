namespace TermGine.Clipping
{
    static class Modes
    {
        ///<summary>
        ///Mode <c>CLIP</c> will ignore all pixels
        ///that are out the matrix
        ///</summary>
        public const byte CLIP = 0;
        ///<summary>
        ///Mode <c>REPEAT</c> will repeat all pixels that
        ///out of matrix on other side (if pixel was 4 pixels
        ///out it will be set to 4 pixels from 0)
        ///</summary>
        public const byte REPEAT = 1;
        ///<summary>
        ///Mode <c>PREVENT</c> will prevent attempts to put pixel
        ///out of matrix by throwing an <c>IndexOutOfRangeException</c>
        ///</summary>
        public const byte PREVENT = 2;
    }
}
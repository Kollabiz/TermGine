namespace TermGine.Rendering
{
    static class MixModes
    {
        ///<summary>
        ///Mode <c>OPAQUE</c> will ignore alpha channel
        ///and rewrite pixel value
        ///</summary>
        public const byte OPAQUE = 0;
        ///<summary>
        ///Mode <c>ALPHA_BLEND</c> will mix two colors
        ///by their alpha channel
        ///</summary>
        public const byte ALPHA_BLEND = 1;
        ///<summary>
        ///Mode <c>ALPHA_CLIP</c> will ignore color if
        ///its alpha value is less than 255
        ///</summary>
        public const byte ALPHA_CLIP = 2;
        ///<summary>
        ///Mode <c>ALPHA_MAX</c> will select color with
        ///biggest alpha value
        ///</summary>
        public const byte ALPHA_MAX = 3;
        ///<summary>
        ///Mode <c>ALPHA_MIN</c> will select color with
        ///lowest alpha value
        ///</summary>
        public const byte ALPHA_MIN = 4;
    }
}
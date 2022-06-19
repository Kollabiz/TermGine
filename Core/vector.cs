namespace TermGine.Core {
    ///<summary>
    ///Class <c>Vector2</c> represents
    ///two-dimensional vector
    ///</summary>
    class Vector2 {

        ///<summary>
        ///Constant <c>ZERO</c> contains two-dimensional
        ///vector with zero values
        ///</summary>
        public static readonly Vector2 ZERO = new Vector2(0, 0);

        ///<summary>X component of vector</summary>
        public float X;
        ///<summary>Y component of vector</summary>
        public float Y;

        public Vector2(float _X, float _Y) {
            X = _X;
            Y = _Y;
        }

        ///<summary>
        ///Method <c>SetX</c> sets <c>X</c> component
        ///of vector to given value
        ///</summary>
        public void SetX(float _X) {
            X = _X;
        }

        ///<summary>
        ///Method <c>SetY</c> sets <c>Y</c> component
        ///of vector to given value
        ///</summary>
        public void SetY(float _Y) {
            Y = _Y;
        }

        ///<summary>
        ///Method <c>Set</c> sets both components of
        ///vector to given values
        ///</summary>
        public void Set(float _X, float _Y) {
            X = _X;
            Y = _Y;
        }

        ///<summary>
        ///Method <c>Resize</c> resizes vector by
        ///given values
        ///</summary>
        public void Resize(float sizeX, float sizeY) {
            X = X * sizeX;
            Y = Y * sizeY;
        }

        ///<summary>
        ///Method <c>Resize</c> resizes vector by
        ///given size
        ///</summary>
        public void Resize(float size) {
            X = X * size;
            Y = Y * size;
        }

        ///<summary>
        ///Method <c>Length</c> returns length of
        ///vector
        ///</summary>
        public float Length() {
            float length = (float)(Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)));
            return length;
        }

        ///<summary>
        ///Method <c>Normalized</c> returns normalized
        ///vector
        ///</summary>
        public Vector2 Normalized() {
            float l = Length();
            float xNormalized = X / l, yNormalized = Y / l;
            return new Vector2(xNormalized, yNormalized);
        }

        ///<summary>
        ///Method <c>Dot</c> returns Dot Product of
        ///this vector and other
        ///</summary>
        public float Dot(Vector2 other)
        {
            return this.X * other.X + this.Y * other.Y;
        }

        ///<summary>
        ///Method <c>Rotate</c> rotates vector on given
        ///angle
        ///</summary>
        public void Rotate(double angle) {
            X = (int)(X * Math.Cos(angle));
            Y = (int)(Y * Math.Sin(angle));
        }

        ///<summary>
        ///Method <c>Rotated</c> returns vector, rotated on
        ///given angle
        ///</summary>
        public Vector2 Rotated(double angle) {
            int _X = (int)(X * Math.Cos(angle));
            int _Y = (int)(Y * Math.Sin(angle));
            return new Vector2(_X, _Y);
        }

        public static Vector2 operator /(Vector2 divided, float divisor)
        {
            return new Vector2(divided.X / divisor, divided.Y / divisor);
        }
    }
    
    ///<summary>
    ///Class <c>Vector3</c> represents a
    ///three-dimensional vector
    ///</summary>
    class Vector3
    {
        ///<summary>X component of vector</summary>
        public float X;
        ///<summary>Y component of vector</summary>
        public float Y;
        ///<summary>Z component of vector</summary>
        public float Z;

        ///<summary>
        ///Constant <c>ZERO</c> contains three-dimensional
        ///vector with zero values
        ///</summary>
        public static readonly Vector3 ZERO = new Vector3(0f, 0f, 0f);

        public Vector3(float _X, float _Y, float _Z)
        {
            X = _X;
            Y = _Y;
            Z = _Z;
        }

        ///<summary>
        ///Method <c>SetX</c> sets <c>X</c> component of
        ///vector to given value
        ///</summary>
        public void SetX(float _X)
        {
            X = _X;
        }

        ///<summary>
        ///Method <c>SetY</c> sets <c>Y</c> component of
        ///vector to given value
        ///</summary>
        public void SetY(float _Y)
        {
            Y = _Y;
        }

        ///<summary>
        ///Method <c>SetZ</c> sets <c>Z</c> component of
        ///vector to given value
        ///</summary>
        public void SetZ(float _Z)
        {
            Z = _Z;
        }

        ///<summary>
        ///Method <c>Set</c> sets all vector components to
        ///given values
        ///</summary>
        public void Set(float _X, float _Y, float _Z)
        {
            X = _X;
            Y = _Y;
            Z = _Z;
        }

        ///<summary>
        ///Method <c>Length</c> returns length of vector
        ///</summary>
        public float Length()
        {
            float length = (float)(Math.Sqrt(Math.Pow(Math.Abs(X), 2) + Math.Pow(Math.Abs(Y), 2) + Math.Pow(Math.Abs(Z), 2)));
            return length;
        }

        ///<summary>
        ///Method <c>Normalized</c> returns normalized vector
        ///</summary>
        public Vector3 Normalized()
        {
            float length = Length();
            float xNormalized = X / length, yNormalized = Y / length, zNormalized = Z / length;
            return new Vector3(xNormalized, yNormalized, zNormalized);
        }

        ///<summary>
        ///Method <c>Resize</c> resizes vector by given values
        ///</summary>
        public void Resize(float sizeX, float sizeY, float sizeZ)
        {
            X = X * sizeX;
            Y = Y * sizeY;
            Z = Z * sizeZ;
        }

        ///<summary>
        ///Method <c>Resize</c> resizes vector by given size
        ///</summary>
        public void Resize(float size)
        {
            X = X * size;
            Y = Y * size;
            Z = Z * size;
        }

        ///<summary>
        ///Method <c>Dot</c> returns Dot Product of <c>this</c> vector
        ///and given vector
        ///</summary>
        public float Dot(Vector3 other)
        {
            float dot = X * other.X + Y * other.Y + Z * other.Z;
            return dot;
        }
        
        public static Vector3 operator +(Vector3 first, Vector3 second)
        {
            return new Vector3(first.X + second.X, first.Y + second.Y, first.Z + second.Z);
        }

        public static Vector3 operator +(Vector3 first, Vector2 second)
        {
            return new Vector3(first.X + second.X, first.Y + second.Y, first.Z);
        }

        public static Vector3 operator /(Vector3 divided, float divisor)
        {
            return new Vector3(divided.X / divisor, divided.Y / divisor, divided.Z / divisor);
        }

        public static Vector3 operator *(Vector3 multiplied, float multiplier)
        {
            return new Vector3(multiplied.X * multiplier, multiplied.Y * multiplier, multiplied.Z * multiplier);
        }
    }
}
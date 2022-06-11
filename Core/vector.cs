namespace TermGine.Core {
    class Vector2 {

        public static readonly Vector2 ZERO = new Vector2(0, 0);

        public int X;
        public int Y;

        public Vector2(int _X, int _Y) {
            X = _X;
            Y = _Y;
        }

        public void SetX(int _X) {
            X = _X;
        }

        public void SetY(int _Y) {
            Y = _Y;
        }

        public void Set(int _X, int _Y) {
            X = _X;
            Y = _Y;
        }

        public void Resize(float sizeX, float sizeY) {
            X = (int)(X * sizeX);
            Y = (int)(Y * sizeY);
        }

        public void Resize(float size) {
            X = (int)(X * size);
            Y = (int)(Y * size);
        }

        public float Length() {
            float length = (float)(Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)));
            return length;
        }

        public Vector2 Normalized() {
            float l = Length();
            int xNormalized = (int)(X / l), yNormalized = (int)(Y / l);
            return new Vector2(xNormalized, yNormalized);
        }

        public void Rotate(double angle) {
            X = (int)(X * Math.Cos(angle));
            Y = (int)(Y * Math.Sin(angle));
        }

        public Vector2 Rotated(double angle) {
            int _X = (int)(X * Math.Cos(angle));
            int _Y = (int)(Y * Math.Sin(angle));
            return new Vector2(_X, _Y);
        }
    }

    class Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public static readonly Vector3 ZERO = new Vector3(0f, 0f, 0f);

        public Vector3(float _X, float _Y, float _Z)
        {
            X = _X;
            Y = _Y;
            Z = _Z;
        }

        public void SetX(float _X)
        {
            X = _X;
        }

        public void SetY(float _Y)
        {
            Y = _Y;
        }

        public void SetZ(float _Z)
        {
            Z = _Z;
        }

        public void Set(float _X, float _Y, float _Z)
        {
            X = _X;
            Y = _Y;
            Z = _Z;
        }

        public float Length()
        {
            float length = (float)(Math.Sqrt(Math.Pow(Math.Abs(X), 2) + Math.Pow(Math.Abs(Y), 2) + Math.Pow(Math.Abs(Z), 2)));
            return length;
        }

        public Vector3 Normalized()
        {
            float length = Length();
            float xNormalized = X / length, yNormalized = Y / length, zNormalized = Z / length;
            return new Vector3(xNormalized, yNormalized, zNormalized);
        }

        public void Resize(float sizeX, float sizeY, float sizeZ)
        {
            X = X * sizeX;
            Y = Y * sizeY;
            Z = Z * sizeZ;
        }

        public void Resize(float size)
        {
            X = X * size;
            Y = Y * size;
            Z = Z * size;
        }

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
    }
}
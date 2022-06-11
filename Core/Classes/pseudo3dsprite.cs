using TermGine.Core;
using System.Drawing;

namespace TermGine
{
    class Pseudo3DSprite: GameObject
    {
        private Vector3[,] normalMap;
        private ColorMatrix diffuse;
        private Vector2 pos;
        private ColorMatrix shaded;

        //  Constructors
        public Pseudo3DSprite(Scene _scene, Vector3[,] _normalMap, ColorMatrix _diff)
        {
            InitGameObject(_scene);
            normalMap = _normalMap;
            diffuse = _diff;
            shaded = new ColorMatrix(_diff.size[0], _diff.size[1]);
            pos = new Vector2(0, 0);
        }

        public Pseudo3DSprite(Scene _scene, string _normalMap, ColorMatrix _diff)
        {
            InitGameObject(_scene);
            normalMap = NormalMapFromImage(_normalMap);
            diffuse = _diff;
            shaded = new ColorMatrix(_diff.size[0], _diff.size[1]);
            pos = new Vector2(0, 0);
        }

        public Pseudo3DSprite(Scene _scene, string _normalMap, string _diff)
        {
            InitGameObject(_scene);
            normalMap = NormalMapFromImage(_normalMap);
            diffuse = ColorMatrix.FromImage(_diff);
            shaded = new ColorMatrix(diffuse.size[0], diffuse.size[1]);
            pos = new Vector2(0, 0);
        }

        public Pseudo3DSprite(Scene _scene, Vector3[,] _normalMap, string _diff)
        {
            InitGameObject(_scene);
            normalMap = _normalMap;
            diffuse = ColorMatrix.FromImage(_diff);
            shaded = new ColorMatrix(diffuse.size[0], diffuse.size[1]);
            pos = new Vector2(0, 0);
        }

        // Main methods

        public void SetPos(Vector2 _pos)
        {
            pos = _pos;
        }

        public void SetPos(int x, int y)
        {
            pos.SetX(x);
            pos.SetY(y);
        }

        public void ShadeAmbient(AmbientLight light)
        {
            for(int y = 0; y < shaded.size[1]; y++)
            {
                for(int x = 0; x < shaded.size[0]; x++)
                {
                    Vector3 normal = normalMap[y, x];
                    Color diff = diffuse.GetPx(x, y);
                    float dot = normal.Dot((light.direction + light.position).Normalized());
                    Color shade = Color.FromArgb((byte)(Math.Clamp(diff.R * dot, 0f, 255f)), (byte)(Math.Clamp(diff.G * dot, 0f, 255f)), (byte)(Math.Clamp(diff.B * dot, 0f, 255f)));
                    shaded.SetPx(x, y, shade);
                }
            }
        }

        // Engine methods

        public override void onUpdate(float dt)
        {
            ShadeAmbient(scene.GetAmbientLight());
            scene.surface.Copy(pos, shaded);
        }

        // Other methods

        public static Vector3[,] NormalMapFromImage(string path)
        {
            Bitmap img = (Bitmap)(Bitmap.FromFile(path));
            Vector3[,] normals = new Vector3[img.Height,img.Width];

            for(int y = 0; y < img.Height; y++)
            {
                for(int x = 0; x < img.Width; x++)
                {
                    Color pxColor = img.GetPixel(x, y);
                    // ?
                    Vector3 normal = new Vector3(pxColor.R, pxColor.G, pxColor.B).Normalized();
                    //  Maybe this crap would work, but i don't sure
                    normals[y, x] = normal;
                }
            }
            img.Dispose();
            return normals;
        }
    }
}
using TermGine.Core;
using System.Drawing;

namespace TermGine
{
    ///<summary>
    ///Class <c>Pseudo3DSprite</c> implements sprite
    ///with simple shading
    ///</summary>
    class Pseudo3DSprite: GameObject
    {
        private Vector3[,] normalMap;
        private ColorMatrix diffuse;
        private Vector2 pos;
        private ColorMatrix shaded;

        //  Constructors
        public Pseudo3DSprite(Scene _scene, Vector3[,] _normalMap, ColorMatrix _diff, string _name)
        {
            InitGameObject(_scene, _name);
            normalMap = _normalMap;
            diffuse = _diff;
            shaded = new ColorMatrix(_diff.size[0], _diff.size[1]);
            pos = new Vector2(0, 0);
        }

        public Pseudo3DSprite(Scene _scene, string _normalMap, ColorMatrix _diff, string _name)
        {
            InitGameObject(_scene, _name);
            normalMap = Utils.NormalMapFromImage(_normalMap);
            diffuse = _diff;
            shaded = new ColorMatrix(_diff.size[0], _diff.size[1]);
            pos = new Vector2(0, 0);
        }

        public Pseudo3DSprite(Scene _scene, string _normalMap, string _diff, string _name)
        {
            InitGameObject(_scene, _name);
            normalMap = Utils.NormalMapFromImage(_normalMap);
            diffuse = ColorMatrix.FromImage(_diff);
            shaded = new ColorMatrix(diffuse.size[0], diffuse.size[1]);
            pos = new Vector2(0, 0);
        }

        public Pseudo3DSprite(Scene _scene, Vector3[,] _normalMap, string _diff, string _name)
        {
            InitGameObject(_scene, _name);
            normalMap = _normalMap;
            diffuse = ColorMatrix.FromImage(_diff);
            shaded = new ColorMatrix(diffuse.size[0], diffuse.size[1]);
            pos = new Vector2(0, 0);
        }

        // Main methods

        ///<summary>
        ///Method <c>SetPos</c> sets sprite
        ///position to given position
        ///</summary>
        public void SetPos(Vector2 _pos)
        {
            pos = _pos;
        }

        ///<summary>
        ///Method <c>SetPos</c> sets sprite
        ///position to given position
        ///</summary>
        public void SetPos(int x, int y)
        {
            pos.SetX(x);
            pos.SetY(y);
        }

        // Ambient (diffuse) shading method
        private void ShadeAmbient(AmbientLight light)
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
            scene.GetSurface().Copy(pos, shaded);
        }
    }
}
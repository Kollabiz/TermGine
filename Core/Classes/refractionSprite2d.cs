using TermGine.Core;
using System.Drawing;

namespace TermGine
{
    class RefractionSprite2D: GameObject
    {
        private Vector3 position;
        private Vector3[,] normals;
        private ColorMatrix diffuse;
        private ColorMatrix shaded;
        private float transparency;

        public RefractionSprite2D(Scene _scene, Vector3 _pos, Vector3[,] _normalMap, ColorMatrix _diff, string _name, float _transp = 0.5f)
        {
            InitGameObject(_scene, _name);
            position = _pos;
            normals = _normalMap;
            diffuse = _diff;
            transparency = _transp;
            shaded = new ColorMatrix(_diff.size[0], _diff.size[1]);
        }

        public RefractionSprite2D(Scene _scene, Vector3 _pos, string _normalMap, ColorMatrix _diff, string _name, float _transp = 0.5f)
        {
            InitGameObject(_scene, _name);
            position = _pos;
            normals = Utils.NormalMapFromImage(_normalMap);
            diffuse = _diff;
            transparency = _transp;
            shaded = new ColorMatrix(_diff.size[0], _diff.size[1]);
        }

        public RefractionSprite2D(Scene _scene, Vector3 _pos, string _normalMap, string _diff, string _name, float _transp = 0.5f)
        {
            InitGameObject(_scene, _name);
            position = _pos;
            normals = Utils.NormalMapFromImage(_normalMap);
            diffuse = ColorMatrix.FromImage(_diff);
            transparency = _transp;
            shaded = new ColorMatrix(diffuse.size[0], diffuse.size[1]);
        }

        public RefractionSprite2D(Scene _scene, Vector3 _pos, Vector3[,] _normalMap, string _diff, string _name, float _transp = 0.5f)
        {
            InitGameObject(_scene, _name);
            position = _pos;
            normals = _normalMap;
            diffuse = ColorMatrix.FromImage(_diff);
            transparency = _transp;
            shaded = new ColorMatrix(diffuse.size[0], diffuse.size[1]);
        }

        // Getter methods

        public Vector2 GetPosition()
        {
            return new Vector2(position.X, position.Y);
        }

        public Vector3 GetFullPosition()
        {
            return position;
        }

        // Setter methods

        public void SetPos(Vector2 _pos)
        {
            position.X = _pos.X;
            position.Y = _pos.Y;
        }

        public void SetPos(Vector3 _pos)
        {
            position = _pos;
        }

        public void SetBGDist(float _dist)
        {
            position.Z = _dist;
        }

        // Shading methods
        private Color GetRefractedColor(int _x, int _y)
        {
            Vector3 invertedNormal = normals[_y, _x] * -1 * position.Z;
            Color primaryColor = scene.GetViewport().GetPx((int)(invertedNormal.X + position.X), (int)(invertedNormal.Y + position.Y));
            Color fragmentDiffuse = diffuse.GetPx(_x, _y);
            Color finalColor = Utils.MixColor(primaryColor, 
                Color.FromArgb(
                    (int)(transparency * 255), 
                    fragmentDiffuse.R, 
                    fragmentDiffuse.G, 
                    fragmentDiffuse.B
                )
            );
            return finalColor;
        }

        private void Shade()
        {
            for(int y = 0; y < diffuse.size[1]; y++)
            {
                for(int x = 0; x < diffuse.size[0]; x++)
                {
                    shaded.SetPx(x, y, GetRefractedColor(x, y));
                }
            }
        }

        // Engine methods

        public override void onUpdate(float dt)
        {
            Shade();
            scene.GetViewport().Copy(new Vector2(position.X, position.Y), shaded);
        }
    }
}
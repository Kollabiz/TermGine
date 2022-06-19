using TermGine.Core;
using System.Drawing;

namespace TermGine
{
    class BumpMaterial2D: RenderMaterial
    {
        private ColorMatrix diffuse;
        private Vector3[,] normals;

        public BumpMaterial2D(Scene _scene, string _name, ColorMatrix diff, Vector3[,] normalMap): base(_scene, _name)
        {
            diffuse = diff;
            normals = normalMap;
        }

        public BumpMaterial2D(Scene _scene, string _name, string diff, Vector3[,] normalMap): base(_scene, _name)
        {
            diffuse = ColorMatrix.FromImage(diff);
            normals = normalMap;
        }

        public BumpMaterial2D(Scene _scene, string _name, string diff, string normalMap): base(_scene, _name)
        {
            diffuse = ColorMatrix.FromImage(diff);
            normals = Utils.NormalMapFromImage(normalMap);
        }

        public BumpMaterial2D(Scene _scene, string _name, ColorMatrix diff, string normalMap): base(_scene, _name)
        {
            diffuse = diff;
            normals = Utils.NormalMapFromImage(normalMap);
        }

        // Shading

        public override void Shade(ColorMatrix matrix, Vector3 bodyPosition)
        {
            for(int y = 0; y < matrix.size[1]; y++)
            {
                for(int x = 0; x < matrix.size[0]; x++)
                {
                    Color diff = diffuse.GetPx(x, y);
                    Vector3 normal = normals[y, x];
                    AmbientLight light = getScene().GetAmbientLight();
                    float dot = normal.Dot((light.direction + light.position).Normalized());
                    Color shade = Color.FromArgb((byte)(Math.Clamp(diff.R * dot, 0f, 255f)), (byte)(Math.Clamp(diff.G * dot, 0f, 255f)), (byte)(Math.Clamp(diff.B * dot, 0f, 255f)));
                    matrix.SetPx(x, y, shade);
                }
            }
        }
    }
}
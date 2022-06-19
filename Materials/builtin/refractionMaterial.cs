using TermGine.Core;
using System.Drawing;

namespace TermGine
{
    class RefractionMaterial2D: Core.RenderMaterial
    {
        private ColorMatrix diffuse;
        private Vector3[,] normals;
        private float transparency;
        private float distance;

        public RefractionMaterial2D(Scene _scene, string _name, ColorMatrix diff, float dist, Vector3[,] normalMap, float transp): base(_scene, _name)
        {
            diffuse = diff;
            normals = normalMap;
            transparency = transp;
            distance = dist;
        }

        public RefractionMaterial2D(Scene _scene, string _name, string diff, float dist, Vector3[,] normalMap, float transp): base(_scene, _name)
        {
            diffuse = ColorMatrix.FromImage(diff);
            normals = normalMap;
            transparency = transp;
            distance = dist;
        }

        public RefractionMaterial2D(Scene _scene, string _name, string diff, float dist, string normalMap, float transp): base(_scene, _name)
        {
            diffuse = ColorMatrix.FromImage(diff);
            normals = Utils.NormalMapFromImage(normalMap);
            transparency = transp;
            distance = dist;
        }

        public RefractionMaterial2D(Scene _scene, string _name, ColorMatrix diff, float dist, string normalMap, float transp): base(_scene, _name)
        {
            diffuse = diff;
            normals = Utils.NormalMapFromImage(normalMap);
            transparency = transp;
        }

        private Color GetRefractedColor(int _x, int _y, Vector3 bodyPosition)
        {
            Vector3 invertedNormal = (normals[_y, _x] * -1 + bodyPosition + new Vector2((float)(_x), (float)(_y))) * distance;
            Color primaryColor = getScene().GetViewport().GetPx((int)(invertedNormal.X + bodyPosition.X), (int)(invertedNormal.Y + bodyPosition.Y));
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

        public override void Shade(ColorMatrix matrix, Vector3 bodyPosition)
        {
            for(int y = 0; y < matrix.size[1]; y++)
            {
                for(int x = 0; x < matrix.size[0]; x++)
                {
                    if(diffuse.GetPx(x, y).A == 0)
                    {
                        continue;
                    }
                    matrix.SetPx(x, y, Utils.MixColor(getScene().GetViewport().GetPx(x, y), GetRefractedColor(x, y, bodyPosition))); 
                }
            }
        }
    }
}
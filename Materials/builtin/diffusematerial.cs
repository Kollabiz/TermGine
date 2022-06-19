using TermGine.Core;

namespace TermGine
{
    class DiffuseMaterial2D: RenderMaterial
    {
        private ColorMatrix diffuse;

        public DiffuseMaterial2D(Scene _scene, string _name, ColorMatrix diff): base(_scene, _name)
        {
            diffuse = diff;
        }

        public DiffuseMaterial2D(Scene _scene, string _name, string diff): base(_scene, _name)
        {
            diffuse = ColorMatrix.FromImage(diff);
        }

        public override void Shade(ColorMatrix matrix, Vector3 bodyPosition)
        {
            for(int y = 0; y < matrix.size[1]; y++)
            {
                for(int x = 0; x < matrix.size[0]; x++)
                {
                    matrix.SetPx(x, y, diffuse.GetPx(x, y));
                }
            }
        }
    }
}
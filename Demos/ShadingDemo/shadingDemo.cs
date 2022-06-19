using TermGine;
using TermGine.Core;
using System.Drawing;

namespace ShadingDemo
{
    class SDemo
    {
        public static void Main()
        {
            Console.ReadKey();
            Scene scene = new Scene(32, 16, 1f);
            scene.SetHeader("Shading test");
            RenderMaterial bg = new DiffuseMaterial2D(scene, "background", "grass.png");
            RenderMaterial mat = new BumpMaterial2D(scene, "bump1", "sphere_diff.png", "sphere_norm.png");
            Sprite background = new Sprite(scene, 16, 16, new Vector2(0, 0), bg, "bg");
            Sprite bg2 = new Sprite(scene, 16, 16, new Vector2(16, 0), bg, "bg2");
            Sprite sphere = new Sprite(scene, 16, 16, new Vector2(0, 0), mat, "sphere");
            scene.SetAmbientLightDirection(new Vector3(-32, -32, 0));

            scene.Start();
            while(!scene.IsStopped())
            {
                Vector2 MPos = Mouse.GetPos();
                scene.SetAmbientLightDirection(new Vector3(-MPos.X / 100, -MPos.Y / 100, 15));
            }
        }
    }
}
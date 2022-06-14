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
            Scene scene = new Scene(16, 16, 1f);
            scene.SetHeader("Shading test");
            Pseudo3DSprite sphere = new Pseudo3DSprite(scene, "sphere_norm.png", "sphere_diff.png", "sphere");
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
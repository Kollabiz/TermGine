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
            scene.header = "Shading test";
            Pseudo3DSprite sphere = new Pseudo3DSprite(scene, "sphere_norm.png", "sphere_diff.png");
            scene.SetAmbientLightDirection(new Vector3(-32, -32, 0));

            scene.Start();
            while(!scene.Stopped)
            {
                Vector2 MPos = Mouse.GetPos();
                scene.SetAmbientLightDirection(new Vector3(-MPos.X / 100, -MPos.Y / 100, 15));
                scene.header = $"{-MPos.X / 100 + MPos.X / 10}; {-MPos.Y / 100 + MPos.Y / 10}                         ";
            }
        }
    }
}
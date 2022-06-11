namespace TermGine
{
    class Sprite: Core.GameObject
    {
        public Core.ColorMatrix matrix;
        public Core.Vector2 position;

        public Sprite(Scene _scene, string imgPath, Core.Vector2 _position)
        {
            InitGameObject(_scene);
            matrix = Core.ColorMatrix.FromImage(imgPath);
            position = _position;
        }

        public Sprite() {}

        public Sprite(Scene _scene, string imgPath, int x, int y)
        {
            InitGameObject(_scene);
            matrix = Core.ColorMatrix.FromImage(imgPath);
            position = new Core.Vector2(x, y);
        }

        public override void onUpdate(float _dt)
        {
            scene.surface.Copy(position, matrix);
        }
    }
}
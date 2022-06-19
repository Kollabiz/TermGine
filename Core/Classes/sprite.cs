namespace TermGine
{
    ///<summary>
    ///Class <c>Sprite</c> implements plain
    ///sprite in 2D space
    ///</summary>
    class Sprite: Core.GameObject
    {
        private Core.ColorMatrix matrix;
        private Core.Vector2 position;
        private Core.RenderMaterial material;

        public Sprite(Scene _scene, int sizeX, int sizeY, Core.Vector2 _position, Core.RenderMaterial _material, string _name)
        {
            InitGameObject(_scene, _name);
            matrix = new Core.ColorMatrix(sizeX, sizeY);
            position = _position;
            material = _material;
        }

        public Sprite() {}

        public Sprite(Scene _scene, Core.Vector2 size, Core.RenderMaterial _material, int x, int y, string _name)
        {
            InitGameObject(_scene, _name);
            matrix = new Core.ColorMatrix((int)(size.X), (int)(size.Y));
            position = new Core.Vector2(x, y);
            material = _material;
        }

        ///<summary>
        ///Method <c>OverrideMatrix</c> overrides
        ///sprite matrix on given one
        ///</summary>
        public void OverrideMatrix(Core.ColorMatrix _matrix)
        {
            matrix = _matrix;
        }

        ///<summary>
        ///Method <c>SetPosition</c> sets sprite
        ///position on given
        ///</summary>
        public void SetPosition(Core.Vector2 _pos)
        {
            position = _pos;
        }

        ///<summary>
        ///Method <c>SetPosition</c> sets sprite
        ///position on given
        ///</summary>
        public void SetPosition(int _x, int _y)
        {
            position.Set(_x, _y);
        }

        ///<summary>
        ///Method <c>GetMatrix</c> returns sprite
        ///matrix
        ///</summary>
        public Core.ColorMatrix GetMatrix()
        {
            return matrix;
        }

        ///<summary>
        ///Method <c>GetPosition</c> returns sprite
        ///position
        ///</summary>
        public Core.Vector2 GetPosition()
        {
            return position;
        }

        public override void onUpdate(float _dt)
        {
            material.Shade(matrix, new Core.Vector3(position.X, position.Y, 0));
            scene.GetViewport().Copy(position, matrix);
        }
    }
}
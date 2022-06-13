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
            scene.GetSurface().Copy(position, matrix);
        }
    }
}
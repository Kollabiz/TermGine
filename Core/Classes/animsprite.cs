namespace TermGine
{
    ///<summary>
    ///Class <c>AnimatedSprite</c> implements simple
    ///animated sprite
    ///</summary>
    class AnimatedSprite: Sprite
    {  
        public List<Core.ColorMatrix> frames;
        public float currentFrame = 0;
        public float animSpeed = 1f;

        public AnimatedSprite(Scene _scene, string folder, Core.Vector2 _position, string _name)
        {
            InitGameObject(_scene, _name);
            frames = new List<Core.ColorMatrix> {};
            foreach(string file in Directory.GetFiles(folder))
            {
                frames.Add(Core.ColorMatrix.FromImage(file));
            }
            SetPosition(_position);
            OverrideMatrix(frames[0]);
        }

        public override void onUpdate(float dt)
        {
            currentFrame += animSpeed;
            if(currentFrame >= frames.Count) {
                currentFrame = 0f;
            }

            OverrideMatrix(frames[(int)(Math.Floor(currentFrame))]);

            scene.GetViewport().Copy(GetPosition(), GetMatrix());
        }
    }
}
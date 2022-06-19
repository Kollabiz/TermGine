namespace TermGine.Core
{
    abstract class RenderMaterial
    {
        public string name;
        Scene scene;

        public RenderMaterial(Scene _scene, string _name)
        {
            scene = _scene;
            name = _name;
        }

        public Scene getScene()
        {
            return scene;
        }
        
        ///<summary>
        ///Shades given matrix
        ///</summary>
        public abstract void Shade(ColorMatrix matrix, Vector3 bodyPosition);
    }
}
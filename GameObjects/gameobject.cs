namespace TermGine.Core
{
    abstract class GameObject
    {
        public Scene scene;
        public List<string> ConnectedSignals;

        public void InitGameObject(Scene _scene) {
            scene = _scene;
            scene.AddObject(this);
            ConnectedSignals = new List<string> {};
        }

        public virtual void onUpdate(float dt) {}

        public virtual void Destroy() {}

        public virtual void OnSignalEmitted(string signal) {}
        public void EmitSignal(string signal) {
            scene.NewSignal(signal);
        }
    }
}
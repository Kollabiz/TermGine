using System.Linq;

namespace TermGine.Core
{
    abstract class GameObject
    {
        public Scene scene;
        public List<string> ConnectedSignals;
        private List<GameObject> Children;
        private string Name;

        public void InitGameObject(Scene _scene, string _name) {
            scene = _scene;
            scene.AddObject(this);
            ConnectedSignals = new List<string> {};
            Children = new List<GameObject>();
            Name = _name;
        }

        public virtual void onUpdate(float dt) {}

        public virtual void Destroy() {}

        public virtual void OnSignalEmitted(string signal) {}
        public void EmitSignal(string signal) 
        {
            scene.NewSignal(signal);
        }

        public string GetName()
        {
            return Name;
        }

        public GameObject? GetChild(string childAddr)
        {
            string[] caddr = childAddr.Split(".");
            foreach(GameObject child in Children)
            {
                if(child.Name == caddr[0])
                {
                    if(caddr.Length > 1)
                    {
                        return child.GetChild(string.Join(".", caddr.Skip(1)));
                    }
                    else
                    {
                        return child;
                    }
                }
            }
            return null;
        }

        public void SetName(string _name)
        {
            Name = _name;
        }
    }
}
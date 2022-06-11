using System.Drawing;

namespace TermGine
{
    class Scene
    {
        private List<Core.GameObject> objects;
        private double LastFrameTime;
        private float KeepDt;
        private AmbientLight ambientLight = new AmbientLight(Core.Vector2.ZERO, Core.Vector3.ZERO);

        public Color clearColor = Color.Black;

        public Core.ColorMatrix surface;

        public bool Stopped = true;
        private bool Paused = false;

        public string header = "TermGine project";

        public Scene(int sizeX, int sizeY, float frameTime) 
        {
            System.Console.WriteLine("DEBUG: initialized a new scene");
            objects = new List<Core.GameObject> {};
            surface = new Core.ColorMatrix(sizeX, sizeY);
            surface.Fill(clearColor);
            KeepDt = frameTime;
            LastFrameTime = Core.Utils.UnixNow();
        }

        public Scene(int sizeX, int sizeY) 
        {
            System.Console.WriteLine("DEBUG: initialized a new scene");
            objects = new List<Core.GameObject> {};
            surface = new Core.ColorMatrix(sizeX, sizeY);
            surface.Fill(clearColor);
            KeepDt = 0.5f;
            LastFrameTime = Core.Utils.UnixNow();
        }

        public void AddObject(Core.GameObject obj)
        {
            objects.Add(obj);
        }

        public void RemoveObject(Core.GameObject obj)
        {
            objects.Remove(obj);
        }

        public void Pause()
        {
            Paused = true;
        }

        public void UnPause()
        {
            Paused = false;
        }

        public List<Core.GameObject> GetObjects()
        {
            return objects;
        }

        public AmbientLight GetAmbientLight()
        {
            return ambientLight;
        }

        public void SetAmbientLightDirection(Core.Vector3 dir)
        {
            ambientLight.direction = dir.Normalized();
        }

        public void SetAmbientLightDirection(float x, float y, float z)
        {
            ambientLight.direction = new Core.Vector3(x, y, z).Normalized();
        }

        public void SetAmbientLightPosition(int x, int y)
        {
            ambientLight.position = new Core.Vector2(x, y);
        }

        public void SetAmbientLightPosition(Core.Vector2 pos)
        {
            ambientLight.position = pos;
        }

        public void Start() {
            System.Console.WriteLine("DEBUG: preparing to start scene loop");
            Stopped = false;
            var thread = new Thread(new ThreadStart(SceneLoop));
            System.Console.WriteLine("DEBUG: thread allocated. Starting");
            thread.Start();
            Console.Clear();
            System.Console.WriteLine("DEBUG: thread started successful");
            Console.CursorVisible = false;
        }

        private float CalcDeltaTime() {
            return (float)(Core.Utils.UnixNow() - LastFrameTime);
        }

        public void NewSignal(string signal) {
            System.Console.WriteLine($"DEBUG: emitted signal {signal}");
            foreach(Core.GameObject obj in objects)
            {
                if(obj.ConnectedSignals.Contains(signal))
                {
                    obj.OnSignalEmitted(signal);
                }
            }
        }

        private void SceneLoop()
        {
            System.Console.WriteLine("DEBUG: started a scene loop");
            while(!Stopped)
            {
                if(Paused)
                {
                    continue;
                }
                Update();
            }
            System.Console.WriteLine("DEBUG: ended a scene loop");
            Console.CursorVisible = true;
        }

        public void Update()
        {
            float dt = CalcDeltaTime();
            surface.Fill(clearColor);
            for(int i = 0; i < objects.Count; i++)
            {
                Core.GameObject obj = objects[i];
                obj.onUpdate(dt);
            }
            int diff = (int)((KeepDt - dt) * 100);
            if(diff > 0)
            {
                Thread.Sleep(diff);
            }
            System.Console.WriteLine(header);
            surface.Print();
            Console.SetCursorPosition(0, 1);
            LastFrameTime = Core.Utils.UnixNow();
        }
    }
}
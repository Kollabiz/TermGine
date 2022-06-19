using System.Drawing;

namespace TermGine
{
    ///<summary>
    ///Class <c>Scene</c> is a 2D scene
    ///used to create applications with
    ///multiple objects and make their
    ///each-other interaction easier
    ///</summary>
    class Scene
    {
        private List<Core.GameObject> objects;
        private double LastFrameTime;
        private float KeepDt;
        private AmbientLight ambientLight = new AmbientLight(Core.Vector2.ZERO, Core.Vector3.ZERO);

        private Color clearColor = Color.Black;
        private Core.ColorMatrix viewport;
        private bool Stopped = true;
        private bool Paused = false;

        private string header = "TermGine project";

        public Scene(int sizeX, int sizeY, float frameTime) 
        {
            System.Console.WriteLine("DEBUG: initialized a new scene");
            objects = new List<Core.GameObject> {};
            viewport = new Core.ColorMatrix(sizeX, sizeY);
            viewport.Fill(clearColor);
            KeepDt = frameTime;
            LastFrameTime = Core.Utils.UnixNow();
        }

        public Scene(int sizeX, int sizeY) 
        {
            System.Console.WriteLine("DEBUG: initialized a new scene");
            objects = new List<Core.GameObject> {};
            viewport = new Core.ColorMatrix(sizeX, sizeY);
            viewport.Fill(clearColor);
            KeepDt = 0.5f;
            LastFrameTime = Core.Utils.UnixNow();
        }

        ///<summary>
        ///Method <c>AddObject</c> adds given object
        ///to scene
        ///</summary>
        public void AddObject(Core.GameObject obj)
        {
            objects.Add(obj);
        }

        ///<summary>
        ///Method <c>RemoveObject</c> removes given
        ///object from scene
        ///</summary>
        public void RemoveObject(Core.GameObject obj)
        {
            objects.Remove(obj);
        }

        ///<summary>
        ///Method <c>Pause</c> temporarily stops
        ///main app thread
        ///</summary>
        public void Pause()
        {
            Paused = true;
        }

        ///<summary>
        ///Method <c>UnPause</c> starts main thread
        ///if it was paused
        public void UnPause()
        {
            Paused = false;
        }

        ///<summary>
        ///Method <c>GetObjects</c> returns list of
        ///scene objects
        ///</summary>
        public List<Core.GameObject> GetObjects()
        {
            return objects;
        }

        ///<summary>
        ///Method <c>GetAmbientLight</c> returns
        ///ambient light of scene
        ///</summary>
        public AmbientLight GetAmbientLight()
        {
            return ambientLight;
        }

        ///<summary>
        ///Method <c>GetSurface</c> returns scene
        ///viewport matrix
        ///</summary>
        public Core.ColorMatrix GetViewport()
        {
            return viewport;
        }

        ///<summary>
        ///Method <c>GetHeader</c> returns scene
        ///header
        ///</summary>
        public string GetHeader()
        {
            return header;
        }

        public Core.GameObject? GetNode(string childAddr)
        {
            string[] caddr = childAddr.Split(".");

            foreach(Core.GameObject node in objects)
            {
                if(node.GetName() == caddr[0])
                {
                    if(caddr.Length > 1)
                    {
                        return node.GetChild(string.Join(".", caddr.Skip(1)));
                    }
                    else
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        ///<summary>
        ///Method <c>IsStopped</c> returns current
        ///scene running state
        ///</summary>
        public bool IsStopped()
        {
            return Stopped;
        }

        ///<summary>
        ///Method <c>SetAmbientLightDirection</c>
        ///sets direction of scene ambient light.
        ///use it instead of GetAmbientLight().direction = ...
        ///</summary>
        public void SetAmbientLightDirection(Core.Vector3 dir)
        {
            ambientLight.direction = dir.Normalized();
        }

        ///<summary>
        ///Method <c>SetAmbientLightDirection</c>
        ///sets direction of scene ambient light.
        ///use it instead of <c>GetAmbientLight().direction = ...</c>
        ///</summary>
        public void SetAmbientLightDirection(float x, float y, float z)
        {
            ambientLight.direction = new Core.Vector3(x, y, z).Normalized();
        }

        ///<summary>
        ///Method <c>SetAmbientLightPosition</c>
        ///sets position of scene ambient light.
        ///use it instead of <c>GetAmbientLight().position = ...</c>
        ///</summary>
        public void SetAmbientLightPosition(int x, int y)
        {
            ambientLight.position = new Core.Vector2(x, y);
        }

        ///<summary>
        ///Method <c>SetAmbientLightPosition</c>
        ///sets position of scene ambient light.
        ///use it instead of <c>GetAmbientLight().position = ...</c>
        ///</summary>
        public void SetAmbientLightPosition(Core.Vector2 pos)
        {
            ambientLight.position = pos;
        }

        ///<summary>
        ///Method <c>SetHeader</c> sets scene
        ///header
        ///</summary>
        public void SetHeader(string _header)
        {
            header = _header;
        }

        ///<summary>
        ///Method <c>Start</c> starts scene in
        ///separate thread
        ///</summary>
        public void Start() 
        {
            System.Console.WriteLine("DEBUG: preparing to start scene loop");
            Stopped = false;
            var thread = new Thread(new ThreadStart(SceneLoop));
            System.Console.WriteLine("DEBUG: thread allocated. Starting");
            thread.Start();
            Console.Clear();
            System.Console.WriteLine("DEBUG: thread started successful");
            Console.CursorVisible = false;
        }

        ///<summary>
        ///Method <c>Interrupt</c> stops scene main
        ///thread
        ///</summary>
        public void Interrupt()
        {
            Stopped = true;
        }

        ///<summary>
        ///Method <c>CalcDeltaTime</c> calculates
        ///time since last frame
        ///</summary>
        private float CalcDeltaTime() 
        {
            return (float)(Core.Utils.UnixNow() - LastFrameTime);
        }

        ///<summary>
        ///Method <c>NewSignal</c> creates signal
        ///in scene
        ///</summary>
        public void NewSignal(string signal) 
        {
            System.Console.WriteLine($"DEBUG: emitted signal {signal}");
            foreach(Core.GameObject obj in objects)
            {
                if(obj.ConnectedSignals.Contains(signal))
                {
                    obj.OnSignalEmitted(signal);
                }
            }
        }

        // Scene main loop that run on separate thread
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
            System.Console.WriteLine("DEBUG: ended scene loop");
            Console.CursorVisible = true;
        }

        // Scene state update
        private void Update()
        {
            float dt = CalcDeltaTime();
            viewport.Fill(clearColor);
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
            viewport.Print();
            Console.SetCursorPosition(0, 1);
            LastFrameTime = Core.Utils.UnixNow();
        }
    }
}
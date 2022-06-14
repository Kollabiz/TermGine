using TermGine;
using System.Drawing;

namespace Demos
{
    // Bar class used to visualize sorting
    class BarClass: TermGine.Core.GameObject
    {
        private byte height;
        private TermGine.Core.Vector2 pos;
        private TermGine.Core.ColorMatrix matrix;

        public BarClass(Scene _scene, byte _height, string _name)
        {
            InitGameObject(_scene, _name);
            height = _height;
            matrix = new TermGine.Core.ColorMatrix(1, 60);
            for(byte y = 0; y <= height; y++)
            {
                matrix.SetPx(0, 59 - y, Color.White);
            }

            pos = new TermGine.Core.Vector2(0, 0);
        }

        public void setHeight(byte _height)
        {
            height = _height;
            matrix.Fill(Color.Black);
            for(byte y = 0; y <= height; y++)
            {
                matrix.SetPx(0, 59 - y, Color.White);
            }

        }

        public void setPos(int x)
        {
            pos = new TermGine.Core.Vector2(x, 0);
        }

        public byte getHeight()
        {
            return height;
        }

        public TermGine.Core.Vector2 getPos()
        {
            return pos;
        }

        public override void onUpdate(float dt)
        {
            scene.GetSurface().Copy(pos, matrix);
        }
    }

    class BarSorterClass: TermGine.Core.GameObject
    {
        public byte[]? bars;
        public BarClass[]? b;
        public int steps = 0;
        public double estTime = 0d;

        public BarSorterClass() {}

        public void delayedInit(Scene _scene, BarClass[] _b, string _name)
        {
            InitGameObject(_scene, _name);
            b = _b;
            bars = Other.NumbersFromBars(_b);
        }

        public virtual void Sort() {}

        public virtual string getSorterAlgorithmName() {return "";}

        public override void onUpdate(float _dt)
        {
            double time = TermGine.Core.Utils.UnixNow();
            Sort();
            estTime += TermGine.Core.Utils.UnixNow() - time;
            
            if(Other.IsSorted(bars))
            {
                Destroy();
            }
            Other.BarsFromInts(b, bars);
        }

        public override void Destroy()
        {
            scene.RemoveObject(this);
            Other.EndSort(this, bars.Length, scene);
        }
    }

    class BubbleSorter: BarSorterClass
    {
        public BubbleSorter() {}

        public override void Sort()
        {
            for(byte j = 1; j < bars.Length; j++)
            {
                if(bars[j - 1] > bars[j])
                {
                    byte _tmp = bars[j];
                    bars[j] = bars[j - 1];
                    bars[j - 1] = _tmp;
                }
                steps++;
            }
        }

        public override string getSorterAlgorithmName()
        {
            return "Bubble sort";
        }
    }

    class Info
    {
        public static void printStats(BarSorterClass sorter, int countOfBars)
        {
            System.Console.WriteLine(
            $"Count of elements: {countOfBars}\n " + 
            $"Sorter algorithm: {sorter.getSorterAlgorithmName()}\n" + 
            $"Count of steps: {sorter.steps}\n" +
            $"Estimated time: {sorter.estTime}"
            );
        }
    }

    class Other
    {
        private static Random rng = new Random();

        public static void BarsFromInts(BarClass[] bars, byte[] nums)
        {
            for(byte i = 0; i < nums.Length; i++)
            {
                bars[i].setHeight(nums[i]);
            }
        }

        public static BarClass[] CreateBars(byte count, Scene scene)
        {
            BarClass[] bars = new BarClass[count];
            for(byte i = 0; i < count; i++)
            {
                bars[i] = new BarClass(scene, i, $"bar_{i}");
                bars[i].setPos(i);
            }
            return bars;
        }

        public static bool IsSorted(byte[] values)
        {
            byte last = values[0];
            foreach(byte i in values)
            {
                if(i < last)
                {
                    return false;
                }
                else
                {
                    last = i;
                }
            }
            return true;
        }

        public static byte[] NumbersFromBars(BarClass[] bars)
        {
            byte[] nums = new byte[bars.Length];
            for(byte i = 0; i < bars.Length; i++)
            {
                nums[i] = bars[i].getHeight();
            }
            return nums;
        }

        public static void Shuffle(BarClass[] bars)
        {
             for(int i = 0; i < bars.Length; i++)
             {
                int index = (int)(rng.NextInt64(0, bars.Length));
                byte _tmp = bars[index].getHeight();
                bars[index].setHeight(bars[i].getHeight());
                bars[i].setHeight(_tmp);
             }
        }

        public static void EndSort(BarSorterClass sorter, int countOfBars, Scene scene)
        {
            Thread.Sleep(500);
            scene.Interrupt();
            Console.Clear();
            Thread.Sleep(500);
            Info.printStats(sorter, countOfBars);
            Console.ReadKey();
        }

    }

    class TheArtOfSorting
    {
        public static void MainTAOF()
        {
            System.Console.WriteLine("The Art Of Sorting\nBy Kollobz\n\n\nMake sure console is fullscreen and can handle 120\ncharacters in single line\n\nPress any key");
            System.Console.ReadKey();
            BarSorterClass[] sorters = 
            {
                new BubbleSorter()
            };

            Scene scene = new Scene(60, 60, 5f);
            scene.SetHeader("The Art Of Sorting");
            BarClass[] bars = Other.CreateBars(60, scene);

            foreach(BarSorterClass sorter in sorters)
            {
                Other.Shuffle(bars);
                sorter.delayedInit(scene, bars, "sorter");
                scene.SetHeader(sorter.getSorterAlgorithmName());
                scene.Start();
            }
        }
    }
}
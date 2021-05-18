using System;
using System.Collections.Generic;

namespace LameScooter
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine(args);
        }
    }

    public class Station {
        public int id { get; set; }
        public string name { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public int bikesAvailable { get; set; }
        public int spaceAvailable { get; set; }
        public int capacity { get; set; }
        public bool allowDropOff { get; set; }
        public bool allowOverloading { get; set; }
        public bool isFloatingBike { get; set; }
        public bool isCarStation { get; set; }
        public string state { get; set; }
        public List<string> networks { get; set; }
        public bool realTimeData { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LameScooter
{
    class Program
    {
        static async Task Main(string[] args) {
            ILameScooterRental rental = null;
            var count = await rental.GetScooterCountInStation(args[0]);
            Console.WriteLine($"Number of Scooters Available at {args[0]}: {count}");
        }
    }

    public class Station{
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
    public interface ILameScooterRental
    {
        Task<int> GetScooterCountInStation(string stationName);
    }
}

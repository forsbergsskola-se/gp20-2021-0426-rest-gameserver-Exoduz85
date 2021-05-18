using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter
{
    class Program
    {
        static async Task Main(string[] args) {
            ILameScooterRental rental = new OfflineLameScooterRental();
            var count = await rental.GetScooterCountInStation(args[0]);
            Console.WriteLine($"Number of Scooters Available at {args[0]}: {count}");
        }
    }
    public class OfflineLameScooterRental : ILameScooterRental {
        async Task<Stations> LoadStations() {
            using var sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "scooters.json"));
            return JsonSerializer.Deserialize<Stations>(await sr.ReadToEndAsync());
        }
        public Task<int> GetScooterCountInStation(string stationName) {
            var station = LoadStations().Result.stations.Where(a => a.name == stationName).ToArray();
            if(!station.Any()) throw new InvalidDataException($"{stationName} is not Valid");
            return Task.FromResult(station.Select(a => a.bikesAvailable).FirstOrDefault());
        }
    }

    public class Stations {
        public List<Station> stations { get; set; }
    }
    public class Station{
        public string id { get; set; }
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
        public string[] networks { get; set; }
        public bool realTimeData { get; set; }
    }
    public interface ILameScooterRental
    {
        Task<int> GetScooterCountInStation(string stationName);
    }
}

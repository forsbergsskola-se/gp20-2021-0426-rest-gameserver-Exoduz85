using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter {
    public class OfflineLameScooterRental : ILameScooterRental {
        async Task<Stations> LoadStations() {
            using var sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "scooters.json"));
            return JsonSerializer.Deserialize<Stations>(await sr.ReadToEndAsync());
        }
        public Task<int> GetScooterCountInStation(string stationName) {
            var station = LoadStations().Result.stations.Where(a => a.name == stationName).ToArray();
            if (!station.Any()) {
                var NotFoundException = new Exception($"Could not find: {stationName}");
                throw NotFoundException;
            }
            return Task.FromResult(station.Select(a => a.bikesAvailable).FirstOrDefault());
        }
    }
}
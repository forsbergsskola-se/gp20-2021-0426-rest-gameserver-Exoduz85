using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LameScooter {
    public class DeprecatedLameScooterRental : ILameScooterRental {
        async Task<List<Station>> LoadStations() {
            using var sr = new StreamReader(Path.Combine(Environment.CurrentDirectory, "scooters.txt"));
            var result= DeprecatedDeserialized(await sr.ReadToEndAsync());
            return result;
        }
        public Task<int> GetScooterCountInStation(string stationName) {
            var station = LoadStations().Result.Where(a => a.name == stationName).ToArray();
            if (station.Any()) return Task.FromResult(station.Select(a => a.bikesAvailable).FirstOrDefault());
            var NotFoundException = new Exception($"Could not find: {stationName}");
            throw NotFoundException;
        }
        List<Station> DeprecatedDeserialized(string data) {
            var result = new List<Station>();
            var regex = new Regex("(?<name>\\w*?)\\s*?:\\s*?(?<count>\\d*?)\\r\\n",RegexOptions.IgnoreCase);
            if (!regex.IsMatch(data)) return result;
            foreach(Match match in regex.Matches(data)) {
                var station = new Station{
                    name = match.Groups["name"].Value,
                    bikesAvailable = int.Parse(match.Groups["count"].Value)
                };
                result.Add(station);
            }
            return result;
        }
    }
}
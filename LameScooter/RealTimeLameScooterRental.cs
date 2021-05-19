using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter {
    public class RealTimeLameScooterRental : ILameScooterRental{
        public Task<int> GetScooterCountInStation(string stationName) {
            var station = ReadFile().Result.Where(a => a.name == stationName).ToArray();
            if (station.Any()) return Task.FromResult(station.Select(a => a.bikesAvailable).FirstOrDefault());
            var NotFoundException = new Exception($"Could not find: {stationName}");
            throw NotFoundException;
        }
        async Task<List<Station>> ReadFile() {
            var uri = new Uri("https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json");
            try {
                var client = new HttpClient
                {
                    DefaultRequestHeaders =
                    {
                        Accept = {new MediaTypeWithQualityHeaderValue("application/json")},
                        UserAgent = {ProductInfoHeaderValue.Parse("Exoduz85")},
                    }
                };
                var getString = await client.GetStringAsync(uri);
                client.Dispose();
                var result = JsonSerializer.Deserialize<Stations>(getString);
                return result.stations;
            }
            catch(HttpRequestException e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }

    public static class StringExtension {
        static UriBuilder StringUri(this string host, string toPath) {
            return new UriBuilder() {
                Host = host,
                Path = toPath,
                Scheme = "Https"
            };
        }
    }
}
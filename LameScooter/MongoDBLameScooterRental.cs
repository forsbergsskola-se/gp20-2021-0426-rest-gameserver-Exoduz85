using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace LameScooter {
    public class MongoDBLameScooterRental : ILameScooterRental{
        public Task<int> GetScooterCountInStation(string stationName) {
            try {
                var station = ReadMongoDB().Result.Where(a => a.name == stationName).ToArray();
                if(!station.Any()) throw new InvalidDataException($"{stationName} is not Valid");
                return Task.FromResult(station.Select(a => a.bikesAvailable).FirstOrDefault());
            }
            catch(InvalidDataException e) {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        async Task<List<Station>> ReadMongoDB() {
            try {
                var client = new MongoClient();
                var collection = await client.
                    GetDatabase("lamescooters").
                    GetCollection<BsonDocument>("lamescooters").
                    Find(new BsonDocument()).ToListAsync();
                return collection.Select(bsonDocument => BsonSerializer.Deserialize<Station>(bsonDocument)).ToList();
            }
            catch(MongoException e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
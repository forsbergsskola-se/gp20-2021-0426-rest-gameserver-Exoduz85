using MongoDB.Bson.Serialization.Attributes;

namespace LameScooter {
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class Station{
        public string id { get; set; }
        public string name { get; set; }
        public double x { get; set; }
        public double y { get; set; }
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
}
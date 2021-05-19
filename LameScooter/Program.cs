using System;
using System.Linq;
using System.Threading.Tasks;

namespace LameScooter
{
    class Program
    {
        static async Task Main(string[] args) {
            try {
                if (args[0].Any(char.IsDigit)) throw new ArgumentException("Argument may not contain any numbers");
                var validArgs = new [] {"offline", "deprecated", "realtime","mongodb"};
                if(!validArgs.Contains(args[1].ToLower()))throw new ArgumentException("Not a valid argument");
                ILameScooterRental rental = args[1].ToLower() switch {
                    "offline" => new OfflineLameScooterRental(),
                    "deprecated" => new DeprecatedLameScooterRental(),
                    "realtime" => new RealTimeLameScooterRental(),
                    "mongodb" => new MongoDBLameScooterRental(),
                    _ => null
                };
                var count = await rental.GetScooterCountInStation(args[0]);
                Console.WriteLine($"Number of Scooters Available at {args[0]}: {count}");
            }
            catch (ArgumentException e) {
                Console.WriteLine($"Invalid Argument: {e.Message}");
            }
        }
    }
}

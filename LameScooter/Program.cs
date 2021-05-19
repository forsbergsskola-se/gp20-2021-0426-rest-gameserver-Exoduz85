using System;
using System.Linq;
using System.Threading.Tasks;

namespace LameScooter
{
    class Program
    {
        static async Task Main(string[] args) {
            ILameScooterRental rental = new OfflineLameScooterRental();
            try {
                if (args[0].Any(char.IsDigit)) throw new ArgumentException("Argument may not contain any numbers");
                
                var count = await rental.GetScooterCountInStation(args[0]);
                Console.WriteLine($"Number of Scooters Available at {args[0]}: {count}");
            }
            catch (ArgumentException e) {
                Console.WriteLine($"Invalid Argument: {e.Message}");
            }
        }
    }
}

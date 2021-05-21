using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MMORPG {
    public partial class FileRepository : IRepository {
        const string filePath = "game-dev.txt";
        public async Task<Player> Get(Guid id) {
            var playerData = await File.ReadAllTextAsync(filePath);
            var players = JsonSerializer.Deserialize<Players>(playerData);
            return players?.ListOfPlayers.FirstOrDefault(player => player.Id == id);
        }
        public async Task<Player[]> GetAll() {
            var playerData = await File.ReadAllTextAsync(filePath);
            var players = JsonSerializer.Deserialize<Players>(playerData);
            return players?.ListOfPlayers.ToArray();
        }
        public async Task<Player> Create(Player player) {
            var playerData = await File.ReadAllTextAsync(filePath);
            var players = JsonSerializer.Deserialize<Players>(playerData);
            players?.ListOfPlayers.Add(player);
            var serialize = JsonSerializer.Serialize(players, typeof(Players));
            await File.WriteAllTextAsync(filePath, serialize);
            return player;
        }
        public async Task<Player> Modify(Guid id, ModifiedPlayer player) {
            var playerData = await File.ReadAllTextAsync(filePath);
            var players = JsonSerializer.Deserialize<Players>(playerData);
            foreach (var p in players.ListOfPlayers.Where(p => p.Id == id)) {
                p.Score = player.Score;
                var serialize = JsonSerializer.Serialize(players, typeof(Players));
                await File.WriteAllTextAsync(filePath, serialize);
                return p;
            }
            return null;
        }
        public Task<Player> Delete(Guid id) {
            throw new NotImplementedException();
        }
    }
}
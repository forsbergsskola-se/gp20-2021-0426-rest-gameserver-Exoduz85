using System.Collections.Generic;

namespace MMORPG {
    public partial class FileRepository {
        class Players {
            public List<Player> ListOfPlayers { get; set; } = new List<Player>();
        }
    }
}
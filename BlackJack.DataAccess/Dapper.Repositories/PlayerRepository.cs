using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities;

namespace BlackJack.DataAccess.Dapper.Repositories
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(string connectionString) : base(connectionString)
        {
        }
    }
}

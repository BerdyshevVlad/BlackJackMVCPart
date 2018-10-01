using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.DataAccess.Context.MVC;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities;

namespace BlackJack.DataAccess.Repositories
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(BlackJackContext context):base(context)
        {
        }
    }
}

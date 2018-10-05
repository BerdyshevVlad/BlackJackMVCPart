using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities;

namespace BlackJack.DataAccess.Interfaces
{
    public interface IPlayerCardRepository:IBaseRepository<PlayerCard>
    {
        Task AddCard(Player player, Card card, int currentRound);
        Task<List<PlayerCard>> GetAll();
    }
}

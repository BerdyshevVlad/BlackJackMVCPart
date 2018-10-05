using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.DataAccess.Context.MVC;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities;

namespace BlackJack.DataAccess.Repositories
{
    public class PlayerCardRepository : BaseRepository<PlayerCard>, IPlayerCardRepository
    {
        public PlayerCardRepository(BlackJackContext context):base(context)
        {
        }

        public async Task AddCard(Player player, Card card, int currentRound)
        {
            Player tmpPlayer = await _dbContext.Players.FindAsync(player.Id);
            Card tmpCard = await _dbContext.Cards.FindAsync(card.Id);

            var tmpPlayersCards = new PlayerCard();
            tmpPlayersCards.Card = tmpCard;
            tmpPlayersCards.Player = tmpPlayer;
            tmpPlayersCards.CurrentRound = currentRound;

            _dbContext.PlayersCards.Add(tmpPlayersCards);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<List<PlayerCard>> GetAll()
        {
            List<PlayerCard> playerCardsList =  _dbContext.PlayersCards.ToList();
            return playerCardsList;
        }
    }
}

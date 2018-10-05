using System.Linq;
using System.Threading.Tasks;
using BlackJack.DataAccess.Context.MVC;
using BlackJack.DataAccess.Interfaces;
using BlackJack.Entities;

namespace BlackJack.DataAccess.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(BlackJackContext context) : base(context)
        {

        }

        public async Task<bool> IsExist()
        {
            var isExist = _dbContext.Cards.Any();
            return isExist;
        }
    }
}

using System.Threading.Tasks;
using BlackJack.Entities;

namespace BlackJack.DataAccess.Interfaces
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        Task<bool> IsExist();
    }
}

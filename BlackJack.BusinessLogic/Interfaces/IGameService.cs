using System.Threading.Tasks;
using BlackJack.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameService
    {        
        Task<StartGameView> Start(SetNameAndBotCount userNameAndBotCount);
        Task<MoreGameView> More();
        Task<EnoughGameView> Enough();
        Task<HistoryGameView> GetHistory();
    }
}

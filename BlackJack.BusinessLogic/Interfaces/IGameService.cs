using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.Entities;
using BlackJack.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<int> DefineCurrentGame();
        int DefineCurrentRound();
        Task InitializePlayers(int game,string userName);
        Task<GetDeckGameView> GetDeck();
        Task SetBotCount(int botsCount,string userName);
        Task<Dictionary<Player, List<Card>>> DefinePlayersFromLastGame();
        int GenerateRandomValue();
        Task<bool> IsCardAlreadyDrawned(int randomValue);
        Task<Card> DrawCard();
        Task GiveCardToPlayer(Player player, Card card);
        Task GiveCardToEachPlayer();
        void CountSum(ref List<PlayerGameViewItem> playerViewItemList);
        Task<StartGameView> Start(SetNameAndBotCount userNameAndBotCount);
        Task<List<PlayerGameViewItem>> GetScoreCount();
        Task TakeCardIfNotEnough(bool takeCard);
        Task<MoreGameView> More();
        Task<EnoughGameView> Enough();
        Task<bool> IsGameEnded(bool takeCard);
        Task<bool> IsUserBusted();
        Task<List<PlayerGameViewItem>> DefineTheWinner();
        Task<HistoryGameView> GetHistory();
    }
}

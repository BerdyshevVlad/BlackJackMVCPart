using BlackJack.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.Entities;
using BlackJack.ViewModels;
using BlackJack.DataAccess.Enums;

namespace BlackJack.BusinessLogic.Services
{
    public class GameService : IGameService
    {

        public readonly ICardRepository _cardRepository;
        public readonly IPlayerRepository _playerRepository;
        public readonly IPlayerCardRepository _playerCardRepository;

        public int _round;
        public readonly string _personPlayerType = "Person";
        public readonly string _dealerPlayerType = "Dealer";

        public GameService(ICardRepository cardRepository, IPlayerRepository playerRepository, IPlayerCardRepository playerCardRepository)
        {
            _cardRepository = cardRepository;
            _playerRepository = playerRepository;
            _playerCardRepository = playerCardRepository;
            _round = DefineCurrentRound();
        }


        public async Task<int> DefineCurrentGame()
        {
            int currentRound = 0;
            try
            {
                var gamePlayersList = await _playerRepository.GetAllAsync();
                int maxGame = gamePlayersList.Max(x => x.GameNumber);
                if (maxGame > 0)
                {
                    currentRound = maxGame + 1;
                }
            }
            catch
            {
                currentRound = 1;
            }

            return currentRound;
        }


        public int DefineCurrentRound()
        {
            int _currentRound = 0;
            try
            {
                List<PlayerCard> gamePlayersList = _playerCardRepository.GetAll();
                int maxRound = gamePlayersList.Max(x => x.CurrentRound);
                if (maxRound > 0)
                {
                    _currentRound = maxRound;
                }
            }
            catch
            {
                _currentRound = 0;
            }

            return _currentRound;
        }


        public async Task InitializePlayers(int game, string userName)
        {
            var dealer = new Player();
            dealer.Name = "Dealer";
            dealer.PlayerType = _dealerPlayerType;
            dealer.GameNumber = game;
            var playerPerson = new Player();
            playerPerson.Name = userName;
            playerPerson.PlayerType = _personPlayerType;
            playerPerson.GameNumber = game;

            try
            {
                await _playerRepository.InsertAsync(dealer);
                await _playerRepository.InsertAsync(playerPerson);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<GetDeckGameView> GetDeck()
        {
            var cardsViewModel = new GetDeckGameView();
            try
            {
                IEnumerable<Card> cardListCollection = await _cardRepository.GetAllAsync();
                foreach (var card in cardListCollection)
                {
                    cardsViewModel.Cards.Add(new DeckViewItem
                    {
                        Id = card.Id,
                        Value = card.Value
                    });
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return cardsViewModel;
        }


        public async Task SetBotCount(int botsCount, string userName)
        {
            int gameNumber = await DefineCurrentGame();
            await InitializePlayers(gameNumber, userName);
            try
            {
                for (int i = 0; i < botsCount; i++)
                {
                    await _playerRepository.InsertAsync(new Player
                    { Name = $"Bot{i}", PlayerType = "Bot", GameNumber = gameNumber });
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task<Dictionary<Player, List<Card>>> DefinePlayersFromLastGame()
        {
            List<PlayerCard> playerCards = _playerCardRepository.GetAll();
            int max = playerCards.Max(x => x.Player.GameNumber);
            var playersLastGame = playerCards.Where(p => p.Player.GameNumber == _round).ToList();
            Dictionary<Player, List<Card>> playerCardsDictionary =new Dictionary<Player, List<Card>>();


            var count = playersLastGame.Where(x => x.CurrentRound == max)
                .GroupBy(x => x.PlayerId).Select(g => g.Key).Count();

            for (int j = 0; j < count; j++)
            {
                var player = playersLastGame.Where(x => x.Player.GameNumber == max)
                    .GroupBy(x => x.PlayerId).ToList();

                var cardList = player[j].Select(x=>x.Card).ToList();
                var playerTmp = player[j].Select(x => x.Player).FirstOrDefault();

                playerCardsDictionary.Add(playerTmp, cardList);
            }
            
            return playerCardsDictionary;
        }


        public int GenerateRandomValue()
        {
            int minDeckValue = 1;
            int maxDeckValue = 52;
            var random = new Random();

            int randomValue = random.Next(minDeckValue, maxDeckValue);
            return randomValue;
        }


        public async Task<bool> IsCardAlreadyDrawned(int randomValue)
        {
            IEnumerable<PlayerCard> playersCards = _playerCardRepository.GetAll();
            List<PlayerCard> playersOfCurrentRound = playersCards.Where(x => x.CurrentRound == _round).ToList();
            foreach (var pc in playersOfCurrentRound)
            {
                if (pc.CardId == randomValue)
                {
                    return true;
                }
            }

            return false;
        }


        public async Task<Card> DrawCard()
        {

            int randomValue = GenerateRandomValue();
            bool isCurrentCardDrawned = await IsCardAlreadyDrawned(randomValue);

            for (; isCurrentCardDrawned == true;)
            {
                randomValue = GenerateRandomValue();
                isCurrentCardDrawned = await IsCardAlreadyDrawned(randomValue);
            }

            Card card = await _cardRepository.GetByIdAsync(randomValue);

            return card;
        }


        public async Task GiveCardToPlayer(Player player, Card Card)
        {
            Player playerTmp = await _playerRepository.GetByIdAsync(player.Id);
            Card cardTmp = await _cardRepository.GetByIdAsync(Card.Id);
            await _playerCardRepository.AddCardAsync(playerTmp, cardTmp, _round);
        }


        public async Task GiveCardToEachPlayer()
        {
            IEnumerable<Player> playersList = await _playerRepository.GetAllAsync();

            int max = playersList.Max(x => x.GameNumber);
            IEnumerable<Player> playerList = playersList.ToList().Where(x => x.GameNumber == max);

            foreach (var player in playerList)
            {
                Card drawnedCard = await DrawCard();
                await GiveCardToPlayer(player, drawnedCard);
            }
        }


        public void CountSum(ref List<PlayerGameViewItem> playerViewItemList)
        {
            int scoreMaxValue = 21;
            int aceValueMax = 11;
            int aceValueMin = 1;
            foreach (var playerCards in playerViewItemList)
            {
                int sum = 0;
                for (int i = 0; i < playerCards.Cards.Count; i++)
                {
                    if (playerCards.Cards[i].Value == aceValueMax && (sum + aceValueMax) > scoreMaxValue)
                    {
                        sum += aceValueMin;
                    }
                    else
                    {
                        sum += playerCards.Cards[i].Value;
                    }
                }

                playerCards.Score = sum;
            }
        }


        public async Task<StartGameView> Start(SetNameAndBotCount SetNameAndBotCount)
        {
            await SetBotCount(SetNameAndBotCount.BotCount, SetNameAndBotCount.UserName);

            int handOverCount = 2;
            _round++;

            for (int i = 0; i < handOverCount; i++)
            {
                await GiveCardToEachPlayer();
            }

            Dictionary<Player, List<Card>> playerCardsLastGame = await DefinePlayersFromLastGame();
            List<PlayerGameViewItem> playerViewItemList = new List<PlayerGameViewItem>();

            foreach (var player in playerCardsLastGame)
            {
                PlayerGameViewItem playerViewItem = new PlayerGameViewItem();
                playerViewItem.Id = player.Key.Id;
                playerViewItem.Name = player.Key.Name;
                playerViewItem.GameNumber = player.Key.GameNumber;
                playerViewItem.PlayerType = player.Key.PlayerType;
                foreach (var card in player.Value)
                {
                    playerViewItem.Cards.Add(new CardViewItem { Id = card.Id, Value = card.Value });
                }

                playerViewItemList.Add(playerViewItem);
            }

            CountSum(ref playerViewItemList);

            StartGameView startViewModel = new StartGameView();
            startViewModel.Players = playerViewItemList;

            return startViewModel;
        }


        public async Task<List<PlayerGameViewItem>> GetScoreCount()
        {
            Dictionary<Player, List<Card>> playerCardsLastGame = await DefinePlayersFromLastGame();

            var playerViewItemList = new List<PlayerGameViewItem>();
            foreach (var player in playerCardsLastGame)
            {
                //IEnumerable<Card> cardsList = await _playerRepository.GetAllCardsFromPlayer(player.Key.Id, _round);
                PlayerGameViewItem playerViewItem = new PlayerGameViewItem();
                playerViewItem.Id = player.Key.Id;
                playerViewItem.Name = player.Key.Name;
                playerViewItem.GameNumber = player.Key.GameNumber;
                playerViewItem.PlayerType = player.Key.PlayerType;
                foreach (var card in player.Value)
                {
                    playerViewItem.Cards.Add(new CardViewItem { Id = card.Id, Value = card.Value });
                }

                playerViewItemList.Add(playerViewItem);
            }

            CountSum(ref playerViewItemList);

            return playerViewItemList;
        }


        public async Task TakeCardIfNotEnough(bool takeCard)
        {
            int scoreCountToStop = 17;
            List<PlayerGameViewItem> playerViewItemList = await GetScoreCount();

            foreach (var playerView in playerViewItemList)
            {
                if (playerView.Score < scoreCountToStop && playerView.PlayerType != _personPlayerType)
                {
                    Player player = await _playerRepository.GetByIdAsync(playerView.Id);
                    Card card = await DrawCard();
                    await GiveCardToPlayer(player, card);
                }

                if (playerView.PlayerType == _personPlayerType && takeCard)
                {
                    Player player = await _playerRepository.GetByIdAsync(playerView.Id);
                    Card card = await DrawCard();
                    await GiveCardToPlayer(player, card);
                }
            }
        }


        public async Task<MoreGameView> More()
        {
            bool takeCard = false;
            bool more = true;
            bool isGameEnded = await IsGameEnded(takeCard);
            if (!isGameEnded)
            {
                await TakeCardIfNotEnough(more);
            }
            var isUserBusted = await IsUserBusted();
            if (isUserBusted)
            {
                EnoughGameView tmp = await Enough();
                MoreGameView moreModel = new MoreGameView();
                moreModel.Players = tmp.Players;
                return moreModel;
            }

            Dictionary<Player, List<Card>> playerCardsLastGame = await DefinePlayersFromLastGame();

            List<PlayerGameViewItem> playerViewItemList = new List<PlayerGameViewItem>();

            foreach (var player in playerCardsLastGame)
            {
                PlayerGameViewItem playerViewItem = new PlayerGameViewItem();
                playerViewItem.Id = player.Key.Id;
                playerViewItem.Name = player.Key.Name;
                playerViewItem.GameNumber = player.Key.GameNumber;
                playerViewItem.PlayerType = player.Key.PlayerType;

                int cardCountForDealerMax = 2;
                int cardCountForDealerCurrent = 0;
                foreach (var card in player.Value)
                {
                    if (player.Key.PlayerType == _dealerPlayerType && cardCountForDealerCurrent < cardCountForDealerMax)
                    {
                        playerViewItem.Cards.Add(new CardViewItem { Id = card.Id, Value = card.Value });
                        cardCountForDealerCurrent++;
                    }
                    if (player.Key.PlayerType != _dealerPlayerType)
                    {
                        playerViewItem.Cards.Add(new CardViewItem { Id = card.Id, Value = card.Value });
                    }
                }

                playerViewItemList.Add(playerViewItem);
            }

            CountSum(ref playerViewItemList);

            MoreGameView moreViewModel = new MoreGameView();
            moreViewModel.Players = playerViewItemList;

            return moreViewModel;
        }



        public async Task<EnoughGameView> Enough()
        {
            bool takeCard = true;
            bool enough = false;
            bool isGameEnded = await IsGameEnded(takeCard);
            for (; !isGameEnded;)
            {
                await TakeCardIfNotEnough(enough);
                isGameEnded = await IsGameEnded(takeCard);
            }

            Dictionary<Player, List<Card>> playerCardsLastGame = await DefinePlayersFromLastGame();

            List<PlayerGameViewItem> playerViewItemList = new List<PlayerGameViewItem>();

            foreach (var player in playerCardsLastGame)
            {
                PlayerGameViewItem playerViewItem = new PlayerGameViewItem();
                playerViewItem.Id = player.Key.Id;
                playerViewItem.Name = player.Key.Name;
                playerViewItem.GameNumber = player.Key.GameNumber;
                playerViewItem.PlayerType = player.Key.PlayerType;
                foreach (var card in player.Value)
                {
                    playerViewItem.Cards.Add(new CardViewItem { Id = card.Id, Value = card.Value });
                }

                playerViewItemList.Add(playerViewItem);
            }

            CountSum(ref playerViewItemList);

            EnoughGameView moreOrEnoughViewModel = new EnoughGameView();
            moreOrEnoughViewModel.Players = playerViewItemList;

            return moreOrEnoughViewModel;
        }


        public async Task<bool> IsGameEnded(bool takeCard)
        {
            int scoreCountToStop = 17;
            int maxWinScor = 21;
            List<PlayerGameViewItem> playerViewItemList = await GetScoreCount();
            var cardCount = new List<int>();

            foreach (var playerScore in playerViewItemList.Where(x => x.PlayerType != _personPlayerType))
            {
                int scoreValue = playerScore.Score;
                cardCount.Add(scoreValue);
            }

            var isGameEnded = cardCount.TrueForAll(c => c >= scoreCountToStop);
            PlayerGameViewItem playerViewItem =
                playerViewItemList.SingleOrDefault(x => x.PlayerType == _personPlayerType);
            int cardSumPlayerPerson = playerViewItem.Cards.Sum(c => c.Value);
            if (cardSumPlayerPerson < maxWinScor && !takeCard)
            {
                isGameEnded = false;
            }

            return isGameEnded;
        }


        public async Task<bool> IsUserBusted()
        {
            int scoreMaxValue = 21;

            List<PlayerGameViewItem> playerViewItemList = await GetScoreCount();
            CountSum(ref playerViewItemList);
            PlayerGameViewItem playerViewItem = playerViewItemList.SingleOrDefault(x => x.PlayerType == _personPlayerType);

            int score = playerViewItem.Score;
            if (score >= scoreMaxValue)
            {
                return true;

            }

            return false;
        }


        public async Task<List<PlayerGameViewItem>> DefineTheWinner()
        {
            int maxWinScor = 21;
            List<PlayerGameViewItem> playerViewItemList = await GetScoreCount();
            var max = playerViewItemList.Where(x => x.Score <= maxWinScor).Max(x => x.Score);


            var winners = playerViewItemList.Where(x => x.Score == max).ToList();

            return winners;
        }


        public async Task<HistoryGameView> GetHistory()
        {
            List<PlayerCard> playersList = _playerCardRepository.GetAll();
            int maxRound = playersList.Max(r => r.CurrentRound);
            var history = new HistoryGameView();
            var tmpPlayerItemList = new List<PlayerGameViewItem>();

            for (int i = 1; i <= maxRound; i++)
            {
                var count = playersList.Where(x => x.CurrentRound == i)
                    .GroupBy(x => x.PlayerId).Select(g => g.Key).Count();

                for (int j = 0; j < count; j++)
                {
                    var player = playersList.Where(x => x.Player.GameNumber == i)
                        .GroupBy(x => x.PlayerId).ToList();

                    var cardList = player[j].Select(x => x.Card).ToList();
                    var playerTmp = player[j].Select(x => x.Player).FirstOrDefault();

                    PlayerGameViewItem playerModel = new PlayerGameViewItem();
                    playerModel.Id = playerTmp.Id;
                    playerModel.Name = playerTmp.Name;
                    playerModel.GameNumber = playerTmp.GameNumber;
                    playerModel.PlayerType = playerTmp.PlayerType;
                    playerModel.Round = i;
                    for (int k = 0; k < cardList.Count; k++)
                    {

                        playerModel.Cards.Add(new CardViewItem
                        {
                            Id = cardList[k].Id,
                            Value = cardList[k].Value
                        });
                    }
                    playerModel.Score = playerModel.Cards.Sum(x => x.Value);
                    tmpPlayerItemList.Add(playerModel);
                }
            }

            CountSum(ref tmpPlayerItemList);
            history.Players = tmpPlayerItemList;

            return history;
        }
    }
}
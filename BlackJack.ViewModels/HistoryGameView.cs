using System.Collections.Generic;

namespace BlackJack.ViewModels
{
    public class HistoryGameView
    {
        public List<PlayerGameViewItem> Players { get; set; }

        public HistoryGameView()
        {
            Players = new List<PlayerGameViewItem>();
        }
    }
}

using System.Collections.Generic;

namespace BlackJack.ViewModels
{
    public class MoreGameView
    {
        public List<PlayerGameViewItem> Players { get; set; }

        public MoreGameView()
        {
            Players = new List<PlayerGameViewItem>();
        }
    }
}

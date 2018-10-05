using System.Collections.Generic;

namespace BlackJack.ViewModels
{
    public class EnoughGameView
    {
        public List<PlayerGameViewItem> Players { get; set; }

        public EnoughGameView()
        {
            Players = new List<PlayerGameViewItem>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

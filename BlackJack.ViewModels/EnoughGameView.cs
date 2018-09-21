using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

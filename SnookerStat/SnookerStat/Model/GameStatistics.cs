using System;
using System.Collections.Generic;
using System.Text;

namespace SnookerStat.Model
{
    public class GameStatistics
    {
        public GameStatistics()
        {
        }

        public int potsSuccessPlayer1 { get; set; }
        public int totalTriesPlayer1 { get; set; }
        public int potsSuccessPlayer2 { get; set; }
        public int totalTriesPlayer2 { get; set; }
        public int longSuccess1 { get; set; }
        public int longTotal1 { get; set; }
        public int longSuccess2 { get; set; }
        public int longTotal2 { get; set; }
        public int restSuccess1 { get; set; }
        public int restTotal1 { get; set; }
        public int restSuccess2 { get; set; }
        public int restTotal2 { get; set; }
        public int player1total { get; set; }
        public int player2total { get; set; }
        public int player1break { get; set; }
        public int player2break { get; set; }
        public int currentAmountRedPotted { get; set; }
    }
}

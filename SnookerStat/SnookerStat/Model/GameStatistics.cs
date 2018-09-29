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

        public int PotsSuccessPlayer1 { get; set; }
        public int PotalTriesPlayer1 { get; set; }
        public int PotsSuccessPlayer2 { get; set; }
        public int TotalTriesPlayer2 { get; set; }
        public int TotalTriesPlayer1 { get; set; }
        public int LongSuccess1 { get; set; }
        public int LongTotal1 { get; set; }
        public int LongSuccess2 { get; set; }
        public int LongTotal2 { get; set; }
        public int RestSuccess1 { get; set; }
        public int RestTotal1 { get; set; }
        public int RestSuccess2 { get; set; }
        public int RestTotal2 { get; set; }
        public int Player1total { get; set; }
        public int Player2total { get; set; }
        public int Player1break { get; set; }
        public int Player2break { get; set; }
        public int CurrentAmountRedPotted { get; set; }
        public int YellowPoint { get; set; }
        public int GreenPoint { get; set; }
        public int BluePoint { get; set; }
        public int BrownPoint { get; set; }
        public int PinkPoint { get; set; }
        public int BlackPoint { get; set; }
        public int GameNumber { get; set; }
    }
}

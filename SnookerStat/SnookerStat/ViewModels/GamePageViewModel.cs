using SnookerStat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace SnookerStat.ViewModels
{
    public class GamePageViewModel
    {
        private int player1Score, player2Score, player1Break, player2Break;

        private int totalPointsInGame = 147;
        private int currentPointsGained, currentAmountRedPotted;
        private string gameTextArea, gameScoreArea;
        private string playerStatsDisplayText;

        int potsSuccessPlayer1, totalTriesPlayer1, potsSuccessPlayer2, totalTriesPlayer2, longSuccess1, longTotal1,
            longSuccess2, longTotal2, restSuccess1, restTotal1, restSuccess2, restTotal2;

        private double average;
        private Boolean isPlayer1Turn;
        private Boolean isRest;
        private Boolean isLong;

        private Boolean yellow = false;
        private Boolean green = false;
        private Boolean blue = false;
        private Boolean brown = false;
        private Boolean pink = false;
        private Boolean black = false;

        private INavigation _navigation;

        GameStatistics _gameStatistics;

        //if red is false player can pot colored ball
        Boolean red = true;

        Boolean player1Turn = true;

        public GamePageViewModel(GameStatistics gameStatistics, INavigation navigation)
        {
            _navigation = navigation;
            AddOnePoint = new Command(() => AddPoints(1));
            AddTwoPoint = new Command(() => AddPoints(2));
            AddThreePoint = new Command(() => AddPoints(3));
            AddFourPoint = new Command(() => AddPoints(4));
            AddFivePoint = new Command(() => AddPoints(5));
            AddSixPoint = new Command(() => AddPoints(6));
            AddSevenPoint = new Command(() => AddPoints(7));
            MissedPot = new Command(PotMissed);
            GoToStats = new Command<object>(CheckStats);
            Rest = new Command(RestEnabled);
            Long = new Command(LongEnabled);

            potsSuccessPlayer1 = gameStatistics.potsSuccessPlayer1;
            totalTriesPlayer1 = gameStatistics.totalTriesPlayer1;
            potsSuccessPlayer2 = gameStatistics.potsSuccessPlayer2;
            totalTriesPlayer2 = gameStatistics.totalTriesPlayer2;
            longSuccess1 = gameStatistics.longSuccess1;
            longTotal1 = gameStatistics.longTotal1;
            longSuccess2 = gameStatistics.longSuccess2;
            longTotal2 = gameStatistics.longTotal2;
            restSuccess1 = gameStatistics.restSuccess1;
            restTotal1 = gameStatistics.restTotal1;
            restSuccess2 = gameStatistics.restSuccess2;
            restTotal2 = gameStatistics.restTotal2;
            player1Score = gameStatistics.player1total;
            player1Break = gameStatistics.player1break;
            player2Score = gameStatistics.player2total;
            player2Score = gameStatistics.player2break;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Command<object> GoToStats { get; set; }

        //update scores
        public int Player1Score {
            get {
                return player1Score;
            }
            set {
                player1Score += value;
                OnPropertyChanged();
            }
        }
        public int Player1Break {
            get {
                return player1Break;
            }
            set {
                player1Break += value;
                OnPropertyChanged();
            }
        }
        public int Player2Score {
            get {
                return player2Score;
            }
            set {
                player2Score += value;
                OnPropertyChanged();
            }
        }
        public int Player2Break {
            get {
                return player2Break;
            }
            set {
                player2Break += value;
                OnPropertyChanged();
            }
        }
        public int PotsSuccessPlayer1 {
            get {
                return _gameStatistics.potsSuccessPlayer1;
            }
            set {
                _gameStatistics.potsSuccessPlayer1 += 1;
                OnPropertyChanged();
            }
        }
        public int TotalTriesPlayer1 {
            get {
                return _gameStatistics.totalTriesPlayer1;
            }
            set {
                _gameStatistics.totalTriesPlayer1 += 1;
                OnPropertyChanged();
            }
        }
        public int PotsSuccessPlayer2 {
            get {
                return _gameStatistics.potsSuccessPlayer2;
            }
            set {
                _gameStatistics.potsSuccessPlayer2 += 1;
                OnPropertyChanged();
            }
        }
        public int TotalTriesPlayer2 {
            get {
                return _gameStatistics.totalTriesPlayer2;
            }
            set {
                _gameStatistics.totalTriesPlayer2 += 1;
                OnPropertyChanged();
            }
        }
        public string GameTextArea {
            get {
                return gameTextArea;
            }
            set {
                gameTextArea += value;
            }
        }

        public Command AddOnePoint { get; }
        public Command AddTwoPoint { get; }
        public Command AddThreePoint { get; }
        public Command AddFourPoint { get; }
        public Command AddFivePoint { get; }
        public Command AddSixPoint { get; }
        public Command AddSevenPoint { get; }
        public Command MissedPot { get; }
        public Command Long { get; }
        public Command Rest { get; }

        void AddPoints(int amount)
        {
            if (currentAmountRedPotted < 15 || totalPointsInGame < 147)
            {
                if (player1Turn)
                {
                    if (amount == 1)
                    {
                        Player1Score = amount;
                        Player1Break = amount;
                        AddPointsGained(amount);
                        CheckLongRest(isLong, isRest, player1Turn);
                        potsSuccessPlayer1 += 1;
                        totalTriesPlayer1 += 1;
                        currentAmountRedPotted += 1;
                        red = false;
                    }
                    if (!red && amount > 1)
                    {
                        Player1Score = amount;
                        Player1Break = amount;
                        AddPointsGained(amount);
                        CheckLongRest(isLong, isRest, player1Turn);
                        potsSuccessPlayer1 += 1;
                        totalTriesPlayer1 += 1;
                        red = true;
                    }



                }
                else
                {
                    if (amount == 1)
                    {
                        Player2Score = amount;
                        Player2Break = amount;
                        AddPointsGained(amount);
                        CheckLongRest(isLong, isRest, player1Turn);
                        potsSuccessPlayer2 += 1;
                        totalTriesPlayer2 += 1;
                        currentAmountRedPotted += 1;
                        red = false;
                    }
                    if (!red && amount > 1)
                    {
                        Player2Score = amount;
                        Player2Break = amount;
                        AddPointsGained(amount);
                        CheckLongRest(isLong, isRest, player1Turn);
                        potsSuccessPlayer1 += 1;
                        totalTriesPlayer1 += 1;
                        red = true;
                    }

                }
            }
            else if (currentAmountRedPotted == 15 && currentPointsGained == 140)
            {
                AddLastPoints();
            }

        }
        void AddPointsGained(int amount)
        {
            if (player1Turn)
            {
                currentPointsGained += amount;
            }
            else
            {
                currentPointsGained += amount;
            }

        }
        void CheckLongRest(Boolean isLong, Boolean isRest, Boolean player1Turn)
        {

            if (player1Turn)
            {
                if (isLong)
                {
                    longSuccess1 += 1;
                    longTotal1 += 1;
                    isLong = false;
                }

                if (isRest)
                {
                    restSuccess1 += 1;
                    restTotal1 += 1;
                    isLong = false;
                }
            }
            else
            {

                if (isLong)
                {
                    longSuccess2 += 1;
                    longTotal2 += 1;
                    isLong = false;
                }

                if (isRest)
                {
                    restSuccess2 += 1;
                    restTotal2 += 1;
                    isLong = false;
                }
            }
        }
        void PotMissed()
        {
            if (player1Turn)
            {
                player1Turn = false;
                player1Break = 0;
                Player1Break = 0;
                totalTriesPlayer1 += 1;
                if (isLong)
                {
                    longTotal1 += 1;
                    isLong = false;
                }

                if (isRest)
                {
                    restTotal1 += 1;
                    isRest = false;
                }
            }
            else
            {
                player1Turn = true;
                player2Break = 0;
                Player2Break = 0;
                totalTriesPlayer2 += 1;
                if (isLong)
                {
                    longTotal2 += 1;
                    isLong = false;
                }

                if (isRest)
                {
                    restTotal2 += 1;
                    isRest = false;
                }
            }
        }
        void RestEnabled()
        {
            isRest = true;
        }
        void LongEnabled()
        {
            isLong = true;
        }
        private void CheckStats(object sender)
        {
            GameStatistics stats = new GameStatistics();
            stats.potsSuccessPlayer1 = potsSuccessPlayer1;
            stats.totalTriesPlayer1 = totalTriesPlayer1;
            stats.potsSuccessPlayer2 = potsSuccessPlayer2;
            stats.totalTriesPlayer2 = totalTriesPlayer2;
            stats.longSuccess1 = longSuccess1;
            stats.longTotal1 = longTotal1;
            stats.longSuccess2 = longSuccess2;
            stats.longTotal2 = longTotal2;
            stats.restSuccess1 = restSuccess1;
            stats.restTotal1 = restTotal1;
            stats.restSuccess2 = restSuccess2;
            stats.restTotal2 = restTotal2;
            stats.player1total = player1Score;
            stats.player1break = player1Break;
            stats.player2total = player2Score;
            stats.player2break = player2Score;

            _navigation.PushAsync(new StatisticPage(stats));
        }
        void AddLastPoints()
        {
            if (!black)
            {
                if (player1Turn)
                {
                    Player1Score = 7;
                    Player1Break = 7;
                    currentPointsGained += 7;
                    black = true;

                    if (isLong)
                    {
                        longSuccess1 += 1;
                        longTotal1 += 1;
                        isLong = false;
                    }

                    if (isRest)
                    {
                        restSuccess1 += 1;
                        restTotal1 += 1;
                        isRest = false;
                    }
                }
                else
                {
                    Player2Score = 7;
                    Player2Break = 7;
                    currentPointsGained += 7;
                    black = true;

                    if (isLong)
                    {
                        longSuccess2 += 1;
                        longTotal2 += 1;
                        isLong = false;
                    }

                    if (isRest)
                    {
                        restSuccess2 += 1;
                        restTotal2 += 1;
                        isRest = false;
                    }
                }
            }
        }
    }
}

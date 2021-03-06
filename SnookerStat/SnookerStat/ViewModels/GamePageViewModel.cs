﻿using SnookerStat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SnookerStat.ViewModels
{
    public class GamePageViewModel : INotifyPropertyChanged
    {
        private int player1Score;
        private int player2Score;
        private int player1Break;
        private int player2Break;

        private string player1;
        private string player2;

        private int totalPointsInGame = 147;
       // private int currentPointsGained;
        private int currentAmountRedPotted;
        private int lastPoints;
        private string gameTextArea, gameScoreArea;
        private string playerStatsDisplayText;

        int potsSuccessPlayer1, totalTriesPlayer1, potsSuccessPlayer2, totalTriesPlayer2, longSuccess1, longTotal1,
            longSuccess2, longTotal2, restSuccess1, restTotal1, restSuccess2, restTotal2;

        private int gameNumber;
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

        //hack for last points.
        private int yellowPoint;
        private int greenPoint;
        private int bluePoint;
        private int brownPoint;
        private int pinkPoint;
        private int blackPoint;

        private INavigation _navigation;

        GameStatistics _gameStatistics;
        Players _players;

        //if red is false player can pot colored ball
        Boolean red = true;

        Boolean player1Turn = true;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public GamePageViewModel(GameStatistics gameStatistics, INavigation navigation, Players players)
        {
            _navigation = navigation;
            _players = players;
            player1 = players.Player1;
            player2 = players.Player2;
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

            potsSuccessPlayer1 = gameStatistics.PotsSuccessPlayer1;
            totalTriesPlayer1 = gameStatistics.TotalTriesPlayer1;
            potsSuccessPlayer2 = gameStatistics.PotsSuccessPlayer2;
            totalTriesPlayer2 = gameStatistics.TotalTriesPlayer2;
            longSuccess1 = gameStatistics.LongSuccess1;
            longTotal1 = gameStatistics.LongTotal1;
            longSuccess2 = gameStatistics.LongSuccess2;
            longTotal2 = gameStatistics.LongTotal2;
            restSuccess1 = gameStatistics.RestSuccess1;
            restTotal1 = gameStatistics.RestTotal1;
            restSuccess2 = gameStatistics.RestSuccess2;
            restTotal2 = gameStatistics.RestTotal2;
            player1Score = gameStatistics.Player1total;
            player1Break = gameStatistics.Player1break;
            player2Score = gameStatistics.Player2total;
            player2Score = gameStatistics.Player2break;
            currentAmountRedPotted = gameStatistics.CurrentAmountRedPotted;
            yellowPoint = gameStatistics.YellowPoint;
            greenPoint = gameStatistics.GreenPoint;
            bluePoint = gameStatistics.BluePoint;
            brownPoint = gameStatistics.BrownPoint;
            pinkPoint = gameStatistics.PinkPoint;
            blackPoint = gameStatistics.BlackPoint;
            gameNumber = gameStatistics.GameNumber;
        }

        public Command<object> GoToStats { get; set; }

        public string Player1 {
            get {
                return player1;
            }
            set {
                player1 = value;
                OnPropertyChanged();
            }
        }

        public string Player2 {
            get {
                return player2;
            }
            set {
                player2 = value;
                OnPropertyChanged();
            }
        }

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
                return _gameStatistics.PotsSuccessPlayer1;
            }
            set {
                _gameStatistics.PotsSuccessPlayer1 += 1;
                OnPropertyChanged();
            }
        }
        public int TotalTriesPlayer1 {
            get {
                return _gameStatistics.TotalTriesPlayer1;
            }
            set {
                _gameStatistics.TotalTriesPlayer1 += 1;
                OnPropertyChanged();
            }
        }
        public int PotsSuccessPlayer2 {
            get {
                return _gameStatistics.PotsSuccessPlayer2;
            }
            set {
                _gameStatistics.PotsSuccessPlayer2 += 1;
                OnPropertyChanged();
            }
        }
        public int TotalTriesPlayer2 {
            get {
                return _gameStatistics.TotalTriesPlayer2;
            }
            set {
                _gameStatistics.TotalTriesPlayer2 += 1;
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
            if (currentAmountRedPotted < 15)
            {
                if (player1Turn)
                {
                    if (amount == 1)
                    {
                        Player1Score = amount;
                        Player1Break = amount;
                        //AddPointsGained(amount);
                        CheckLongRest(isLong, isRest, player1Turn);
                        potsSuccessPlayer1 += 1;
                        totalTriesPlayer1 += 1;
                        currentAmountRedPotted += 1;
                        red = false;
                    }
                    //!red prevents to get more points before red is potted
                    if (!red && amount > 1)
                    {
                        Player1Score = amount;
                        Player1Break = amount;
                        //AddPointsGained(amount);
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
                        //AddPointsGained(amount);
                        CheckLongRest(isLong, isRest, player1Turn);
                        potsSuccessPlayer2 += 1;
                        totalTriesPlayer2 += 1;
                        currentAmountRedPotted += 1;
                        red = false;
                    }
                    //!red prevents to get more points before red is potted
                    if (!red && amount > 1)
                    {
                        Player2Score = amount;
                        Player2Break = amount;
                        //AddPointsGained(amount);
                        CheckLongRest(isLong, isRest, player1Turn);
                        potsSuccessPlayer1 += 1;
                        totalTriesPlayer1 += 1;
                        red = true;
                    }
                }
            }
            else if (currentAmountRedPotted == 15)
            {
                if(yellowPoint == 0 && amount == 2 
                    && greenPoint == 0)
                {
                    yellowPoint = 2;
                    AddLastPoints(amount);
                }
                if(greenPoint == 0 && amount == 3 
                    && yellowPoint > 0 && bluePoint == 0)
                {
                    greenPoint = 3;     
                    AddLastPoints(amount);
                }
                if(bluePoint == 0 && amount == 4
                    && brownPoint == 0 && greenPoint > 0)
                {
                    bluePoint = 4;
                    AddLastPoints(amount);
                }
                if(brownPoint == 0 && amount == 5
                    && pinkPoint == 0 && bluePoint > 0)
                {
                    brownPoint = 5;
                    AddLastPoints(amount);
                }
                if(pinkPoint == 0 && amount == 6
                    && blackPoint == 0 && brownPoint > 0)
                {
                    pinkPoint = 6;
                    AddLastPoints(amount);
                }
                if(blackPoint == 0 && amount == 7
                    && pinkPoint > 0)
                {
                    blackPoint = 7;
                    AddLastPoints(amount);
                }
            }

        }
        /*
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
        */
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
            //take stats to statistic page but also keep scores so it won't reset
            //this is basically hack as cache not used
            //TODO: some alternate method might be in place
            GameStatistics stats = new GameStatistics();
            stats.PotsSuccessPlayer1 = potsSuccessPlayer1;
            stats.TotalTriesPlayer1 = totalTriesPlayer1;
            stats.PotsSuccessPlayer2 = potsSuccessPlayer2;
            stats.TotalTriesPlayer2 = totalTriesPlayer2;
            stats.LongSuccess1 = longSuccess1;
            stats.LongTotal1 = longTotal1;
            stats.LongSuccess2 = longSuccess2;
            stats.LongTotal2 = longTotal2;
            stats.RestSuccess1 = restSuccess1;
            stats.RestTotal1 = restTotal1;
            stats.RestSuccess2 = restSuccess2;
            stats.RestTotal2 = restTotal2;
            stats.Player1total = player1Score;
            stats.Player1break = player1Break;
            stats.Player2total = player2Score;
            stats.Player2break = player2Score;
            stats.CurrentAmountRedPotted = currentAmountRedPotted;
            stats.YellowPoint = yellowPoint;
            stats.GreenPoint = greenPoint;
            stats.BluePoint = bluePoint;
            stats.BrownPoint = brownPoint;
            stats.PinkPoint = pinkPoint;
            stats.BlackPoint = blackPoint;
            stats.GameNumber = gameNumber;

            _navigation.PushAsync(new StatisticPage(stats, _players));
        }
        void AddLastPoints(int amount)
        {
            if(amount == 2)
            {
                AddLastBallPoints(2);
            }      
            if (amount == 3)
            {
                AddLastBallPoints(3);
            }

            if (amount == 4)
            {
                AddLastBallPoints(4);
            }

            if (amount == 5)
            {
                AddLastBallPoints(5);
            }

            if (amount == 6)
            {
                AddLastBallPoints(6);
            }

            if (amount == 7)
            {
                AddLastBallPoints(7);
                StartNewGame();
            }         

        }
        private void AddLastBallPoints(int point)
        {
            if(player1Turn)
            {
                Player1Score = point;
                Player1Break = point;
               // AddPointsGained(point);
            } else
            {
                Player2Score = point;
                Player2Break = point;
                // AddPointsGained(point);
            }
        }
        public async Task StartNewGame()
        {
            GameScores gameScores = new GameScores();
            if(gameNumber == 0)
            {
                gameNumber = 1;
            } else
            {
                gameNumber++;
            }
            await gameScores.SaveScore(player1, player2, player1Score, player2Score, player1Break, player2Break, gameNumber);
            ClearContent(0);
        }
        void ClearContent(int clear)
        {
            potsSuccessPlayer1 = clear;
            totalTriesPlayer1 = clear;
            potsSuccessPlayer2 = clear;
            totalTriesPlayer2 = clear;
            longSuccess1 = clear;
            longTotal1 = clear;
            longSuccess2 = clear;
            longTotal2 = clear;
            restSuccess1 = clear;
            restTotal1 = clear;
            restSuccess2 = clear;
            restTotal2 = clear;
            Player1Score = 0;
            Player1Break = 0;
            Player2Score = 0;
            Player2Score = 0;
        }
    }
}

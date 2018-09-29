using SnookerStat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace SnookerStat.ViewModels
{
    public class GameStatisticViewModel
    {
        private double averagePotsPlayer1;
        private double averagePotsPlayer2;
        private double averageLongPlayer1;
        private double averageLongPlayer2;
        private double averageRestPlayer1;
        private double averageRestPlayer2;
        //private int potsSuccessPlayer1, totalTriesPlayer1, longSuccessPlayer1, longTotalPlayer1, restSuccessPlayer1, restTotalPlayer1;
        //private int potsSuccessPlayer2, totalTriesPlayer2, longSuccessPlayer2, longTotalPlayer2, restSuccessPlayer2, restTotalPlayer2;

        private Boolean isLongEnabled;
        private Boolean isRestEnabled;
        private Boolean Player1Turn;

        private string calculateAverageFor;
        private string player1;
        private string player2;

        private INavigation _navigation;

        GameStatistics _gameStatistics;
        Players _player;

        public GameStatisticViewModel(GameStatistics gameStatistics, INavigation navigation, Players player)
        {
            _navigation = navigation;
            _gameStatistics = gameStatistics;
            _player = player;
            BackToGame = new Command<object>(GoBackToGame);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Command<object> BackToGame { get; set; }

        public string Player1 {
            set {
                player1 = _player.Player1;
                OnPropertyChanged();
            }
        }
        public string Player2 {
            set {
                player2 = _player.Player2;
                OnPropertyChanged();
            }
        }

        //update statistic for player1
        public string PotsPlayer1 {
            get {
                calculateAverageFor = "potsPlayer1";
                CalculateAverage(_gameStatistics.PotsSuccessPlayer1, _gameStatistics.TotalTriesPlayer1);

                return $"{averagePotsPlayer1}% ({_gameStatistics.PotsSuccessPlayer1}/{_gameStatistics.TotalTriesPlayer1})";
            }
        }
        public string LongPlayer1 {
            get {
                calculateAverageFor = "longPlayer1";
                CalculateAverage(_gameStatistics.LongSuccess1, _gameStatistics.LongTotal1);

                return $"{averageLongPlayer1}% ({_gameStatistics.LongSuccess1}/{_gameStatistics.LongTotal1})";
            }
        }
        public string RestPlayer1 {
            get {
                calculateAverageFor = "restPlayer1";
                CalculateAverage(_gameStatistics.RestSuccess1, _gameStatistics.RestTotal1);

                return $"{averageRestPlayer1}% ({_gameStatistics.RestSuccess1}/{_gameStatistics.RestTotal1})";
            }
        }
        public string PsntPlayer1 {
            get {
                return "";
            }
        }
        public string SafetyPlayer1 {
            get {
                return "";
            }
        }
        public string ShotTimePlayer1 {
            get {
                return "";
            }
        }
        public string TableTimePlayer1 {
            get {
                return "";
            }
        }
        public string EscapePlayer1 {
            get {
                return "";
            }
        }
        //update statistic for player2
        public string PotsPlayer2 {
            get {
                calculateAverageFor = "potsPlayer2";
                CalculateAverage(_gameStatistics.PotsSuccessPlayer2, _gameStatistics.TotalTriesPlayer2);

                return $"{averagePotsPlayer2}% ({_gameStatistics.PotsSuccessPlayer2}/{_gameStatistics.TotalTriesPlayer2})";
            }
        }
        public string LongPlayer2 {
            get {
                calculateAverageFor = "longPlayer2";
                CalculateAverage(_gameStatistics.LongSuccess2, _gameStatistics.LongTotal2);

                return $"{averagePotsPlayer2}% ({_gameStatistics.LongSuccess2}/{_gameStatistics.LongTotal2})";
            }
        }
        public string RestPlayer2 {
            get {
                calculateAverageFor = "restPlayer2";
                CalculateAverage(_gameStatistics.RestSuccess2, _gameStatistics.RestTotal2);

                return $"{averageRestPlayer1}% ({_gameStatistics.RestSuccess2}/{_gameStatistics.RestTotal2})";
            }
        }
        public void CalculateAverage(int success, int total)
        {
            if (calculateAverageFor.Equals("potsPlayer1"))
            {
                if (success == 0)
                {
                    averagePotsPlayer1 = 0;
                }
                else
                {
                    averagePotsPlayer1 = Math.Floor(((double)success / total) * 100);
                }
            }
            else if (calculateAverageFor.Equals("longPlayer1"))
            {
                if (success == 0)
                {
                    averageLongPlayer1 = 0;
                }
                else
                {
                    averageLongPlayer1 = Math.Floor(((double)success / total) * 100);
                }
            }
            else if (calculateAverageFor.Equals("restPlayer1"))
            {
                if (success == 0)
                {
                    averageRestPlayer1 = 0;
                }
                else
                {
                    averageRestPlayer1 = Math.Floor(((double)success / total) * 100);
                }
            }
            else if (calculateAverageFor.Equals("potsPlayer2"))
            {
                if (success == 0)
                {
                    averagePotsPlayer2 = 0;
                }
                else
                {
                    averagePotsPlayer2 = Math.Floor(((double)success / total) * 100);
                }
            }
            else if (calculateAverageFor.Equals("longPlayer2"))
            {
                if (success == 0)
                {
                    averageLongPlayer2 = 0;
                }
                else
                {
                    averageLongPlayer2 = Math.Floor(((double)success / total) * 100);
                }
            }
            else if (calculateAverageFor.Equals("restPlayer2"))
            {
                if (success == 0)
                {
                    averageRestPlayer2 = 0;
                }
                else
                {
                    averageRestPlayer2 = Math.Floor(((double)success / total) * 100);
                }
            }
        }
        public void GoBackToGame(object sender)
        {
            //take stats to statistic page but also keep scores so it won't reset
            //this is basically hack as cache not used
            //TODO: some alternate method might be in place
            GameStatistics stats = new GameStatistics();
            stats.PotsSuccessPlayer1 = _gameStatistics.PotsSuccessPlayer1;
            stats.TotalTriesPlayer1 = _gameStatistics.TotalTriesPlayer1;
            stats.PotsSuccessPlayer2 = _gameStatistics.PotsSuccessPlayer2;
            stats.TotalTriesPlayer2 = _gameStatistics.TotalTriesPlayer2;
            stats.LongSuccess1 = _gameStatistics.LongSuccess1;
            stats.LongTotal1 = _gameStatistics.LongTotal1;
            stats.LongSuccess2 = _gameStatistics.LongSuccess2;
            stats.LongTotal2 = _gameStatistics.LongTotal2;
            stats.RestSuccess1 = _gameStatistics.RestSuccess1;
            stats.RestTotal1 = _gameStatistics.RestTotal1;
            stats.RestSuccess2 = _gameStatistics.RestSuccess2;
            stats.RestTotal2 = _gameStatistics.RestTotal2;
            stats.Player1total = _gameStatistics.Player1total;
            stats.Player1break = _gameStatistics.Player1break;
            stats.Player2total = _gameStatistics.Player2total;
            stats.Player2break = _gameStatistics.Player2break;
            stats.CurrentAmountRedPotted = _gameStatistics.CurrentAmountRedPotted;

            var playGamePage = new PlayGamePage(stats, _player);
            NavigationPage.SetHasNavigationBar(playGamePage, false);

            _navigation.PushAsync(playGamePage);
        }
    }
}

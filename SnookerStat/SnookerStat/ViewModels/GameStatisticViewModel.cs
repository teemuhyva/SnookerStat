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

        private INavigation _navigation;

        GameStatistics _gameStatistics;

        public GameStatisticViewModel(GameStatistics gameStatistics, INavigation navigation)
        {
            _navigation = navigation;
            _gameStatistics = gameStatistics;
            BackToGame = new Command<object>(GoBackToGame);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Command<object> BackToGame { get; set; }

        //update statistic for player1
        public string PotsPlayer1 {
            get {
                calculateAverageFor = "potsPlayer1";
                CalculateAverage(_gameStatistics.potsSuccessPlayer1, _gameStatistics.totalTriesPlayer1);

                return $"{averagePotsPlayer1}% ({_gameStatistics.potsSuccessPlayer1}/{_gameStatistics.totalTriesPlayer1})";
            }
        }
        public string LongPlayer1 {
            get {
                calculateAverageFor = "longPlayer1";
                CalculateAverage(_gameStatistics.longSuccess1, _gameStatistics.longTotal1);

                return $"{averageLongPlayer1}% ({_gameStatistics.longSuccess1}/{_gameStatistics.longTotal1})";
            }
        }
        public string RestPlayer1 {
            get {
                calculateAverageFor = "restPlayer1";
                CalculateAverage(_gameStatistics.restSuccess1, _gameStatistics.restTotal1);

                return $"{averageRestPlayer1}% ({_gameStatistics.restSuccess1}/{_gameStatistics.restTotal1})";
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
                CalculateAverage(_gameStatistics.potsSuccessPlayer2, _gameStatistics.totalTriesPlayer2);

                return $"{averagePotsPlayer2}% ({_gameStatistics.potsSuccessPlayer2}/{_gameStatistics.totalTriesPlayer2})";
            }
        }
        public string LongPlayer2 {
            get {
                calculateAverageFor = "longPlayer2";
                CalculateAverage(_gameStatistics.longSuccess2, _gameStatistics.longTotal2);

                return $"{averagePotsPlayer2}% ({_gameStatistics.longSuccess2}/{_gameStatistics.longTotal2})";
            }
        }
        public string RestPlayer2 {
            get {
                calculateAverageFor = "restPlayer2";
                CalculateAverage(_gameStatistics.restSuccess2, _gameStatistics.restTotal2);

                return $"{averageRestPlayer1}% ({_gameStatistics.restSuccess2}/{_gameStatistics.restTotal2})";
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
            GameStatistics stats = new GameStatistics();
            stats.potsSuccessPlayer1 = _gameStatistics.potsSuccessPlayer1;
            stats.totalTriesPlayer1 = _gameStatistics.totalTriesPlayer1;
            stats.potsSuccessPlayer2 = _gameStatistics.potsSuccessPlayer2;
            stats.totalTriesPlayer2 = _gameStatistics.totalTriesPlayer2;
            stats.longSuccess1 = _gameStatistics.longSuccess1;
            stats.longTotal1 = _gameStatistics.longTotal1;
            stats.longSuccess2 = _gameStatistics.longSuccess2;
            stats.longTotal2 = _gameStatistics.longTotal2;
            stats.restSuccess1 = _gameStatistics.restSuccess1;
            stats.restTotal1 = _gameStatistics.restTotal1;
            stats.restSuccess2 = _gameStatistics.restSuccess2;
            stats.restTotal2 = _gameStatistics.restTotal2;
            stats.player1total = _gameStatistics.player1total;
            stats.player1break = _gameStatistics.player1break;
            stats.player2total = _gameStatistics.player2total;
            stats.player2break = _gameStatistics.player2break;

            var playGamePage = new PlayGamePage(stats);
            NavigationPage.SetHasNavigationBar(playGamePage, false);

            _navigation.PushAsync(playGamePage);
        }
    }
}

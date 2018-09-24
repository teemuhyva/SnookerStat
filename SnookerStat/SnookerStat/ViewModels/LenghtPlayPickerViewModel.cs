using SnookerStat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SnookerStat.ViewModels
{
    public class LenghtPlayPickerViewModel
    {
        Players _players;

        public LenghtPlayPickerViewModel(Players players)
        {
            HandyCap = AddHandyCapSelection();
            BestOff = AddHBestOffSelection();
            _players = players;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public List<GameSettings> HandyCap { get; set; }
        public List<GameSettings> BestOff { get; set; }
        public string Player1 {
            get {
                return _players.Player1;
            }
        }

        public string Player2 {
            get {
                return _players.Player2;
            }
        }
        public List<GameSettings> AddHandyCapSelection()
        {
            var handyCap = new List<GameSettings>()
            {
                new GameSettings() { HandyCap = "+1"},
                new GameSettings() { HandyCap = "+2"},
                new GameSettings() { HandyCap = "+3"},
                new GameSettings() { HandyCap = "+4"},
                new GameSettings() { HandyCap = "+5"},
                new GameSettings() { HandyCap = "+6"},
                new GameSettings() { HandyCap = "+7"}
            };

            return handyCap;

        }

        public List<GameSettings> AddHBestOffSelection()
        {
            var handyCap = new List<GameSettings>()
            {
                new GameSettings() { BestOff = 1},
                new GameSettings() { BestOff = 3},
                new GameSettings() { BestOff = 5}
            };

            return handyCap;
        }
    }
}

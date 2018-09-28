using SnookerStat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SnookerStat.ViewModels
{
    public class PlayerPickViewModel : INotifyPropertyChanged
    {
        private string findPlayerWithNick;
        private string player1;
        private string player2;
        private string playerNickFound;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string Players {
            get {
                return findPlayerWithNick;
            }
            set {
                findPlayerWithNick = value;
                OnPropertyChanged();
            }
        }

        public string Player1 {
            get {
                return player1;
            }
            set {
                if (player1 != value)
                {
                    player1 = value;
                }
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

        public string PlayerNickFound {
            get {
                return playerNickFound;
            }
            set {
                playerNickFound = value;
                if(playerNickFound == null)
                {
                    playerNickFound = "Player with nick not found";
                }
                OnPropertyChanged();
            }
        }

        public async Task FindPlayerByNick(string nick)
        {
            Players player = new Players();
            await player.FindPlayerByNick(nick);            
        }
    }
}

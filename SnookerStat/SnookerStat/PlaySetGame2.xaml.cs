using SnookerStat.Model;
using SnookerStat.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnookerStat
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlaySetGame2 : ContentPage
	{
        Players players = new Players();
        public PlaySetGame2 ()
		{
			InitializeComponent ();

            BindingContext = new PlayerPickViewModel();

            LoadPlayerList();
            //playerView.ItemsSource = players.PlayerList;
        }

        async void SetHandiCapPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlaySetGame3(players));
        }
        void ChooseItem(object sender, SelectedItemChangedEventArgs e)
        {

            string p1 = player1.Text;
            string p2 = player2.Text;

            if (e.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(p1))
                {
                    var selection = e.SelectedItem as Players;
                    player1.Text = selection.PlayerName;
                    players.Player1 = selection.PlayerName;

                }
                else if (string.IsNullOrEmpty(p2))
                {
                    var selection = e.SelectedItem as Players;
                    player2.Text = selection.PlayerName;
                    players.Player2 = selection.PlayerName;
                }

            }
        }

        async Task FindPlayerWithNick(object sender, EventArgs e)
        {
            PlayerPickViewModel findPlayer = new PlayerPickViewModel();
            string playerNick = EnteredNick.Text;
            if(playerNick != null)
            {
                await findPlayer.FindPlayerByNick(playerNick);
            }
        }

        async Task LoadPlayerList()
        {
            ObservableCollection<Players> playerList = await players.ListPreviousPlayedNicks();
            playerView.ItemsSource = playerList;
        }
    }
}
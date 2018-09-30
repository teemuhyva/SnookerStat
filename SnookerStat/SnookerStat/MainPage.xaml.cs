using SnookerStat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SnookerStat
{
    public partial class MainPage : ContentPage
    {
        LoginPlayer loginPlayer = new LoginPlayer();
        public MainPage(LoginPlayer loginUser)
        {
            InitializeComponent();
            loginPlayer.NickName = loginUser.NickName;
        }

        async void Play(object sender, EventArgs e)
        {
            var playSetGame = new PlaySetGame(loginPlayer);
            NavigationPage.SetHasNavigationBar(playSetGame, false);
            await Navigation.PushAsync(playSetGame);
        }
    }
}

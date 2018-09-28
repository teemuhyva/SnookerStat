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
        public MainPage()
        {
            InitializeComponent();
        }

        async void Play(object sender, EventArgs e)
        {
            var playSetGame = new PlaySetGame();
            NavigationPage.SetHasNavigationBar(playSetGame, false);
            await Navigation.PushAsync(playSetGame);
        }
    }
}

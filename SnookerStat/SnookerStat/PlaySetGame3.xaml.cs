using SnookerStat.Model;
using SnookerStat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnookerStat
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlaySetGame3 : ContentPage
	{
        GameStatistics gameStatistics = new GameStatistics();
        public PlaySetGame3 (Players players)
		{
			InitializeComponent ();
            BindingContext = new LenghtPlayPickerViewModel(players);
        }
        async void PlayGame(object sender, EventArgs e)
        {
            var playGamePage = new PlayGamePage(gameStatistics);
            NavigationPage.SetHasNavigationBar(playGamePage, false);
            await Navigation.PushAsync(playGamePage);

        }

        async void BackToSetGame2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlaySetGame2());
        }
    }
}
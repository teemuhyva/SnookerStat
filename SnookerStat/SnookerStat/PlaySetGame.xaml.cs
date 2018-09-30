using SnookerStat.Model;
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
	public partial class PlaySetGame : ContentPage
	{
        LoginPlayer _loginPlayer;

        public PlaySetGame (LoginPlayer loginPlayer)
		{
			InitializeComponent ();
            _loginPlayer = loginPlayer;
		}
        async void StartNewMatch(object sender, EventArgs e)
        {
            var playSetGame2 = new PlaySetGame2(_loginPlayer);
            NavigationPage.SetHasNavigationBar(playSetGame2, false);
            await Navigation.PushAsync(playSetGame2);
        }
    }
}
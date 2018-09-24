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
		public PlaySetGame ()
		{
			InitializeComponent ();
		}
        async void StartNewMatch(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlaySetGame2());
        }
    }
}
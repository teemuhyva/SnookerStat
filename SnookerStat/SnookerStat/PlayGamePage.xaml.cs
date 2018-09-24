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
	public partial class PlayGamePage : ContentPage
	{
		public PlayGamePage (GameStatistics gameStatistics)
		{
			InitializeComponent ();
            BindingContext = new GamePageViewModel(gameStatistics, Navigation);

        }
	}
}
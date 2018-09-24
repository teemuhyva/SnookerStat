﻿using SnookerStat.Model;
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
	public partial class StatisticPage : ContentPage
	{
		public StatisticPage (GameStatistics gameStatistics)
		{
			InitializeComponent ();
            BindingContext = new GameStatisticViewModel(gameStatistics, Navigation);
        }
	}
}
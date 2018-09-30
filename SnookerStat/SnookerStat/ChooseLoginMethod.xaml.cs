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
    public partial class ChooseLoginMethod : ContentPage
    {
        public ChooseLoginMethod()
        {
            InitializeComponent();
        }

        async void Login(object sender, EventArgs e)
        {
            var loginPage = new LoginPage();
            NavigationPage.SetHasNavigationBar(loginPage, false);
            await Navigation.PushAsync(loginPage);
        }

        async void Register(object sender, EventArgs e)
        {
            var registerPage = new RegisterPage();
            NavigationPage.SetHasNavigationBar(registerPage, false);
            await Navigation.PushAsync(registerPage);
        }
    }
}
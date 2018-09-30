using SnookerStat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnookerStat.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string nickName;
        private string password;
        private string notCorrectPassword;

        INavigation _navigation;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Login = new Command(async () => LoginExistingUser());
        }

        public string NickName {
            get {
                return nickName;
            }
            set {
                nickName = value;
                OnPropertyChanged();
            }
        }
        public string Password {
            get {
                return password;
            }
            set {
                password = value;
                OnPropertyChanged();
            }
        }

        public string NotCorrectPassword {
            get {
                return notCorrectPassword;
            }
            set {
                notCorrectPassword = value;
                OnPropertyChanged();
            }
        }

        public ICommand Login { get; set; }

        public async Task LoginExistingUser()
        {
            LoginPlayer login = new LoginPlayer();
            string salt = PasswordHash.CreateSalt(10);
            string hash = PasswordHash.GenerateSHA256Hash(Password, salt);

            var loginPlayer = new LoginPlayer
            {
                NickName = NickName,
                GivenPasswordHash = hash,
                GivenPassword = Password,
                GivenPasswordSalt = salt
            };

            await login.LoginAsExistingPlayer(loginPlayer);
            if (login.CorrectPassword.Equals("Password was correct"))
            {
                _navigation.PushAsync(new MainPage());
            }
            else
            {
                NotCorrectPassword = "Incorrect Password";
            }
        }
    }
}

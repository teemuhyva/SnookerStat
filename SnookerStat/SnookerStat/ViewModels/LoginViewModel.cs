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
        private string nickNameEmpty;
        private string password;
        private string passwordEmpty;
        private string notCorrectPassword;
        private string playerNotRegistered;

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
        public string NickNameEmpty {
            get {
                return nickNameEmpty;
            }
            set {
                nickNameEmpty = value;
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
        public string PasswordEmpty {
            get {
                return passwordEmpty;
            }
            set {
                passwordEmpty = value;
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

        public string PlayerNotRegistered {
            get {
                return playerNotRegistered;
            }
            set {
                playerNotRegistered = value;
                OnPropertyChanged();
            }
        }

        public ICommand Login { get; set; }

        public async Task LoginExistingUser()
        {

            if(NickName == null)
            {
                NickNameEmpty = "NickName required";
            } else if(Password == null)
            {
                PasswordEmpty = "Password required";
            } else
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
                if(login.NickName == null)
                {
                    PlayerNotRegistered = "Player not registered";
                } else
                {
                    if (login.CorrectPassword.Equals("Password was correct"))
                    {
                        _navigation.PushAsync(new MainPage(login));
                    }
                    else
                    {
                        NotCorrectPassword = "Incorrect Password";
                    }
                }               
            }            
        }
    }
}

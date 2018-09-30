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
    public class RegisterPlayerViewModel : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string nickName;
        private string email;
        private string password;
        private string emptyField;

        INavigation _navigation;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public RegisterPlayerViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Register = new Command(async() =>RegisterNewUser());
        }


        public string FirstName {
            get {
                return firstName;
            }
            set {
                firstName = value;
                OnPropertyChanged();
            }
        }
        public string EmptyField {
            get {
                return emptyField;
            }
            set {
                emptyField = value;
                OnPropertyChanged();
            }
        }
        public string LastName {
            get {
                return lastName;
            }
            set {
                lastName = value;
                OnPropertyChanged();
            }
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
        public string Email {
            get {
                return email;
            }
            set {
                email = value;
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

        public ICommand Register { get; set; }

        public async Task RegisterNewUser()
        {
            RegisterPlayer register = new RegisterPlayer();
            string salt = PasswordHash.CreateSalt(10);
            string hash = PasswordHash.GenerateSHA256Hash(Password, salt);

            if(FirstName == null || LastName == null || NickName == null || Email == null || Password == null)
            {
                EmptyField = "All Fields must be added";
            } else
            {
                var registerPlayer = new RegisterPlayer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    NickName = nickName,
                    Email = email,
                    Password = hash
                };

                await register.RegisterNewPlayer(registerPlayer);
                _navigation.PushAsync(new ChooseLoginMethod());
            }
        }
    }
}

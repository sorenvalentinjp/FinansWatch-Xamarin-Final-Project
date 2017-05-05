using Prototype.Database;
using Prototype.ModelControllers;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prototype.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly StateController _stateController;
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value) { return; }
                _email = value;
                Notify("Email");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value) { return; }
                _password = value;
                Notify("Password");
            }
        }


        public LoginViewModel(StateController stateController)
        {
            this._stateController = stateController;
            _stateController.LoginController.LoginAsync("michael.m.hansen@jp.dk", "1234qwer");

            _stateController.LoginController.LoginErrorOccured += LoginErrorOccured;
            _stateController.LoginController.LoginSucceeded += LoginSucceeded;
        }

        private void LoginErrorOccured(Error error)
        {
            App.Navigation.NavigationStack.First().DisplayAlert("", error.friendlyErrorText, "OK");
            Password = "";
        }

        private void LoginSucceeded(Subscriber obj)
        {
            App.Navigation.PopAsync();
        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(() =>
                {
                    _stateController.LoginController.LoginAsync(this.Email, this.Password);
                });
            }
        }

        protected void Notify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}

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
        private string _email; //the email entry value
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

        private string _password; //the password entry value
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
            _stateController = stateController;
        }

        private void LoginErrorOccured(Error error)
        {
            App.Navigation.NavigationStack.First().DisplayAlert("", error.friendlyErrorText, "OK");
            Password = ""; //password is reset, but we keep the entered email
        }

        private void LoginSucceeded(Subscriber subscriber)
        {
            _stateController.LoginController.Subscriber = subscriber; //setting the subscriber, so we can reference him from other pages
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

        /// <summary>
        /// Directs the user to the watch website using the device's default browser
        /// </summary>
        public ICommand TryWatchCommand
        {
            get
            {
                return new Command(() =>
                {
                    string url = "https://secure.finanswatch.dk/user/create?mode=trial";
                    Device.OpenUri(new Uri(url));
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

        public void SubscribeToLoginEvents()
        {
            //events for successfull and failed login
            _stateController.LoginController.LoginEventErrorOccured += LoginErrorOccured;
            _stateController.LoginController.LoginEventSucceeded += LoginSucceeded;
        }

        public void UnsubscribeLoginEvents()
        {
            //events for successfull and failed login
            _stateController.LoginController.LoginEventErrorOccured -= LoginErrorOccured;
            _stateController.LoginController.LoginEventSucceeded -= LoginSucceeded;
        }
    }
}

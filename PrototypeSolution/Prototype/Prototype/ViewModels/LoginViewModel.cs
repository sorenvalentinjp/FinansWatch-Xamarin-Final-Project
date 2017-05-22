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

        /// <summary>
        /// Invoked when the user fails to log in.
        /// </summary>
        /// <param name="error"></param>
        private void LoginErrorOccured(Error error)
        {
            App.Navigation.NavigationStack.First().DisplayAlert("", error.friendlyErrorText, "OK");
            Password = ""; //password is reset, but we keep the entered email
        }

        /// <summary>
        /// Invoked when the user successfully logs in.
        /// </summary>
        /// <param name="subscriber"></param>
        private void LoginSucceeded(Subscriber subscriber)
        {
            _stateController.LoginController.Subscriber = subscriber; //setting the subscriber as the user is now logged in
        }

        /// <summary>
        /// Tries to log the user in. Depending on the result an event are invoked.
        /// See LoginErrorOccured and LoginSucceeded in this class.
        /// </summary>
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

        /// <summary>
        /// Subscribes the viewModel to events for successfull and failed login
        /// </summary>
        public void SubscribeToLoginEvents()
        {
            _stateController.LoginController.LoginEventErrorOccured += LoginErrorOccured;
            _stateController.LoginController.LoginEventSucceeded += LoginSucceeded;
        }

        /// <summary>
        /// Unsubscribes the viewModel to events for successfull and failed login
        /// </summary>
        public void UnsubscribeLoginEvents()
        {
            _stateController.LoginController.LoginEventErrorOccured -= LoginErrorOccured;
            _stateController.LoginController.LoginEventSucceeded -= LoginSucceeded;
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

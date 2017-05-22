using Newtonsoft.Json;
using Prototype.Database;
using Prototype.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.ModelControllers
{
    public class LoginController
    {
        private readonly ILoginApi _loginApi;
        public event Action<Error> LoginEventErrorOccured;
        public event Action<Subscriber> LoginEventSucceeded;
        public Subscriber Subscriber;

        public LoginController()
        {
            _loginApi = new LoginApi();
        }

        /// <summary>
        /// This constructor is used to mock testing only
        /// </summary>
        /// <param name="loginApi"></param>
        public LoginController(ILoginApi loginApi)
        {
            _loginApi = loginApi;
        }

        /// <summary>
        /// Used to log the user in. This is done i two stages:
        /// First stage: Use the password and email to generate a token using Jyllands-Postens LoginAPI.
        /// Second stage: Use the token to generate the subscriber using Jyllands-Postens LoginAPI.
        /// Events are then invoked to notify other classes when a subscriber is logged in/logging in failed.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public async void LoginAsync(string email, string password)
        {
            SubscriberToken token = JsonConvert.DeserializeObject<SubscriberToken>(await _loginApi.DownloadLoginToken(email, password));

            //code = 0 means email and password was correctly entered
            if(token.error.code == 0)
            {
                Subscriber subscriber = JsonConvert.DeserializeObject<Subscriber>(await _loginApi.DownloadSubscriber(token));

                //code = 0 once again means no error occured
                if (subscriber.error.code == 0)
                {
                    Subscriber = subscriber;
                    LoginEventSucceeded?.Invoke(subscriber);
                }
                else
                    LoginEventErrorOccured?.Invoke(subscriber.error);
            }
            else
                LoginEventErrorOccured?.Invoke(token.error);
        }

        /// <summary>
        /// Logs the user out by setting the subscriber to null.
        /// Also notifies other classes, that the user is now logged out.
        /// </summary>
        public void LogoutEventAction()
        {
            Subscriber = null;
            LoginEventSucceeded?.Invoke(null);
        }
    }
}

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
        private readonly LoginApi _loginApi;
        public event Action<Error> LoginEventErrorOccured;
        public event Action<Subscriber> LoginEventSucceeded;
        public Subscriber Subscriber;

        public LoginController()
        {
            _loginApi = new LoginApi();
        }

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

        public void LogoutEventAction()
        {
            Subscriber = null;
            LoginEventSucceeded?.Invoke(null);
        }
    }
}

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
        private readonly StateController _stateController;
        public event Action<Error> LoginEventErrorOccured;
        public event Action<Subscriber> LoginEventSucceeded;
        public event Action LogoutEvent;

        public LoginController(StateController stateController)
        {
            _loginApi = new LoginApi();
            _stateController = stateController;
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
                    _stateController.Subscriber = subscriber;
                    LoginEventSucceeded(subscriber);
                }
                else
                    LoginEventErrorOccured(subscriber.error);
            }
            else
                LoginEventErrorOccured(token.error);
        }

        public void LogoutEventAction()
        {
            _stateController.Subscriber = null;
            LogoutEvent();
        }
    }
}

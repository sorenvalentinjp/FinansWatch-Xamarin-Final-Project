using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Models
{
    public class Subscriber
    {
        public List<FeatureAccessList> featureAccessList { get; set; }
        public Error error { get; set; }
        public User user { get; set; }

        public bool HasAccessToSite(string site)
        {
            foreach (var featureAccess in featureAccessList)
            {
                if (featureAccess.name == site.ToUpper()) return featureAccess.access;
            }
            return false;
        }
    }

    public class FeatureAccessList
    {
        public bool access { get; set; }
        public string denyReasonText { get; set; }
        public string obtainAccessText { get; set; }
        public string code { get; set; }
        public long expiration { get; set; }
        public string name { get; set; }
    }

    public class Error
    {
        public string friendlyErrorText { get; set; }
        public int code { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }

    public class User
    {
        public int userId { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }
}

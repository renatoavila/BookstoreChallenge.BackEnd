using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreChallenge.Domain.Notifications
{
    public class Notifiable
    {
        private List<string> _notifications;

        // Write: Fazer o Dapper ignorar essa propriedade 
        [Write(false)]
        [JsonIgnore]
        public bool Valid { get; private set; }

        // Write: Fazer o Dapper ignorar essa propriedade 
        [Write(false)]
        [JsonIgnore]
        public bool Invalid
        {
            get{ return !Valid;}
            private set { }
        }

        public Notifiable()
        {
            _notifications = new List<string>();
            Valid = true;
        }

        public void AddNotification(bool condition, string mesage)
        {
            if (condition)
            {
                _notifications.Add(mesage);
                Valid = false;
            }
                
        }

        public List<string> GetNotification()
        {
            return _notifications;
        }

    }
}

using BookstoreChallenge.Domain.Entity.Interfaces;
using BookstoreChallenge.Domain.Notifications;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BookstoreChallenge.Domain.Entity.Base
{
    public class Entity : Notifiable, IEntity
    {
        [JsonIgnore]
        [Key]
        public long Id { get; set; }

        private Guid _key;
         
        public Guid Key
        {
            
            get
            {
                if (_key == null || _key == Guid.Empty)
                {
                    _key = Guid.NewGuid();
                }
                return _key;
            }
            set
            {
                _key = value;
            }
        }
    }
}

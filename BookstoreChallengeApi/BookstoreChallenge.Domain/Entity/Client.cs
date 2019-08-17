using System;
using System.Collections.Generic; 
using System.Text;
using Dapper.Contrib.Extensions;
using BookstoreChallenge.Domain.Entity.Base;
using Newtonsoft.Json;

namespace BookstoreChallenge.Domain.Entity
{

    [Table("Client")]
    public class Client : Base.Entity
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}

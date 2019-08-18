using System;
using System.Collections.Generic; 
using System.Text;
using BookstoreChallenge.Domain.Entity.Base;
using BookstoreChallenge.Util.Extensions;
using Newtonsoft.Json;

namespace BookstoreChallenge.Domain.Entity
{  
    [Table("client")]
    public class Client : Base.Entity
    {  
        public string CPF { get; set; } 
        public string Name { get; set; } 
        public string Email { get; set; }

    }
}

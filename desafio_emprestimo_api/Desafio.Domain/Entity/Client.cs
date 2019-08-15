using System;
using System.Collections.Generic; 
using System.Text;
using Dapper.Contrib.Extensions;
using Desafio.Domain.Entity.Base;
using Newtonsoft.Json;

namespace Desafio.Domain.Entity
{

    [Table("Client")]
    public class Client : Base.Entity
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}

using System;

namespace Desafio.Domain.Entity.Interfaces
{
    public interface IEntity 
    {
        long Id { get; set; }
        Guid Key { get; set; }
    }
}

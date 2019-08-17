using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreChallenge.Repository.Interface
{
    public interface IRepository<T>
    {
        long Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        T Get(long id);
        T Get(Guid key);
        List<T> GetAll();
        long GetId(Guid key);
        Guid GetKey(long id);

        //object Query(string sql);
    }
}

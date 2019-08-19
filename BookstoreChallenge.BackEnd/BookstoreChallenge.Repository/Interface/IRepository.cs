using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        List<T> GetAll(Expression<Func<T, bool>> expression);
        long GetId(Guid key);
        Guid GetKey(long id);

        //object Query(string sql);
    }
}

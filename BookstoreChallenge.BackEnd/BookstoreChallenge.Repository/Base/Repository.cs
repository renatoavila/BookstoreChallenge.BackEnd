using BookstoreChallenge.Domain.Entity.Interfaces;
using BookstoreChallenge.Util.Extensions;
using BookstoreChallenge.Repository.Interface;
using BookstoreChallenge.Repository.Util;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace BookstoreChallenge.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly IConfiguration _config;
        private readonly ILogger<Repository<T>> _logger;

        private const int RETRY = 3;
        private const int RETRY_TIME_SECONDS = 2;
        private const string DEFAULT_CONNECTION = "DefaultConnection";

        public Repository(IConfiguration config,
                          ILogger<Repository<T>> logger)
        {
            _config = config;
            _logger = logger;
        }

        public virtual long Insert(T entity)
        {
            int retryCount = 0;
            long id = 0;
            do
            {
                try
                {
                    using (var conn = new ConnectionFactory().GetConnection(_config.GetConnectionString(DEFAULT_CONNECTION)))
                    {
                        id = conn.Insert<T>(entity);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (!ValidateException(ex, entity, ref retryCount))
                    {
                        throw;
                    }
                }
            } while (true);

            return id;
        }

        public virtual bool Update(T entity)
        {
            int retryCount = 0;
            bool ret = false;
            do
            {
                try
                {
                    using (var conn = new ConnectionFactory().GetConnection(_config.GetConnectionString(DEFAULT_CONNECTION)))
                    {
                        if (entity.Id == 0)
                            entity.Id = this.GetId(entity.Key);

                        if (entity.Id > 0)
                            ret = conn.Update<T>(entity);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (!ValidateException(ex, entity, ref retryCount))
                    {
                        throw;
                    }
                }
            } while (true);

            return ret;
        }

        public virtual T Get(long id)
        {
            int retryCount = 0;
            T ret;
            do
            {
                try
                {
                    using (var conn = new ConnectionFactory().GetConnection(_config.GetConnectionString("DefaultConnection")))
                    {
                        ret = conn.Get<T>(id);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (!ValidateException(ex, null, ref retryCount))
                    {
                        throw;
                    }
                }
            } while (true);

            return ret;
        }

        public virtual T Get(Guid key)
        {
            int retryCount = 0;
            T ret = null;
            do
            {
                try
                {
                    using (var conn = new ConnectionFactory().GetConnection(_config.GetConnectionString("DefaultConnection")))
                    {
                        var id = this.GetId(key);
                        if (id > 0)
                            ret = conn.Get<T>(id);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (!ValidateException(ex, new { key = key }, ref retryCount))
                    {
                        throw;
                    }
                }
            } while (true);

            return ret;
        }

        public virtual List<T> GetAll()
        {
            int retryCount = 0;
            List<T> ret;
            do
            {
                try
                {
                    using (var conn = new ConnectionFactory().GetConnection(_config.GetConnectionString("DefaultConnection")))
                    {
                        ret = conn.GetAll<T>()?.ToList();
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (!ValidateException(ex, null, ref retryCount))
                    {
                        throw;
                    }
                }
            } while (true);

            return ret;
        }

        public virtual bool Delete(T entity)
        {
            int retryCount = 0;
            bool ret = false;
            do
            {
                try
                {
                    using (var conn = new ConnectionFactory().GetConnection(_config.GetConnectionString("DefaultConnection")))
                    {
                        if (entity.Id == 0)
                        {
                            entity.Id = this.GetId(entity.Key);
                        }
                        ret = conn.Delete<T>(entity);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (!ValidateException(ex, entity, ref retryCount))
                    {
                        throw;
                    }
                }
            } while (true);

            return ret;
        }

        public long GetId(Guid key)
        {
            int retryCount = 0;
            long id = 0;
            do
            {
                try
                {
                    using (var conn = new ConnectionFactory().GetConnection(_config.GetConnectionString(DEFAULT_CONNECTION)))
                    {
                        id = conn.Query<int>($"select id from {GetTableName()} where \"Key\" = @Key", new { Key = key }).FirstOrDefault<int>();
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (!ValidateException(ex, new { key = key }, ref retryCount))
                    {
                        throw;
                    }
                }
            } while (true);

            return id;
        }

        public Guid GetKey(long id)
        {
            int retryCount = 0;
            Guid key;
            do
            {
                try
                {
                    using (var conn = new ConnectionFactory().GetConnection(_config.GetConnectionString(DEFAULT_CONNECTION)))
                    {
                        key = conn.Query<Guid>($"select \"Key\" from {GetTableName()} where id = @id", new { id = id }).FirstOrDefault<Guid>();
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (!ValidateException(ex, new { id = id }, ref retryCount))
                    {
                        throw;
                    }
                }
            } while (true);

            //return key;
            return Guid.NewGuid();
        }

        private string GetTableName()
        {
            var dnAttribute = typeof(T).GetCustomAttributes(
                typeof(TableAttribute), true
            ).FirstOrDefault() as TableAttribute;
            if (dnAttribute != null)
            {
                return dnAttribute.Name;
            }
            return null;
        }

        private bool ValidateException(Exception ex, object obj, ref int retryCount)
        {
            retryCount++;
            if (retryCount <= RETRY
                        && (ex.Message.Equals("The operation has timed out.")
                            || ex.Message.Contains("40P01:")))
            {
                _logger.LogWarning(ex, $"TRY: {retryCount} - {ex.Message}", obj);
                Thread.Sleep(RETRY_TIME_SECONDS * 1000);
                return true;
            }
            else
            {
                _logger.LogError(ex, ex.Message, obj);
                return false;
            }
        }

    }
}

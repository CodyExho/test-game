using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.DataAccess.Entity;
using Game.DataAccess.Repositories;
using Game.DataAccess.Entity;
using Game.DataAccess.Repository;
using Game.DataAccess;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Game.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly Dictionary<string, object> _cachedRepos;

        private IClientSessionHandle _session;

        public UnitOfWork(MongoClient client, IMongoDatabase database)
        {
            _client = client;
            _database = database;
            _cachedRepos = new Dictionary<string, object>();
        }

        public IClientSessionHandle Session => _session ?? _client.StartSession();

        public void BeginTransaction()
        {
            var session = _client.StartSession();
            session.StartTransaction();
            _session = session;
        }

        public async Task BeginTransactionAsync()
        {
            var session = await _client.StartSessionAsync();
            session.StartTransaction();
            _session = session;
        }

        public void SaveChanges()
        {
            _session.CommitTransaction();
        }

        public async Task SaveChangesAsync()
        {
            await _session.CommitTransactionAsync();
        }

        public void AbortTransaction()
        {
            _session.AbortTransaction();
        }

        public async Task AbortTransactionAsync()
        {
            await _session.AbortTransactionAsync();
        }

        public IRepository<T> GetRepository<T>() where T : AbstractEntity
        {
            var type = typeof(T).ToString();

            if (_cachedRepos.ContainsKey(type))
            {
                return _cachedRepos[type] as Repository<T>;
            }

            var attribute = (CollectionNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(CollectionNameAttribute));
            _cachedRepos[type] = new Repository<T>(_database.GetCollection<T>(attribute.Name), this);
            return (Repository<T>)_cachedRepos[type];
        }

        public void Dispose()
        {
            _session?.Dispose();
        }
    }
}
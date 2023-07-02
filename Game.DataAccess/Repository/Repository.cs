using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Game.DataAccess.Entity;
using Game.DataAccess.Repository;
using Game.DataAccess;
using MongoDB.Driver;

namespace Game.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : AbstractEntity
    {
        private readonly IMongoCollection<T> _entities;
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IMongoCollection<T> entities, IUnitOfWork unitOfWork)
        {
            _entities = entities;
            _unitOfWork = unitOfWork;
        }

        public T Find(string id) => _entities.Find(entity => entity.Id.ToString() == id).FirstOrDefault();

        public IEnumerable<T> All => _entities.AsQueryable().AsEnumerable();

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate) => _entities.Find(predicate).ToList();

        public void Insert(T entity)
        {
            _entities.InsertOne(_unitOfWork.Session, entity);
        }

        public async Task InsertAsync(T entity)
        {
            await _entities.InsertOneAsync(_unitOfWork.Session, entity);
        }

        public void InsertRange(IEnumerable<T> entities)
        {
            _entities.InsertMany(_unitOfWork.Session, entities);
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            await _entities.InsertManyAsync(_unitOfWork.Session, entities);
        }

        public void Update(Expression<Func<T, bool>> predicate, T entity)
        {
            _entities.ReplaceOne(_unitOfWork.Session, predicate, entity);
        }

        public async Task UpdateAsync(Expression<Func<T, bool>> predicate, T entity)
        {
            await _entities.ReplaceOneAsync(_unitOfWork.Session, predicate, entity);
        }


        public void Delete(T entityToDelete)
        {
            _entities.DeleteOne(_unitOfWork.Session, IdPredicate(entityToDelete.Id));
        }

        public async Task DeleteAsync(T entityToDelete)
        {
            await _entities.DeleteOneAsync(_unitOfWork.Session, IdPredicate(entityToDelete.Id));
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            _entities.DeleteOne(_unitOfWork.Session, predicate);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            await _entities.DeleteOneAsync(_unitOfWork.Session, predicate);
        }

        private Expression<Func<T, bool>> IdPredicate(string id) => entity => entity.Id == id;
    }
}
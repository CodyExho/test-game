using System.Threading.Tasks;
using Game.DataAccess.Entity;
using Game.DataAccess.Repository;
using MongoDB.Driver;

namespace Game.DataAccess
{
    public interface IUnitOfWork
    {
        public IClientSessionHandle Session { get; }
        void BeginTransaction();
        Task BeginTransactionAsync();
        void SaveChanges();
        Task SaveChangesAsync();
        void AbortTransaction();
        Task AbortTransactionAsync();
        IRepository<T> GetRepository<T>() where T : AbstractEntity;
    }
}
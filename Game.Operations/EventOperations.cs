using AutoMapper;
using Game.DataAccess.Repository;
using Game.DataAccess;
using System.Diagnostics.Contracts;
using Game.DataAccess.Entity;
using Game.Common.Models;

namespace Game.Operations
{
    public class EventOperations : SingleEntityOperations<EventModel, EventEntity>, IEventOperations
    {
        private readonly IRepository<EventEntity> _eventRepository;

        public EventOperations(IUnitOfWork unitOfWork, IMapper mapper) : base(mapper)
        {
            _eventRepository = unitOfWork.GetRepository<EventEntity>();
        }

        public async Task CreateEventAsync(EventModel model)
        {
            var entity = Convert(model);

            await _eventRepository.InsertAsync(entity);
        }

        public IEnumerable<EventModel> Get() =>
            _eventRepository.All.Select(Convert);
    }
}

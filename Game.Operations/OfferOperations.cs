using AutoMapper;
using Game.DataAccess.Repository;
using Game.DataAccess;
using System.Diagnostics.Contracts;
using Game.DataAccess.Entity;
using Game.Common.Models;

namespace Game.Operations
{
    public class OfferOperations : SingleEntityOperations<OfferModel, OfferEntity>, IOfferOperations
    {
        private readonly IRepository<OfferEntity> _offerRepository;

        public OfferOperations(IUnitOfWork unitOfWork, IMapper mapper) : base(mapper)
        {
            _offerRepository = unitOfWork.GetRepository<OfferEntity>();
        }

        public async Task CreateOfferAsync(OfferModel model)
        {
            var entity = Convert(model);

            await _offerRepository.InsertAsync(entity);
        }

        public IEnumerable<OfferModel> Get() =>
            _offerRepository.All.Select(Convert);
    }
}

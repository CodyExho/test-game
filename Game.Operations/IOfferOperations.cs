using Game.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Operations
{
    public interface IOfferOperations
    {
        public Task CreateOfferAsync(OfferModel model);
        public IEnumerable<OfferModel> Get();
    }
}

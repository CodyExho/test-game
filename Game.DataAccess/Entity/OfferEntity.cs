using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DataAccess.Entity
{
    [CollectionName("offer")]
    public class OfferEntity : AbstractEntity
    {
        public string Name { get; set; }

        public DateTimeOffset StartsAt { get; set; }

        public DateTimeOffset ExpiresAt { get; set; }

        public int OfferType { get; set; }
    }
}

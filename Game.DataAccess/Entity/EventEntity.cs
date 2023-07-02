using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DataAccess.Entity
{
    [CollectionName("event")]
    public class EventEntity : AbstractEntity
    {
        public string Name { get; set; }

        public DateTimeOffset StartsAt { get; set; }

        public DateTimeOffset ExpiresAt { get; set; }

        public int EventType { get; set; }
    }
}

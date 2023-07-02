using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Common.Models
{
    public abstract class AbstractModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset StartsAt { get; set; }

        public DateTimeOffset ExpiresAt { get; set; }

    }
}

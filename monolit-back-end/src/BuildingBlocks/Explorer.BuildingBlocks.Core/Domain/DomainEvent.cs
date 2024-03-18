using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.Domain
{
    public abstract class DomainEvent
    {
        public DomainEvent(long aggregateId)
        {
            Id = aggregateId;
        }
        public long Id { get; private set; }
    }
}

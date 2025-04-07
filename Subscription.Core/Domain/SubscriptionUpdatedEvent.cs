using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscription.Core.Domain
{
    public class SubscriptionUpdatedEvent
    {
        public Guid SubscriptionId { get; set; }
        public required string Status { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

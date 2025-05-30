using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscription.Core.Domain
{
    public class SubscriptionNotificationRequest
    {
        public Guid UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public SubscriptionNotificationType NotificationType { get; set; }
    }
}

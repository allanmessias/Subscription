using Subscription.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscription.Core.Use_Cases
{
    public class RestoreSubscriptionUseCase : IRestoreSubscriptionUseCase
    {
        public Task Execute(Guid userId, Guid subscriptionId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

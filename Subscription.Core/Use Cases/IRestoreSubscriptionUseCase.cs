using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscription.Core.Application
{
    public interface IRestoreSubscriptionUseCase
    {
        Task Execute(Guid userId, Guid subscriptionId, CancellationToken cancellationToken = default);
    }
}

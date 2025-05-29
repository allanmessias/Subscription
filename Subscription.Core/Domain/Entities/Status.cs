using System.ComponentModel.DataAnnotations;

namespace Subscription.Core.Domain.Entities
{
    public enum Status
    {

        Active = 1,
        Canceled = 2,
        Restarted = 3,
    }
}

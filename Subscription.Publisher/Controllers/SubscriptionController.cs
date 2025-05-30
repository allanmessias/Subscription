using Microsoft.AspNetCore.Mvc;
using Subscription.Core.Application;
using Subscription.Core.Domain;

namespace Subscription.Publisher.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly IActivateSubscriptionUseCase _activate;
    private readonly IDeactivateSubscriptionUseCase _cancel;
    private readonly IRestoreSubscriptionUseCase _restore;

    public SubscriptionController(IActivateSubscriptionUseCase activate, 
        IDeactivateSubscriptionUseCase cancel, IRestoreSubscriptionUseCase restore)
    {
        _activate = activate;
        _cancel = cancel;
        _restore = restore;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SubscriptionNotificationRequest request)
    {
        switch ()
    }
}

public class SubscriptionEventDto
{
    public Guid SubscriptionId { get; set; }
    public string Status { get; set; }
}

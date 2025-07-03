using Microsoft.AspNetCore.Mvc;
using Subscription.Core.Application;
using Subscription.Core.Domain;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly IActivateSubscriptionUseCase _activate;
    private readonly IDeactivateSubscriptionUseCase _cancel;
    private readonly IRestoreSubscriptionUseCase _restore;

    public SubscriptionController(
        IActivateSubscriptionUseCase activate,
        IDeactivateSubscriptionUseCase cancel,
        IRestoreSubscriptionUseCase restore)
    {
        _activate = activate;
        _cancel = cancel;
        _restore = restore;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SubscriptionNotificationRequest request)
    {
     
        switch (request.NotificationType)
        {
            case SubscriptionNotificationType.SUBSCRIPTION_PURCHASED:
                await _activate.Execute(request.UserId, request.SubscriptionId);
                return Ok("Subscription activated successfully.");

            case SubscriptionNotificationType.SUBSCRIPTION_CANCELED:
                await _cancel.Execute(request.UserId, request.SubscriptionId);
                return Ok("Subscription canceled successfully.");

            case SubscriptionNotificationType.SUBSCRIPTION_RESTARTED:
                await _restore.Execute(request.UserId, request.SubscriptionId);
                return Ok("Subscription restarted successfully.");

            default:
                return BadRequest("Unhandled notification type.");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Subscription.Core.Application;
using Subscription.Core.Domain;

namespace Subscription.Publisher.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ISendSubscriptionUseCase _useCase;

    public SubscriptionController(ISendSubscriptionUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SubscriptionEventDto dto)
    {
        await _useCase.ExecuteAsync("subscription.test", dto);
        return Ok("Published Message");
    }
}

public class SubscriptionEventDto
{
    public Guid SubscriptionId { get; set; }
    public string Status { get; set; }
}

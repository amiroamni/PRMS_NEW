using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PRMS_BackendAPI.Hubs;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IHubContext<CommunicationHub> _hubContext;

    public ChatController(IHubContext<CommunicationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("ReceiveMessage")]
    public async Task<IActionResult> ReceiveMessage(string userId, string message)
    {
        await _hubContext.Clients.User(userId).SendAsync("ReceiveMessage", userId, message);
        return Ok();
    }
}

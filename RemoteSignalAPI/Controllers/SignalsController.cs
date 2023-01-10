using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;

namespace RemoteSignalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SignalsController : ControllerBase
{
    private readonly ILogger<SignalsController> _logger;

    public SignalsController(ILogger<SignalsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [Route("SendSignal")]
    public async Task<ActionResult> SendSignal(SignalDTO signalDto)
    {
        _logger.LogInformation($"Signal with id {signalDto.Id} received by Remote Signal API");
        return Ok();
    }
}


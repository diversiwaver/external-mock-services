using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;

namespace RemoteSignalAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SignalsController : ControllerBase
{
    private readonly ILogger<SignalsController> _logger;

    public SignalsController(ILogger<SignalsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("SendSignal")]
    public async Task<ActionResult> SendSignal(SignalDTO signalDto)
    {
        _logger.LogInformation($"Signal {signalDto.Id} received by Remote Signal API");
        return Ok();
    }
}


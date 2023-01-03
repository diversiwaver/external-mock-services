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
    [Route("PostSignal")]
    public async Task<ActionResult> PostSignal(SignalDTO signalDto)
    {
        _logger.LogInformation($"Signal {signalDto.Id} received Remote Signal API");
        return Ok();
    }
}


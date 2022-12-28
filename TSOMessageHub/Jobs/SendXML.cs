using Quartz;

namespace TSOMessageHub;

using System.Data.SqlTypes;
using MassTransit;
using TSOMessageHub.Models;

public class SendXML : IJob
{
    private readonly ILogger<SendXML> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public SendXML(ILogger<SendXML> logger, IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;

    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Reading XML file: {time}", DateTimeOffset.Now);
        using (var reader = new StreamReader("XML/Signal1.xml"))
        {
            var xmlString = reader.ReadToEnd();
            await _publishEndpoint.Publish(new HubSignal(xmlString, "123456"));
            _logger.LogInformation("Sent signal at: {time}", DateTimeOffset.Now);
        }
    }
}


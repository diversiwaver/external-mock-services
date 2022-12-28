using Quartz;
using System;
using System.Data.SqlTypes;
using System.Xml;
using System.Xml.Serialization;
using MassTransit;
using TSOMessageHub.XML;

namespace TSOMessageHub;

public class SendXML : IJob
{
    private readonly ILogger<SendXML> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly char _separator = Path.DirectorySeparatorChar;
    private readonly string _projectPath = new Uri(Directory.GetCurrentDirectory()).LocalPath;
    private readonly static Random _random = new Random();
    private static decimal _quantityMw = 0;
    private static int _counter = 0;
    private readonly AfrrSignal _signal;

    public SendXML(ILogger<SendXML> logger, IPublishEndpoint publishEndpoint)
    { 
        _publishEndpoint = publishEndpoint;
        _logger = logger;
        // Read the XML file into a string
        string documentPath = $"{_separator}XML{_separator}SignalTemplate.xml";
        string xmlString = File.ReadAllText(_projectPath + documentPath);

        var serializer = new XmlSerializer(typeof(AfrrSignal));
        using (StringReader reader = new(xmlString))
        {
            _signal = serializer.Deserialize(reader) as AfrrSignal ?? new AfrrSignal();
        }
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _signal.SignalId += ++_counter;
        _quantityMw += _random.Next(-2, 3);
        _signal.QuantityMw += _quantityMw;
        _signal.ReceivedUTC = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss.fff");

        await _publishEndpoint.Publish(_signal);
    }
}


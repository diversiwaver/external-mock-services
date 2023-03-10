using Quartz;
using System.Xml.Serialization;
using MassTransit;
using TSOMessageHub.DTOs;

namespace TSOMessageHub;

public class SendXML : IJob
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly char _separator = Path.DirectorySeparatorChar;
    private readonly string _projectPath = new Uri(Directory.GetCurrentDirectory()).LocalPath;
    private readonly static Random _random = new Random();
    private static decimal _quantityMw = 38;
    private static int _counter = 0;
    private readonly TSOSignal _signal;
    private static bool _isDown = true;
    private static int _bottomLimit = -3;
    private static int _topLimit = 1;

    public SendXML(IPublishEndpoint publishEndpoint)
    { 
        _publishEndpoint = publishEndpoint;
        // Read the XML file into a string
        string documentPath = $"{_separator}XML{_separator}TSOSignalTemplate.xml";
        string xmlString = File.ReadAllText(_projectPath + documentPath);

        var serializer = new XmlSerializer(typeof(TSOSignal));
        using StringReader reader = new(xmlString);
        _signal = serializer.Deserialize(reader) as TSOSignal ?? new TSOSignal();
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _signal.SignalId += ++_counter;
        if ((_quantityMw <= -40) && (_isDown))
        {
            _quantityMw = -40;
            _isDown = false;
            _bottomLimit = 0; 
            _topLimit = 4;
        }
        else if ((_quantityMw >= 40) && (_isDown is false))
        {
            _quantityMw = 40;
            _isDown = true;
            _bottomLimit = -3;
            _topLimit = 1;
        }
            
        _quantityMw += _random.Next(_bottomLimit, _topLimit);
        _signal.QuantityMw = _quantityMw;
        _signal.ReceivedUTC = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss.fff");

        await _publishEndpoint.Publish(_signal);
    }
}



using MassTransit;
using TSOMessageHub.XML;

namespace TSOMessageHub.Consumers;
	public class SignalConsumer : IConsumer<TSOSignal>
	{
        private readonly ILogger<SignalConsumer> _logger;

		public SignalConsumer(ILogger<SignalConsumer> logger)
		{
            _logger = logger;
		}

        public async Task Consume(ConsumeContext<TSOSignal> context)
        {
            _logger.LogInformation("Message Sent at: {Time}", context.Message.ReceivedUTC);
            _logger.LogInformation("Received a new message with id: {Id} and Quantity: {Quantity}", context.Message.SignalId, context.Message.QuantityMw);
            _logger.LogInformation("Current Time: {Time}", DateTimeOffset.Now.ToString("dd-MM-yyyy HH:mm:ss.fff"));
        }
    }


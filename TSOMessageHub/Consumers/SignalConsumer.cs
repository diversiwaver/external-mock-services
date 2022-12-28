
using MassTransit;
using TSOMessageHub.XML;

namespace TSOMessageHub.Consumers
{
	public class SignalConsumer : IConsumer<AfrrSignal>
	{
        private readonly ILogger<SignalConsumer> _logger;

		public SignalConsumer(ILogger<SignalConsumer> logger)
		{
            _logger = logger;
		}

        public async Task Consume(ConsumeContext<AfrrSignal> context)
        {
            _logger.LogInformation("Message Sent at: " + context.Message.ReceivedUTC);
            _logger.LogInformation("Received a new message with id: " + context.Message.SignalId + " and Quantity: " + context.Message.QuantityMw);
            _logger.LogInformation("Current Time: " + DateTimeOffset.Now.ToString("dd-MM-yyyy HH:mm:ss.fff"));
        }
    }
}


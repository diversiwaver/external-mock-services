
using MassTransit;
using TSOMessageHub.Models;

namespace TSOMessageHub.Consumers
{
	public class SignalConsumer : IConsumer<HubSignal>
	{
        private readonly ILogger<SignalConsumer> _logger;

		public SignalConsumer(ILogger<SignalConsumer> logger)
		{
            _logger = logger;
		}

        public async Task Consume(ConsumeContext<HubSignal> context)
        {
            _logger.LogInformation("Received a new message");
            throw new NotImplementedException();
        }
    }
}


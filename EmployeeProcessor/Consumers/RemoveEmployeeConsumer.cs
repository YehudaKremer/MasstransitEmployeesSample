using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;
using Contracts.Commands;

namespace EmployeeProcessor.Consumers
{
    public class RemoveEmployeeConsumer : IConsumer<RemoveEmployee>
    {
        readonly ILogger<RemoveEmployeeConsumer> _logger;

        public RemoveEmployeeConsumer(ILogger<RemoveEmployeeConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<RemoveEmployee> context)
        {
            _logger.LogInformation("Removing Employee id: {EmployeeName}", context.Message.EmployeeID);

            return Task.CompletedTask;
        }
    }
}
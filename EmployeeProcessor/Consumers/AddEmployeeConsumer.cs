using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;
using Contracts.Commands;

namespace EmployeeProcessor.Consumers
{
    public class AddEmployeeConsumer : IConsumer<AddEmployee>
    {
        readonly ILogger<AddEmployeeConsumer> _logger;

        public AddEmployeeConsumer(ILogger<AddEmployeeConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<AddEmployee> context)
        {
            var employee = context.Message.EmployeeToAdd;
            _logger.LogInformation("Adding Employee {EmployeeName}, id:{EmployeeID}", employee.Name, employee.EmployeeID);

            return Task.CompletedTask;
            //await context.Send(new RemoveEmployee { EmployeeToRemove = context.Message.EmployeeToAdd });
        }
    }
}
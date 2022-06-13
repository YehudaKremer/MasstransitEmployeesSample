using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;
using Contracts.Commands;
using Contracts.Events;

namespace EmployeeProcessor.Consumers
{
    public class CheckEmployeeDetailsConsumer : IConsumer<CheckEmployeeDetails>
    {
        readonly ILogger<CheckEmployeeDetails> _logger;

        public CheckEmployeeDetailsConsumer(ILogger<CheckEmployeeDetails> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CheckEmployeeDetails> context)
        {
            var employee = context.Message.EmployeeToCheck;
            _logger.LogInformation("Checking Employee {EmployeeName} Details, id:{EmployeeID}", employee.Name, employee.EmployeeID);

            if (employee.YearsOfExperience >= 3)
            {
                _logger.LogInformation("Employee {EmployeeName} Details Are Match, id:{EmployeeID}", employee.Name, employee.EmployeeID);
                await context.Publish(new EmployeeDetailsMatched { MatchedEmployee = employee });
            }
            else
            {
                _logger.LogInformation("Employee {EmployeeName} Details Are NOT Match, id:{EmployeeID}", employee.Name, employee.EmployeeID);
                await context.Publish(new EmployeeDetailsNotMatched { DeniedEmployeeID = employee.EmployeeID });
            }
        }
    }
}
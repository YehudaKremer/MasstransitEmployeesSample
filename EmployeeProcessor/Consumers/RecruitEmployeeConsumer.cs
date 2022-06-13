using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MassTransit;
using Contracts.Commands;
using Contracts.Events;

namespace EmployeeProcessor.Consumers
{
    public class RecruitEmployeeConsumer : IConsumer<RecruitEmployee>
    {
        readonly ILogger<RecruitEmployeeConsumer> _logger;

        public RecruitEmployeeConsumer(ILogger<RecruitEmployeeConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RecruitEmployee> context)
        {
            var employee = context.Message.EmployeeToRecruit;
            _logger.LogInformation("Recruiting Employee {EmployeeName}, id:{EmployeeID}", employee.Name, employee.EmployeeID);

            await context.Publish(new RecruitmentStarted { EmployeeToRecruit = context.Message.EmployeeToRecruit });
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MassTransit;
using Contracts.Commands;
using Contracts.Entities;

namespace Publisher
{
    public class Worker : BackgroundService
    {
        readonly IBus _bus;
        readonly ILogger<Worker> _logger;

        public Worker(IBus bus, ILogger<Worker> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // start recruiting new employee every x milliseconds

            while (!stoppingToken.IsCancellationRequested)
            {
                var randomNumber = new Random().Next(0, 10);
                var employee = new Employee
                {
                    EmployeeID = Guid.NewGuid(),
                    Name = $"John doe{randomNumber}",
                    PhoneNumber = $"050612345{randomNumber}",
                    YearsOfExperience = new Random().Next(0, 9)
                };

                await _bus.Send(new RecruitEmployee { EmployeeToRecruit = employee }, stoppingToken);

                _logger.LogInformation("Start Recruiting Employee {EmployeeName}, id:{EmployeeID}", employee.Name, employee.EmployeeID);

                await Task.Delay(2000).ConfigureAwait(false);
            }

            await Task.Delay(60000, stoppingToken);
        }
    }
}






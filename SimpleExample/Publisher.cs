using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MassTransit;
using SimpleExample.Entities;

namespace SimpleExample
{
    public class Publisher : BackgroundService
    {
        readonly IBus _bus;
        readonly ILogger<Publisher> _logger;

        public Publisher(IBus bus, ILogger<Publisher> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Sending <HelloWorld> message with a random number every x milliseconds
            while (!stoppingToken.IsCancellationRequested)
            {
                var randomNumber = new Random().Next(0, 10);
                var message = new HelloWorld
                {
                    MyMessage = $"Hello World Message {randomNumber}"
                };

                _logger.LogInformation("Sending <HelloWorld> message with the content:{content}", message.MyMessage);

                await _bus.Send(message, stoppingToken);

                await Task.Delay(2000);
            }

            await Task.Delay(60000, stoppingToken);
        }
    }
}

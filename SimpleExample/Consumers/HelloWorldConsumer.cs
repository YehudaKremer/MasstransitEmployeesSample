using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using SimpleExample.Entities;

namespace SimpleExample.Consumers
{
    public class HelloWorldConsumer : IConsumer<HelloWorld>
    {
        readonly ILogger<HelloWorld> _logger;

        public HelloWorldConsumer(ILogger<HelloWorld> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<HelloWorld> context)
        {
            _logger.LogInformation("Got new <HelloWorld> message with the content: {content}", context.Message.MyMessage);

            return Task.CompletedTask;
        }
    }
}

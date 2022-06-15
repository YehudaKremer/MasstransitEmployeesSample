using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using SimpleExample.Entities;
using SimpleExample.Consumers;

namespace SimpleExample
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(config =>
                    {
                        config.UsingRabbitMq((context, mqConfig) =>
                        {
                            mqConfig.Host("localhost", "simple", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            mqConfig.ConfigureEndpoints(context);
                        });

                        EndpointConvention.Map<HelloWorld>(new Uri($"queue:HelloWorld"));

                        config.AddConsumer<HelloWorldConsumer>();
                    });

                    services.AddHostedService<Publisher>();
                });
    }
}
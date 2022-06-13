using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Contracts;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Publisher
{
    public class Program
    {
        const string SagaConnectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MasstransitEmployeesSample;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";

        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((host, log) =>
                {
                    if (host.HostingEnvironment.IsProduction())
                        log.MinimumLevel.Information();
                    else
                        log.MinimumLevel.Information();

                    log.MinimumLevel.Override("Microsoft", LogEventLevel.Information);
                    log.MinimumLevel.Override("Quartz", LogEventLevel.Information);
                    log.WriteTo.Console();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.SetDapperSagaRepositoryProvider(SagaConnectionstring, config => { });

                        var entryAssembly = Assembly.GetEntryAssembly();

                        x.AddConsumers(entryAssembly);
                        x.AddSagaStateMachines(entryAssembly);
                        x.AddSagas(entryAssembly);
                        x.AddActivities(entryAssembly);

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("localhost", "employees", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.PrefetchCount = 2;
                            cfg.UseMessageRetry(r => r.Interval(5, 1000));
                            cfg.UseInMemoryOutbox();

                            cfg.ConfigureEndpoints(context);
                        });
                    });

                    BrokerMapping.MapEntities();

                    services.AddHostedService<Worker>();
                });
    }
}

using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Serilog;
using Serilog.Events;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Contracts;

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
                    services.AddMassTransit(config =>
                    {
                        config.SetDapperSagaRepositoryProvider(SagaConnectionstring, config => { });

                        var entryAssembly = Assembly.GetEntryAssembly();

                        config.AddConsumers(entryAssembly);
                        config.AddSagaStateMachines(entryAssembly);
                        config.AddSagas(entryAssembly);
                        config.AddActivities(entryAssembly);

                        config.UsingRabbitMq((context, mqConfig) =>
                        {
                            mqConfig.Host("localhost", "employees", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            mqConfig.PrefetchCount = 2;
                            mqConfig.UseMessageRetry(r => r.Interval(5, 1000));
                            mqConfig.UseInMemoryOutbox();

                            mqConfig.ConfigureEndpoints(context);
                        });
                    });

                    services.AddOptions<MassTransitHostOptions>()
                        .Configure(config =>
                        {
                            config.WaitUntilStarted = true;
                        });

                    services.AddOpenTelemetryTracing(builder =>
                    {
                        builder.SetResourceBuilder(ResourceBuilder.CreateDefault()
                                .AddService("Publisher")
                                .AddTelemetrySdk()
                                .AddEnvironmentVariableDetector())
                            .AddSource("MassTransit")
                            .AddAspNetCoreInstrumentation()
                            .AddJaegerExporter(o =>
                            {
                                o.AgentHost = "localhost";
                                o.AgentPort = 6831;
                                o.MaxPayloadSizeInBytes = 4096;
                                o.ExportProcessorType = ExportProcessorType.Batch;
                                o.BatchExportProcessorOptions = new BatchExportProcessorOptions<Activity>
                                {
                                    MaxQueueSize = 2048,
                                    ScheduledDelayMilliseconds = 5000,
                                    ExporterTimeoutMilliseconds = 30000,
                                    MaxExportBatchSize = 512,
                                };
                            });
                    });

                    BrokerMapping.MapEntities();

                    services.AddHostedService<Worker>();
                });
    }
}

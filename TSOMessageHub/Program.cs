using Quartz;
using TSOMessageHub;
using MassTransit;
using RabbitMQ.Client;
using TSOMessageHub.Consumers;
using TSOMessageHub.XML;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        services.AddMassTransit(x =>
        {
            // [1] Uncomment, as well as [2], in case you want to debug and consume your own messages
            /*x.AddConsumer<SignalConsumer>(c =>
            {
                c.UseConcurrencyLimit(1);
                c.UseMessageRetry(f => f.Intervals(TimeSpan.FromMilliseconds(250), TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(1000)));
            });*/
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host.Configuration["RabbitMQHost"], 5672, "ortfmwaf", h => {
                    h.Username(host.Configuration["RabbitMQUsername"]);
                    h.Password(host.Configuration["RabbitMQPassword"]);
                });
                // [2] Uncomment, as well as [1] in case you want to debug and consume your own messages
                /*cfg.ReceiveEndpoint("tso-signal-list", x =>
                {
                    x.ConfigureConsumeTopology = false;
                    x.Bind("TSOMessageHub.XML:TSOSignal", s =>
                    {
                        s.RoutingKey = "TSOSignal";
                        s.ExchangeType = ExchangeType.Topic;
                    });
                    x.ConfigureConsumer<SignalConsumer>(context);
                });*/
                cfg.Publish<TSOSignal>(f =>
                {
                    f.ExchangeType = ExchangeType.Topic;
                });
                
                cfg.Send<TSOSignal>(c => c.UseRoutingKeyFormatter(f => "TSOSignal"));
                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.ScheduleJob<SendXML>(trigger =>
                trigger.WithSimpleSchedule(SimpleScheduleBuilder.RepeatSecondlyForever(4))
                );
        });
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        //Add Logging
        services.AddLogging(configure =>
        {
            configure.AddConsole(); // enables logging in console
            configure.AddDebug(); // enables logging to the debgger
            configure.AddEventSourceLogger(); // Enables logging using the EventSource class.
        });
    })
    .Build();

host.Run();


﻿using Quartz;
using TSOMessageHub;
using MassTransit;
using RabbitMQ.Client;
using TSOMessageHub.Models;
using TSOMessageHub.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<SignalConsumer>(c =>
            {
                c.UseConcurrencyLimit(1);
                c.UseMessageRetry(f => f.Intervals(TimeSpan.FromMilliseconds(250), TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(1000)));
            });
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host.Configuration["RabbitMQHost"], 5672, "ortfmwaf", h => {
                    h.Username(host.Configuration["RabbitMQUsername"]);
                    h.Password(host.Configuration["RabbitMQPassword"]);
                });
                cfg.ReceiveEndpoint("tso-signal-list", x =>
                {
                    x.ConfigureConsumeTopology = false;
                    x.Bind("TSOMessageHub.Models:HubSignal", s =>
                    {
                        s.RoutingKey = "123456";
                        s.ExchangeType = ExchangeType.Topic;
                    });
                    x.ConfigureConsumer<SignalConsumer>(context);
                });
                cfg.Publish<HubSignal>(f =>
                {
                    f.ExchangeType = ExchangeType.Topic;
                });
                cfg.Send<HubSignal>(c => c.UseRoutingKeyFormatter(f => f.Message.MessageId));
                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.ScheduleJob<SendXML>(trigger => trigger.WithCronSchedule("0 * * ? * *"));
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


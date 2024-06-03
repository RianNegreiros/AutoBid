using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
  x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("nt", false));

  x.UsingRabbitMq((context, cfg) =>
  {
    cfg.UseMessageRetry(r =>
      {
        r.Handle<RabbitMqConnectionException>();
        r.Interval(5, TimeSpan.FromSeconds(10));
      });

    cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
      {
        host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
        host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
      });

    cfg.ConfigureEndpoints(context);
  });
});

var app = builder.Build();

app.Run();

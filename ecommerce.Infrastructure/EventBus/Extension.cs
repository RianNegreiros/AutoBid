using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.Infrastructure.EventBus;

public static class Extension
{
  public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
  {
    var rabbitMq = new RabbitMqOption();
    configuration.GetSection("RabbitMq").Bind(rabbitMq);

    services.AddMassTransit(x =>
    {
      x.UsingRabbitMq((context, cfg) =>
      {
        cfg.Host(rabbitMq.ConnectionString, host =>
        {
          host.Username(rabbitMq.UserName);
          host.Password(rabbitMq.Password);
        });
      });
    });

    return services;
  }
}

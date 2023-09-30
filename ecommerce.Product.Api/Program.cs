using ecommerce.Infrastructure.Mongo;
using ecommerce.Product.Api.Handlers;
using ecommerce.Product.Api.Repositories;
using ecommerce.Product.Api.Services;
using MassTransit;
using ecommerce.Infrastructure.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongoDB(builder.Configuration);
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<CreateProductHandler>();

var rabbitMqOptions = new RabbitMqOption();
builder.Configuration.GetSection("rabbitmq").Bind(rabbitMqOptions);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateProductHandler>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(rabbitMqOptions.ConnectionString), h =>
        {
            h.Username(rabbitMqOptions.UserName);
            h.Password(rabbitMqOptions.Password);
        });

        cfg.ReceiveEndpoint("create-product", e =>
        {
            e.PrefetchCount = 16;
            e.UseMessageRetry(r => r.Interval(2, 100));
            e.ConfigureConsumer<CreateProductHandler>(context);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var bus = app.Services.GetService<IBusControl>();
bus.Start();

var databaseInitializer = app.Services.GetService<IDatabaseInitializer>();
if (databaseInitializer != null)
    await databaseInitializer.InitializeAsync();

app.Run();

using SearchService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

try
{
  await DbInitiliazer.InitDb(app);
}
catch (Exception e)
{
  Console.WriteLine(e);
}

app.Run();

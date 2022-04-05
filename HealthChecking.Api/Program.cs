using Jef.HealthChecking.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHealthChecking();

var app = builder.Build();

app.MapControllers();

app.Run();
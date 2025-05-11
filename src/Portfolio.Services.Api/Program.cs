using Portfolio.Services.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddLoggerConfig();
builder.Services.AddApiConfig(builder.Configuration);
builder.Services.AddSwaggerConfig();
builder.Services.AddControllers();
builder.Services.ResolveDependencies(builder.Configuration);
builder.Services.AddAuthConfiguration(builder.Configuration);
builder.Services.AddHealthConfig(builder.Configuration);

var app = builder.Build();

app.UseHealthConfig();
app.UseApiConfiguration(app.Environment);
app.UseSwaggerConfig();
app.Run();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// metrics will be available on :1234/metrics
using var server = new Prometheus.KestrelMetricServer(port: 1234);
server.Start();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

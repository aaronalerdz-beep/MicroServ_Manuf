using Confluent.Kafka;
using CQRS.Core.Consumers;
using Cycle.QUERY.DOMAIN.repository;
using Cycle.QUERY.INFRASTRUCTURE.DataAccess;
using Cycle.QUERY.INFRASTRUCTURE.Handler;
using Cycle.QUERY.INFRASTRUCTURE.Repositories;
using Microsoft.EntityFrameworkCore;
using Post.Query.Infrastructure.Consumers;

var builder = WebApplication.CreateBuilder(args);

Action<DbContextOptionsBuilder> configureDbContext = o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
builder.Services.AddDbContext<DatabaseContext>(configureDbContext);
builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));


var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
dataContext.Database.EnsureCreated();

builder.Services.AddScoped<ICycleRepository, CycleRepository>();
builder.Services.AddScoped<IMachineConfigRepository, MachineConfigRepository>();
builder.Services.AddScoped<IEventHandler, Cycle.QUERY.INFRASTRUCTURE.Handler.EventHandler>();
builder.Services.AddScoped<IEventConsumer, EventConsumer>();
builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));

builder.Services.AddControllers();
builder.Services.AddHostedService<ConsumerHostedService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


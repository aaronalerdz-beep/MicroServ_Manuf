using Confluent.Kafka;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Cycle.Cmd.Commands;
using Cycle.Cmd.INFRASTRUCTURE.Dispatchers;
using Cycle.CMD.INFRASTRUCTURE;
using Cycle.CMD.INFRASTRUCTURE.Producers;
using Cycle.CMD.INFRASTRUCTURE.Stores;
using MES_Cycle.CMD.DOMAIN.Aggregates;
using MES_Cycle.CMD.INFRASTRUCTURE;
using MES_Cycle.CMD.INFRASTRUCTURE.Config;
using MES_Cycle.CMD.INFRASTRUCTURE.Handlers;
using MES_Cycle.CMD.INFRASTRUCTURE.Repositories;

var builder = WebApplication.CreateBuilder(args);

MongoEventClassMaps.Register();
builder.Services.Configure<MongoDBConfig>(builder.Configuration.GetSection(nameof(MongoDBConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<CycleAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();
builder.Services.AddScoped<IEventProducer, EventProducer>();

var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
var dispatcher = new CommandDispatcher();
dispatcher.RegisterHandler<NewCycleCommand>(commandHandler.HandlesAsync);
dispatcher.RegisterHandler<MachineConfigCommand>(commandHandler.HandlesAsync);

builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

builder.Services.AddControllers();
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
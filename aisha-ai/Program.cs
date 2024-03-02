using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tarteeb_bot_test.Brokers.Blobs;
using Tarteeb_bot_test.Brokers.Events;
using Tarteeb_bot_test.Brokers.Redises;
using Tarteeb_bot_test.Brokers.Storages;
using Tarteeb_bot_test.Brokers.Telegrams;
using Tarteeb_bot_test.Services.Coordinations.Telegrams;
using Tarteeb_bot_test.Services.Foundations.Bloobs;
using Tarteeb_bot_test.Services.Foundations.Levents.TelegramEvents;
using Tarteeb_bot_test.Services.Foundations.Redises;
using Tarteeb_bot_test.Services.Foundations.Telegrams;
using Tarteeb_bot_test.Services.Orchestrations.Telegrams;
using Tarteeb_bot_test.Services.Orchestrations.TelegramStates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IStorageBroker, StorageBroker>();
builder.Services.AddSingleton<ITelegramBroker, TelegramBroker>();
builder.Services.AddSingleton<IEventBroker, EventBroker>();
builder.Services.AddTransient<IRedisBroker, RedisBroker>();
builder.Services.AddTransient<IRedisService, RedisService>();
builder.Services.AddTransient<IBlobBroker, BlobBroker>();
builder.Services.AddTransient<IBlobService, BlobService>();
builder.Services.AddTransient<ITelegramUserMessageEventService, TelegramUserMessageEventService>();
builder.Services.AddTransient<ITelegramService, TelegramService>();
builder.Services.AddTransient<ITelegramUserMessageEventService, TelegramUserMessageEventService>();
builder.Services.AddTransient<ITelegramUserOrchestrationService, TelegramUserOrchestrationService>();
builder.Services.AddTransient<ITelegramStateOrchestrationService, TelegramStateOrchestrationService>();
builder.Services.AddTransient<ITelegramCoordinationService, TelegramCoordinationService>();

builder.Services.AddDistributedRedisCache(options =>
{
    string connectionString = builder.Configuration.GetValue<string>("Redis");
    options.Configuration = connectionString;
});

var app = builder.Build();
RegisterEventListeners(app);

static void RegisterEventListeners(IApplicationBuilder app)
{
    app.ApplicationServices.GetRequiredService<ITelegramCoordinationService>()
        .ListenTelegramUserMessage();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

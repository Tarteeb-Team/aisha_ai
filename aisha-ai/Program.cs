using aisha_ai.Brokers.Blobs;
using aisha_ai.Brokers.Events;
using aisha_ai.Brokers.Storages;
using aisha_ai.Brokers.Telegrams;
using aisha_ai.Services.Coordinations.Telegrams;
using aisha_ai.Services.Foundations.Bloobs;
using aisha_ai.Services.Foundations.Levents.TelegramEvents;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;
using aisha_ai.Services.Orchestrations.Telegrams;
using aisha_ai.Services.Orchestrations.TelegramStates;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IStorageBroker, StorageBroker>();
builder.Services.AddSingleton<ITelegramBroker, TelegramBroker>();
builder.Services.AddSingleton<IEventBroker, EventBroker>();
builder.Services.AddTransient<IBlobBroker, BlobBroker>();
builder.Services.AddTransient<IBlobService, BlobService>();
builder.Services.AddTransient<ITelegramUserService, TelegramUserService>();
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

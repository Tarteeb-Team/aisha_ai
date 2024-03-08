using Aisha.Core.Services.Coordnations.Essays;
using Aisha.Core.Services.Orchestrations.Essays;
using aisha_ai.Brokers.Blobs;
using aisha_ai.Brokers.Events;
using aisha_ai.Brokers.OpenAIs;
using aisha_ai.Brokers.Speeches;
using aisha_ai.Brokers.Storages;
using aisha_ai.Brokers.Telegrams;
using aisha_ai.Brokers.Visions;
using aisha_ai.Services.Coordinations.Telegrams;
using aisha_ai.Services.EssayServices.Foundations.FeedbackCheckers;
using aisha_ai.Services.EssayServices.Foundations.Feedbacks;
using aisha_ai.Services.EssayServices.Foundations.ImprovedEssays;
using aisha_ai.Services.EssayServices.Foundations.ImproveEssays;
using aisha_ai.Services.EssayServices.Foundations.Speeches;
using aisha_ai.Services.EssayServices.Foundations.Telegrams;
using aisha_ai.Services.EssayServices.Orchestrations.Essays;
using aisha_ai.Services.EssayServices.Orchestrations.FeedbackToSpeeches;
using aisha_ai.Services.EssayServices.Orchestrations.ImprovedEssays;
using aisha_ai.Services.EssayServices.Orchestrations.SendToTelegramMessages;
using aisha_ai.Services.EssayServices.Orchestrations.Telegrams;
using aisha_ai.Services.EssayServices.Processings.TelegramUsers;
using aisha_ai.Services.Foundations.Bloobs;
using aisha_ai.Services.Foundations.Checkers;
using aisha_ai.Services.Foundations.EssayAnalizers;
using aisha_ai.Services.Foundations.EssayEvents;
using aisha_ai.Services.Foundations.Essays;
using aisha_ai.Services.Foundations.FeedbackEvents;
using aisha_ai.Services.Foundations.Feedbacks;
using aisha_ai.Services.Foundations.ImageMetadataEvents;
using aisha_ai.Services.Foundations.ImprovedEssays;
using aisha_ai.Services.Foundations.ImproveEssays;
using aisha_ai.Services.Foundations.Speeches;
using aisha_ai.Services.Foundations.SpeechInfos;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;
using aisha_ai.Services.Foundations.Visions;
using aisha_ai.Services.Orchestrations.Feedbacks;
using aisha_ai.Services.Orchestrations.FeedbackToSpeeches;
using aisha_ai.Services.Orchestrations.ImprovedEssays;
using aisha_ai.Services.Orchestrations.SendToTelegramMessages;
using aisha_ai.Services.Orchestrations.TelegramStates;
using aisha_ai.Services.Processings.TelegramUsers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

Brokers(builder);
Foundantions(builder);
Processings(builder);
Orcherstrations(builder);
Coordinatioins(builder);


var app = builder.Build();

RegisterEventListeners(app);

static void RegisterEventListeners(IApplicationBuilder app)
{
    app.ApplicationServices.GetRequiredService<ITelegramCoordinationService>()
        .ListenTelegramUserMessage();

    app.ApplicationServices.GetRequiredService<IEssayCoordinatioinService>()
        .ListenEssay();

    app.ApplicationServices.GetRequiredService<IImprovedEssayOrchestratioinService>()
        .ListenEssayEvent();

    app.ApplicationServices.GetRequiredService<IFeedbackToSpeechOrcherstrationService>()
        .ListenToFeedback();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.Run();

static void Brokers(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<IStorageBroker, StorageBroker>();
    builder.Services.AddSingleton<IStorageBroker, StorageBroker>();
    builder.Services.AddSingleton<ITelegramBroker, TelegramBroker>();
    builder.Services.AddSingleton<IEventBroker, EventBroker>();
    builder.Services.AddTransient<IBlobBroker, BlobBroker>();
    builder.Services.AddTransient<IOpenAIBroker, OpenAIBroker>();
    builder.Services.AddTransient<ISpeechBroker, SpeechBroker>();
    builder.Services.AddTransient<IVisionBroker, VisionBroker>();
}

static void Foundantions(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<IBlobService, BlobService>();
    builder.Services.AddTransient<ITelegramUserService, TelegramUserService>();
    builder.Services.AddTransient<ITelegramService, TelegramService>();
    builder.Services.AddTransient<IImageMeatadataEventService, ImageMeatadataEventService>();
    builder.Services.AddTransient<IEssayEventService, EssayEventService>();
    builder.Services.AddTransient<IEssayService, EssayService>();
    builder.Services.AddTransient<IVisionService, VisionService>();
    builder.Services.AddTransient<ICheckerService, CheckerService>();
    builder.Services.AddTransient<IEssayAnalyzerService, EssayAnalyzerService>();
    builder.Services.AddTransient<IFeedbackService, FeedbackService>();
    builder.Services.AddTransient<IImprovedEssayService, ImprovedEssayService>();
    builder.Services.AddTransient<IImproveEssayService, ImproveEssayService>();
    builder.Services.AddTransient<ISpeechService, SpeechService>();
    builder.Services.AddTransient<ISpeechInfoService, SpeechInfoService>();
    builder.Services.AddTransient<IFeedbackEventService, FeedbackEventService>();
    builder.Services.AddTransient<IFeedbackCheckerService, FeedbackCheckerService>();
}

static void Orcherstrations(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ITelegramUserOrchestrationService, TelegramUserOrchestrationService>();
    builder.Services.AddTransient<ITelegramStateOrchestrationService, TelegramStateOrchestrationService>();
    builder.Services.AddTransient<IEssayOrchestrationService, EssayOrchestrationService>();
    builder.Services.AddTransient<IImprovedEssayOrchestratioinService, ImprovedEssayOrchestratioinService>();
    builder.Services.AddTransient<IFeedbackOrchestrationService, FeedbackOrchestrationService>();
    builder.Services.AddTransient<IFeedbackToSpeechOrcherstrationService, FeedbackToSpeechOrcherstrationService>();
    builder.Services.AddTransient<ISendToTelegramMessageOrcherstrationService, SendToTelegramMessageOrcherstrationService>();
}

static void Coordinatioins(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ITelegramCoordinationService, TelegramCoordinationService>();
    builder.Services.AddTransient<IEssayCoordinatioinService, EssayCoordinatioinService>();
}

static void Processings(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ITelegramUserProcessingService, TelegramUserProcessingService>();
}
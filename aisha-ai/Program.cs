using Aisha.Core.Services.Coordnations.Essays;
using Aisha.Core.Services.Orchestrations.Essays;
using aisha_ai.Brokers.Blobs;
using aisha_ai.Brokers.Cognitives;
using aisha_ai.Brokers.Events;
using aisha_ai.Brokers.OpenAIs;
using aisha_ai.Brokers.Speeches;
using aisha_ai.Brokers.Storages;
using aisha_ai.Brokers.Telegrams;
using aisha_ai.Brokers.Visions;
using aisha_ai.Services.Coordinations.Telegrams;
using aisha_ai.Services.EssayServices.Foundations.Events.EssayEvents;
using aisha_ai.Services.EssayServices.Foundations.Events.FeedbackEvents;
using aisha_ai.Services.EssayServices.Foundations.Events.ImageMetadataEvents;
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
using aisha_ai.Services.Foundations.Essays;
using aisha_ai.Services.Foundations.Feedbacks;
using aisha_ai.Services.Foundations.ImprovedEssays;
using aisha_ai.Services.Foundations.ImproveEssays;
using aisha_ai.Services.Foundations.PhotoCheckers;
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
using aisha_ai.Services.SpeechServices.Coordinations.Speeches;
using aisha_ai.Services.SpeechServices.Foundations.Events.SpeechFeecbackEvents;
using aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeechCheckers;
using aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeeches;
using aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeechFeedbackCheckers;
using aisha_ai.Services.SpeechServices.Foundations.PronunciationAssessments;
using aisha_ai.Services.SpeechServices.Foundations.SpeechFeedbackCheckers;
using aisha_ai.Services.SpeechServices.Foundations.SpeechFeedbacks;
using aisha_ai.Services.SpeechServices.Orcherstrations.ImprovedSpeeches;
using aisha_ai.Services.SpeechServices.Orcherstrations.Speeches;
using aisha_ai.Services.SpeechServices.Orcherstrations.SpeechFeedbacks;
using aisha_ai.Services.SpeechServices.SendToTelegramMessages;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Cors(builder);
Brokers(builder);
Foundantions(builder);
Processings(builder);
Orcherstrations(builder);
Coordinatioins(builder);

var app = builder.Build();

RegisterEventListeners(app);
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
    builder.Services.AddTransient<IPronunciationAssessmentBroker, PronunciationAssessmentBroker>();
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
    builder.Services.AddTransient<IPhotoCheckersService, PhotoCheckersService>();
    builder.Services.AddTransient<IFeedbackService, FeedbackService>();
    builder.Services.AddTransient<IImprovedEssayService, ImprovedEssayService>();
    builder.Services.AddTransient<IOpenAIService, OpenAIService>();
    builder.Services.AddTransient<ISpeechService, SpeechService>();
    builder.Services.AddTransient<ISpeechInfoService, SpeechInfoService>();
    builder.Services.AddTransient<IFeedbackEventService, FeedbackEventService>();
    builder.Services.AddTransient<IFeedbackCheckerService, FeedbackCheckerService>();
    builder.Services.AddTransient<IPronunciationAssessmentService, PronunciationAssessmentService>();
    builder.Services.AddTransient<ISpeechFeedbackEventService, SpeechFeedbackEventService>();
    builder.Services.AddTransient<ISpeechFeedbackService, SpeechFeedbackService>();
    builder.Services.AddTransient<ISpeechFeedbackCheckerService, SpeechFeedbackCheckerService>();
    builder.Services.AddTransient<IImprovedSpeechCheckerService, ImprovedSpeechCheckerService>();
    builder.Services.AddTransient<IImprovedSpeechService, ImprovedSpeechService>();
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
    builder.Services.AddTransient<ISpeechOrcherstrationService, SpeechOrcherstrationService>();
    builder.Services.AddTransient<ISpeechFeedbackOrcherstrationService, SpeechFeedbackOrcherstrationService>();
    builder.Services.AddTransient<IImprovedSpeechOrchestrationService, ImprovedSpeechOrchestrationService>();
    builder.Services.AddTransient<ISendSpeechToTelegramMessageOrcherstrationService, SendSpeechToTelegramMessageOrcherstrationService>();
}

static void Coordinatioins(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ITelegramCoordinationService, TelegramCoordinationService>();
    builder.Services.AddTransient<IEssayCoordinatioinService, EssayCoordinatioinService>();
    builder.Services.AddTransient<ISpeechCoordinationService, SpeechCoordinationService>();
}

static void Processings(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ITelegramUserProcessingService, TelegramUserProcessingService>();
}

static void Cors(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });
}

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

    app.ApplicationServices.GetRequiredService<ISpeechCoordinationService>()
        .ListenToTranscription();
}
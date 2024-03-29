﻿using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;

namespace aisha_ai.Services.SpeechServices.Foundations.SpeechFeedbacks
{
    public interface ISpeechFeedbackService
    {
        public ValueTask<SpeechFeedback> AddSpeechFeedbackAsync(SpeechFeedback speechFeedback);
        public IQueryable<SpeechFeedback> RetrieveAllSpeechFeedbacks();
        public ValueTask<SpeechFeedback> RemoveSpeechFeedbackAsync(SpeechFeedback speechFeedback);
        ValueTask<SpeechFeedback> ModifySpeechFeedbackAsync(SpeechFeedback speechFeedback);
    }
}

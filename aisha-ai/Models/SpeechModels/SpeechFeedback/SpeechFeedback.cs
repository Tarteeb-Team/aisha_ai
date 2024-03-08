//===============================================================================
// Project: AISHA
// Description: This project assists with checking essays and speeches using AI.
// Copyright (c) 2024 AIWisards Team. All rights reserved.
//===============================================================================

using System;
using System.Reflection.Metadata.Ecma335;

namespace aisha_ai.Models.SpeechModels.SpeechFeedback
{
    public class SpeechFeedback
    {
        public Guid Id { get; set; }
        public string Transcription { get; set; }
        public decimal AccurancyScore { get; set; }
        public decimal FluencyScore { get; set; }
        public decimal ProsodyScore { get; set; }
        public decimal CompletenessScore { get; set; }
        public decimal PronunciationScore { get; set; }
        public string TelegramUserName { get; set; }
    }
}

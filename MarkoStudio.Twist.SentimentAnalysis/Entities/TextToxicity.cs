﻿namespace MarkoStudio.Twist.SentimentAnalysis.Entities
{
    public class TextToxicity
    {
        public string OriginId { get; set; }

        public string Text { get; set; }

        public float? Score { get; set; }
    }
}

export enum KnownSentiment {
    Positive = 'POSITIVE',
    Negative = 'NEGATIVE',
    Neutral = 'NEUTRAL',
    Mixed = 'MIXED'
}

export enum KnownToxicity {
    Toxic = 'Toxic',
    NonToxic = 'Non-toxic'
}

export const SentimentMap = {
    [KnownSentiment.Positive]: 'Позитивний',
    [KnownSentiment.Negative]: 'Негативний',
    [KnownSentiment.Neutral]: 'Нейтральний',
    [KnownSentiment.Mixed]: 'Змішаний'
}

export const ToxicityMap = {
    [KnownToxicity.Toxic]: 'Токсичний',
    [KnownToxicity.NonToxic]: 'Нетоксичний'
}
  export class ProfileStatistics {
    profileSentimentLabel: string;
    profileToxicityLabel: string;
    records: Statistics[];
  }

  export class Statistics {
    text: string;
    sentimentScore: SentimentScore;
    toxicityScore: ToxicityScore;
  }
  
  export class SentimentScore {
    score: Map<string, number>;
    label: string;
  }
  
  export class ToxicityScore {
    value: number;
    label: string;
  } 

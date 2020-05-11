  export class ProfileStatistics {
    sentimentScore: SentimentScore;
    toxicityScore: ToxicityScore;
    records: Statistics[];
  }
  
  export class SentimentScore {
    score: Map<string, number>;
    label: string;
  }
  
  export class ToxicityScore {
    score: number;
    label: string;
  }
  
  export class Statistics {
    text: string;
    sentimentScore: SentimentScore;
    toxicityScore: ToxicityScore;
  }
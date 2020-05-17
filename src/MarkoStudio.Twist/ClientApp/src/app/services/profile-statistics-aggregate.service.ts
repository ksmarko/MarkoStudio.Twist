import { Injectable } from '@angular/core';
import { ToxicityScore, SentimentScore } from '../models/profile-statistics.model';
import { ProfileToxicityAggregate, ProfileSentimentAggregate } from '../models/aggregate.model';
import { KnownToxicity, KnownSentiment } from '../models/known.enum';

@Injectable()
export class ProfileAggregateService {
    constructor() {
    }

    public getToxicityAggregate(records: ToxicityScore[]): ProfileToxicityAggregate {
        let toxic = records.filter(x => x.label === KnownToxicity.Toxic).length;
        let nonToxic = records.filter(x => x.label === KnownToxicity.NonToxic).length;

        return {
            toxic: toxic,
            nonToxic: nonToxic
        };
    }

    public getSentimentAggregate(records: SentimentScore[]): ProfileSentimentAggregate {
        let labels = records.map(x => x.label);

        let positive = labels.filter(x => x === KnownSentiment.Positive).length;
        let negative = labels.filter(x => x === KnownSentiment.Negative).length;
        let neutral = labels.filter(x => x === KnownSentiment.Neutral).length;
        let mixed = labels.filter(x => x === KnownSentiment.Mixed).length;

        return {
            positive: positive,
            negative: negative,
            neutral: neutral,
            mixed: mixed
        };
    }
}
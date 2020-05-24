import { Component, OnInit, Input } from '@angular/core';
import { ProfileStatistics, TopWordListItem } from '../models/profile-statistics.model';
import { ProfileAggregateService } from '../services/profile-statistics-aggregate.service';
import { ProfileToxicityAggregate, ProfileSentimentAggregate } from '../models/aggregate.model';

@Component({
  selector: 'app-profile-summary',
  templateUrl: './profile-summary.component.html',
  styleUrls: ['./profile-summary.component.scss']
})
export class ProfileSummaryComponent implements OnInit {

  @Input() profileStatistics: ProfileStatistics;

  constructor(
    private aggregateService: ProfileAggregateService
  ) {
  }

  ngOnInit() {
  }

  public getToxicityChartData(): ProfileToxicityAggregate {
    return this.aggregateService.getToxicityAggregate(this.profileStatistics.records.map(x => x.toxicityScore));
  }
  
  public getSentimentChartData(): ProfileSentimentAggregate {
    return this.aggregateService.getSentimentAggregate(this.profileStatistics.records.map(x => x.sentimentScore));
  }

  public getTopWordsData() : TopWordListItem[] {
    return this.profileStatistics.topWords.slice(0, 10).sort((one, two) => (one > two ? -1 : 1));
  }
}

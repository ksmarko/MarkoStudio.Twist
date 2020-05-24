import { Component, OnInit, Input } from '@angular/core';
import { ProfileStatistics } from '../models/profile-statistics.model';
import { ProfileAggregateService } from '../services/profile-statistics-aggregate.service';
import { ProfileToxicityAggregate, ProfileSentimentAggregate } from '../models/aggregate.model';
import { TopWordListItem } from '../top-words-list/top-words-list.component';

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
    let arr = [
      {
        text: "word 1",
        value: 110
      },
      {
        text: "word 2",
        value: 97
      },
      {
        text: "word 3",
        value: 80
      },
      {
        text: "word 4",
        value: 76
      },
      {
        text: "word 5",
        value: 58
      },
      {
        text: "word 6",
        value: 55
      },
      {
        text: "word 7",
        value: 43
      },
      {
        text: "word 8",
        value: 33
      },
      {
        text: "word 9",
        value: 27
      },
      {
        text: "word 10",
        value: 19
      },
      {
        text: "word 11",
        value: 4
      }
    ];

    return arr.slice(0, 10).sort((one, two) => (one > two ? -1 : 1));;
  }
}

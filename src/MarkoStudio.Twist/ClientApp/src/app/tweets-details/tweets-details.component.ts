import { Component, OnInit, Input } from '@angular/core';
import { Statistics } from '../models/profile-statistics.model';
import { ToxicityMap, SentimentMap } from '../models/known.enum';

@Component({
  selector: 'app-tweets-details',
  templateUrl: './tweets-details.component.html',
  styleUrls: ['./tweets-details.component.scss']
})
export class TweetsDetailsComponent implements OnInit {

  @Input() records: Statistics[];

  constructor() { }

  ngOnInit() {
  }
  
  public mapSentiment(text: string) : string {
    return SentimentMap[text];
  }

  public mapToxicity(text: string) : string {
    return ToxicityMap[text];
  }
}

import { Component, OnInit, Input } from '@angular/core';
import { Statistics } from '../models/profile-statistics.model';

@Component({
  selector: 'app-tweets-details',
  templateUrl: './tweets-details.component.html',
  styleUrls: ['./tweets-details.component.css']
})
export class TweetsDetailsComponent implements OnInit {

  @Input() records: Statistics;

  constructor() { }

  ngOnInit() {
  }

}

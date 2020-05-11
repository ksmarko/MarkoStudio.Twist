import { Component, OnInit, Input } from '@angular/core';
import { ProfileStatistics } from '../models/profile-statistics.model';

@Component({
  selector: 'app-profile-summary',
  templateUrl: './profile-summary.component.html',
  styleUrls: ['./profile-summary.component.scss']
})
export class ProfileSummaryComponent implements OnInit {

  @Input() profileStatistics: ProfileStatistics;

  constructor() {
  }

  ngOnInit() {
  }
}

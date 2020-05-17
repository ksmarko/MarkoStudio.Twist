import { Component } from '@angular/core';
import { ProfileStatistics } from '../models/profile-statistics.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public profileStatistics: ProfileStatistics;

  public updateSearchedRecipient(statistics: ProfileStatistics): void {
    this.profileStatistics = statistics;
  }
}
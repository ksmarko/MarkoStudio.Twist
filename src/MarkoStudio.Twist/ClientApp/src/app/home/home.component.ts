import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public profileStatistics: ProfileStatistics;

  private baseUrl: string;

  constructor(
    private httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
  }

  public getProfileStatistics(userName: string) : void {

    var url = this.baseUrl + `api/statistics/profile?userName=${userName}`;

    this.httpClient.get<ProfileStatistics>(url).subscribe(result => {
      this.profileStatistics = result;

      console.log(result);
    }, error => console.error(error));
  }
}

interface ProfileStatistics{
  sentimentScore: SentimentScore,
  toxicityScore: ToxicityScore,
  records: Statistics[]
}

interface SentimentScore {
  score: Map<string, number>,
  label: string
}

interface ToxicityScore {
  score: number,
  label: string
}

interface Statistics {
  text: string,
  sentimentScore: SentimentScore,
  toxicityScore: ToxicityScore
}
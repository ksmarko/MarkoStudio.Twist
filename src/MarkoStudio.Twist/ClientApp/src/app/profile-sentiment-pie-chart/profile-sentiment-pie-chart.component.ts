import { Component, OnInit, OnChanges, Input, SimpleChanges } from '@angular/core';
import { SentimentScore } from '../models/profile-statistics.model';
import { Color } from 'ng2-charts';

@Component({
  selector: 'app-profile-sentiment-pie-chart',
  templateUrl: './profile-sentiment-pie-chart.component.html',
  styleUrls: ['./profile-sentiment-pie-chart.component.css']
})
export class ProfileSentimentPieChartComponent implements OnChanges {

  @Input() profileSentiment: SentimentScore;

  public sentimentChart = [];

  public doughnutChartLabels: string[];
  public doughnutChartData: number[];
  public doughnutChartType: string;
  public chartColors: Color[] = [{}];

  constructor() { }

  public ngOnChanges(changes: SimpleChanges): void {
    this.updateChartData();
  }

  private updateChartData(): void {

    let positive = this.profileSentiment.score["Positive"];
    let negative = this.profileSentiment.score["Negative"];
    let neutral = this.profileSentiment.score["Neutral"];

    this.doughnutChartLabels = ['Positive', 'Negative', 'Neutral'];
    this.doughnutChartData = [positive, negative, neutral];
    this.doughnutChartType = 'doughnut';

    let colors = ["#4BC0C0", "#FF6384", "#FFCD56"];
    let prColors = ["#93d9d9", "#ffa1b5", "#ffe29a"]

    this.chartColors = [{ backgroundColor: prColors, hoverBackgroundColor: colors }];
  }
}

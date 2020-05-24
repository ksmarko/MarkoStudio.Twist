import { Component, OnChanges, Input, SimpleChanges } from '@angular/core';
import { Color } from 'ng2-charts';
import { ChartOptions } from 'chart.js';
import { ProfileSentimentAggregate } from '../models/aggregate.model';

@Component({
  selector: 'app-profile-sentiment-pie-chart',
  templateUrl: './profile-sentiment-pie-chart.component.html',
  styleUrls: ['./profile-sentiment-pie-chart.component.scss']
})
export class ProfileSentimentPieChartComponent implements OnChanges {

  @Input() profileSentiment: ProfileSentimentAggregate;

  public chartLabels: string[];
  public chartData: number[];
  public chartType: string = 'doughnut';
  public chartColors: Color[] = [{}];
  public chartOptions: ChartOptions = {
    title: {
      display: true,
      text: 'Profile sentiment chart',
      position: 'bottom'
    },
    legend: {
      display: true,
      position: 'bottom'
    },
    maintainAspectRatio: false
  };
  
  public ngOnChanges(changes: SimpleChanges): void {
    if (JSON.stringify(changes.profileSentiment.currentValue) !== JSON.stringify(changes.profileSentiment.previousValue))
      this.updateChartData();
  }

  private updateChartData(): void {
    this.chartLabels = ['Positive', 'Negative', 'Neutral', 'Mixed'];
    this.chartData = [
      this.profileSentiment.positive, 
      this.profileSentiment.negative, 
      this.profileSentiment.neutral, 
      this.profileSentiment.mixed];

    let hoverColors = ["#4BC0C0", "#FF6384", "#FFCD56", "#d8d8d8"];
    let mainColors = ["#93d9d9", "#ffa1b5", "#ffe29a", "#e5e5e5"];

    this.chartColors = [{ backgroundColor: mainColors, hoverBackgroundColor: hoverColors }];
  }
}

import { Component, OnChanges, Input, SimpleChanges } from '@angular/core';
import { Color } from 'ng2-charts';
import { ChartOptions } from 'chart.js';
import { ProfileToxicityAggregate } from '../models/aggregate.model';

@Component({
  selector: 'app-profile-toxicity-pie-chart',
  templateUrl: './profile-toxicity-pie-chart.component.html',
  styleUrls: ['./profile-toxicity-pie-chart.component.scss']
})
export class ProfileToxicityPieChartComponent implements OnChanges {

  @Input() profileToxicity: ProfileToxicityAggregate;

  public sentimentChart = [];

  public chartLabels: string[];
  public chartData: number[];
  public chartType: string = 'doughnut';
  public chartColors: Color[] = [{}];
  public chartOptions: ChartOptions = {
    title: {
      display: true,
      text: 'Profile toxicity chart',
      position: 'bottom'
    },
    legend: {
      display: true,
      position: "bottom"
    },
    maintainAspectRatio: false
  };

  private chartUpdated: boolean = false;

  constructor() {
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (!this.chartUpdated){
      this.updateChartData();
      this.chartUpdated = true;
    }
  }

  private updateChartData(): void {
    this.chartLabels = ['Non-toxic', 'Toxic'];
    this.chartData = [this.profileToxicity.nonToxic, this.profileToxicity.toxic];

    let hoverColors = ["#4BC0C0", "#FF6384"];
    let mainColors = ["#93d9d9", "#ffa1b5"];

    this.chartColors = [{ backgroundColor: mainColors, hoverBackgroundColor: hoverColors }];
  }
}


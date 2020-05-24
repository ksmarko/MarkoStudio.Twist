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

  public ngOnChanges(changes: SimpleChanges): void {
    if (JSON.stringify(changes.profileToxicity.currentValue) !== JSON.stringify(changes.profileToxicity.previousValue)) {
      this.updateChartData();
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


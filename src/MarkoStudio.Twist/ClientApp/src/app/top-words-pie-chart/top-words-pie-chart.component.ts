import { Component, SimpleChanges, OnChanges, Input } from '@angular/core';
import { ChartOptions } from 'chart.js';
import { TopWordListItem } from '../top-words-list/top-words-list.component';
import { Color } from 'ng2-charts';

@Component({
  selector: 'app-top-words-pie-chart',
  templateUrl: './top-words-pie-chart.component.html',
  styleUrls: ['./top-words-pie-chart.component.scss']
})
export class TopWordsPieChartComponent implements OnChanges {

  @Input() profileTopWords: TopWordListItem[];

  public chartLabels: string[];
  public chartData: number[];
  public chartColors: Color[] = [{}];
  public chartType: string = 'doughnut';
  public chartOptions: ChartOptions = {
    title: {
      display: false
    },
    legend: {
      display: false
    },
    maintainAspectRatio: false
  };

  private chartUpdated: boolean = false;

  public ngOnChanges(changes: SimpleChanges): void {
    if (!this.chartUpdated){
      this.updateChartData();
      this.chartUpdated = true;
    }
  }

  private updateChartData(): void {
    this.chartLabels = this.profileTopWords.map(x => x.text);
    this.chartData = this.profileTopWords.map(x => x.value);

    let mainColors = [
      "#143939",
      "#104242",
      "#215e5e",
      "#2e8484",
      "#3caaaa",
      "#55c3c3",
      "#7bd1d1",
      "#a1dede",
      "#c6ebeb",
      "#ecf8f8"
    ];

    this.chartColors = [{ backgroundColor: mainColors, hoverBackgroundColor: mainColors }];
  }
}

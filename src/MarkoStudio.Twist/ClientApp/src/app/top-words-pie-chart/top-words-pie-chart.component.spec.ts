import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TopWordsPieChartComponent } from './top-words-pie-chart.component';

describe('TopWordsPieChartComponent', () => {
  let component: TopWordsPieChartComponent;
  let fixture: ComponentFixture<TopWordsPieChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TopWordsPieChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TopWordsPieChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

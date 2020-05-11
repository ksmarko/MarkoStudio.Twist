import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileSentimentPieChartComponent } from './profile-sentiment-pie-chart.component';

describe('ProfileSentimentPieChartComponent', () => {
  let component: ProfileSentimentPieChartComponent;
  let fixture: ComponentFixture<ProfileSentimentPieChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfileSentimentPieChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileSentimentPieChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

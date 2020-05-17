import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileToxicityPieChartComponent } from './profile-toxicity-pie-chart.component';

describe('ProfileToxicityPieChartComponent', () => {
  let component: ProfileToxicityPieChartComponent;
  let fixture: ComponentFixture<ProfileToxicityPieChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfileToxicityPieChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileToxicityPieChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

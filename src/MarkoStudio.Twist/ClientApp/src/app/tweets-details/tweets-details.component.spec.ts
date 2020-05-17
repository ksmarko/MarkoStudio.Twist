import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TweetsDetailsComponent } from './tweets-details.component';

describe('TweetsDetailsComponent', () => {
  let component: TweetsDetailsComponent;
  let fixture: ComponentFixture<TweetsDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TweetsDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TweetsDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

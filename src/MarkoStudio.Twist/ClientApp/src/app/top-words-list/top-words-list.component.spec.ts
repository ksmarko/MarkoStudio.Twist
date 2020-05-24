import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TopWordsListComponent } from './top-words-list.component';

describe('TopWordsListComponent', () => {
  let component: TopWordsListComponent;
  let fixture: ComponentFixture<TopWordsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TopWordsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TopWordsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

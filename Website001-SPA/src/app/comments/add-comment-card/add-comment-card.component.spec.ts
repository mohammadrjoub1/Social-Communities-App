import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCommentCardComponent } from './add-comment-card.component';

describe('AddCommentCardComponent', () => {
  let component: AddCommentCardComponent;
  let fixture: ComponentFixture<AddCommentCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddCommentCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCommentCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

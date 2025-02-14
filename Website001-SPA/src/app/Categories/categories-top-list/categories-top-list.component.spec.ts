import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriesTopListComponent } from './categories-top-list.component';

describe('CategoriesTopListComponent', () => {
  let component: CategoriesTopListComponent;
  let fixture: ComponentFixture<CategoriesTopListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoriesTopListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoriesTopListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

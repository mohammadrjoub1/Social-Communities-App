import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategorieCardComponent } from './categorie-card.component';

describe('CategorieCardComponent', () => {
  let component: CategorieCardComponent;
  let fixture: ComponentFixture<CategorieCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategorieCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategorieCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

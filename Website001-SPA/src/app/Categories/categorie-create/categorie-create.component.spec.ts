import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategorieCreateComponent } from './categorie-create.component';

describe('CategorieCreateComponent', () => {
  let component: CategorieCreateComponent;
  let fixture: ComponentFixture<CategorieCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategorieCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategorieCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

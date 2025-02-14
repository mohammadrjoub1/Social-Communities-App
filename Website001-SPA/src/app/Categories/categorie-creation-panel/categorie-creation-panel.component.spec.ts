import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategorieCreationPanelComponent } from './categorie-creation-panel.component';

describe('CategorieCreationPanelComponent', () => {
  let component: CategorieCreationPanelComponent;
  let fixture: ComponentFixture<CategorieCreationPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategorieCreationPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategorieCreationPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

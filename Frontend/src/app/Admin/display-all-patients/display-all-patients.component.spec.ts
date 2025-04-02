import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayAllPatientsComponent } from './display-all-patients.component';

describe('DisplayAllPatientsComponent', () => {
  let component: DisplayAllPatientsComponent;
  let fixture: ComponentFixture<DisplayAllPatientsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DisplayAllPatientsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DisplayAllPatientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

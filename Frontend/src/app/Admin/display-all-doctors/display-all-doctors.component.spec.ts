import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayAllDoctorsComponent } from './display-all-doctors.component';

describe('DisplayAllDoctorsComponent', () => {
  let component: DisplayAllDoctorsComponent;
  let fixture: ComponentFixture<DisplayAllDoctorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DisplayAllDoctorsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DisplayAllDoctorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorLoginComponent } from './login.component';

describe('LoginComponent', () => {
  let component: DoctorLoginComponent;
  let fixture: ComponentFixture<DoctorLoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DoctorLoginComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoctorLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

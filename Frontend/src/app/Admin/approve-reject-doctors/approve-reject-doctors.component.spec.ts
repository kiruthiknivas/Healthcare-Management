import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveRejectDoctorsComponent } from './approve-reject-doctors.component';

describe('ApproveRejectDoctorsComponent', () => {
  let component: ApproveRejectDoctorsComponent;
  let fixture: ComponentFixture<ApproveRejectDoctorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApproveRejectDoctorsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApproveRejectDoctorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

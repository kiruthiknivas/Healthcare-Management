import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WritePrescriptionComponent } from './write-prescription-component.component';

describe('WritePrescriptionComponentComponent', () => {
  let component: WritePrescriptionComponent;
  let fixture: ComponentFixture<WritePrescriptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WritePrescriptionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WritePrescriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

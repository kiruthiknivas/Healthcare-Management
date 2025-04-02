import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patient-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './patient-dashboard.component.html',
  styleUrls: ['./patient-dashboard.component.css']
})
export class PatientDashboardComponent implements OnInit {
  patientName: string = 'Patient';
  constructor(private router: Router) {}


  ngOnInit() {
    const storedName = localStorage.getItem('patientName');
    if (storedName) {
      this.patientName = storedName;
    }
  }
  navigateToAppointment(): void {
    this.router.navigate(['Patient/bookappointment']);
  }
  viewPrescriptions(): void {
    this.router.navigate(['/view-prescription']);
  }
  makePayment(): void {
    this.router.navigate(['/make-payment']);
  }
}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctor-dashboard',
  templateUrl: './doctor-dashboard.component.html',
  styleUrls: ['./doctor-dashboard.component.css']
})
export class DoctorDashboardComponent implements OnInit {
  doctorName: string = 'doctor';

  constructor(private router: Router) {}

  ngOnInit() {
    const storedName = localStorage.getItem('doctorname');
    console.log(storedName);
    if (storedName) {
      this.doctorName = storedName;
    }
  }

  viewAppointments(): void {
    this.router.navigate(['view-appointments']);
  }

  writePrescription(): void {
    this.router.navigate(['write-prescription']);
  }

  viewPatients(): void {
    this.router.navigate(['view-patients']);
  }
}
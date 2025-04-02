import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-write-prescription',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './write-prescription-component.component.html',
  styleUrls: ['./write-prescription-component.component.css']
})
export class WritePrescriptionComponent implements OnInit {
  appointments: any[] = [];
  selectedAppointmentId!: number;
  prescriptionText: string = '';
  doctorId!: number;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    const storedDoctorId = localStorage.getItem('doctorid');
    if (storedDoctorId) {
      this.doctorId = parseInt(storedDoctorId, 10);
      this.fetchAppointments();
    } else {
      console.error('Doctor ID not found in local storage.');
    }
  }

  fetchAppointments(): void {
    const token = localStorage.getItem('token');
    if (!token) {
      console.error('JWT token not found in local storage.');
      return;
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    this.http.get<any[]>(`https://localhost:7154/api/Doctors/${this.doctorId}/appointments`, { headers }).subscribe(
      data => {
        this.appointments = data;
        console.log('Fetched Appointments:', this.appointments); // Log the fetched appointments
      },
      error => {
        console.error('Error fetching appointments', error);
      }
    );
  }

  submitPrescription(): void {
    const token = localStorage.getItem('token');
    if (!token) {
      console.error('JWT token not found in local storage.');
      return;
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const PrescriptionDto = {
      appointmentID: this.selectedAppointmentId,
      prescriptionText: this.prescriptionText
    };

    console.log('Prescription DTO:', PrescriptionDto); // Log the DTO for debugging

    this.http.post(`https://localhost:7154/api/Doctors/${this.doctorId}/prescriptions`, PrescriptionDto, { headers }).subscribe(
      response => {
        alert('Prescription added successfully!');
        
        this.router.navigate(['doctor-dashboard']); // Navigate back to the dashboard
      },
      error => {
        alert('Failed to add prescription.');
        console.error('Error adding prescription', error);
        console.log(this.appointments);
      }
    );
  }
}
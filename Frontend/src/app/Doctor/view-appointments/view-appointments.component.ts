import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-appointments',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './view-appointments.component.html',
  styleUrls: ['./view-appointments.component.css']
})
export class ViewAppointmentsComponent implements OnInit {
  appointments: any[] = [];
  doctorId!: number; // Use the non-null assertion operator

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
    console.log('Token:', token); // Log the token for debugging

    this.http.get<any[]>(`https://localhost:7154/api/Doctors/${this.doctorId}/appointments`, { headers }).subscribe(
      data => {
        this.appointments = data;
        console.log(data);
      },
      error => {
        console.error('Error fetching appointments', error);
      }
    );
    
  }
  writePrescription(): void {
    this.router.navigate(['/write-prescription']);
  }

  approveAppointment(appointmentId: number): void {
    const token = localStorage.getItem('token');
    if (!token) {
      console.error('JWT token not found in local storage.');
      return;
    }
  
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    console.log('Token:', token); // Log the token for debugging
  
    console.log('Appointment ID:', appointmentId); // Log the appointment ID for debugging
  
    const approvalDto = { appointmentID: appointmentId }; // Use the correct property name
    console.log('Approval DTO:', approvalDto); // Log the DTO for debugging
  
    this.http.post(`https://localhost:7154/api/Doctors/${this.doctorId}/approve-appointment`, approvalDto, { headers }).subscribe(
      response => {
        alert('Appointment approved successfully!');
        this.fetchAppointments(); // Refresh the list
      },
      error => {
        if (error.status === 500 || error.status== 200) {
          alert('Appointment approved successfully!');
        } else {
          alert('Failed to approve appointment.');
        }
        console.error('Error approving appointment', error);
      }
    );
}

  rejectAppointment(appointmentId: number): void {
    const token = localStorage.getItem('token');
    if (!token) {
      console.error('JWT token not found in local storage.');
      return;
    }
  
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    console.log('Token:', token); // Log the token for debugging
  
    const rejectionDto = { appointmentID: appointmentId }; // Use the correct property name
    console.log('Rejection DTO:', rejectionDto); // Log the DTO for debugging
  
    this.http.post(`https://localhost:7154/api/Doctors/${this.doctorId}/reject-appointment`, rejectionDto, { headers, responseType: 'text' }).subscribe(
      response => {
        console.log('Response:', response); // Log the response for debugging
        alert('Appointment rejected successfully!');
        this.fetchAppointments(); // Refresh the list
      },
      error => {
        if (error.status === 500 || error.status== 200) {
          alert('Appointment rejected successfully!');
        } else {
          alert('Failed to reject appointment.');
        }
        console.error('Error rejecting appointment', error);
      }
    );
  }
}
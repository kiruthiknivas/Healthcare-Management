import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Doctor } from './doctor.model'; // Import the Doctor interface

@Component({
  selector: 'app-appointment',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css']
})
export class AppointmentComponent implements OnInit {
  doctors: Doctor[] = []; // Use the Doctor interface
  appointment = {
    DoctorID: 0, // Ensure DoctorID is an integer
    AppointmentDate: '',
    Summary: ''
  };

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchDoctors();
  }

  fetchDoctors(): void {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    this.http.get<Doctor[]>('https://localhost:7154/api/Patients/doctors', { headers }).subscribe(
      data => {
        this.doctors = data;
        console.log(data);
      },
      error => {
        console.error('Error fetching doctors', error);
      }
    );
  }

  bookAppointment(): void {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    // Format the date to "dd/MM/yyyy HH:mm"
    const appointmentDate = new Date(this.appointment.AppointmentDate);
    const formattedDate = `${appointmentDate.getDate().toString().padStart(2, '0')}/${(appointmentDate.getMonth() + 1).toString().padStart(2, '0')}/${appointmentDate.getFullYear()} ${appointmentDate.getHours().toString().padStart(2, '0')}:${appointmentDate.getMinutes().toString().padStart(2, '0')}`;

    // Ensure DoctorID is an integer
    const doctorID = Number(this.appointment.DoctorID);
    console.log('DoctorID:', doctorID); // Debugging log
    console.log('Formatted Date:', formattedDate); // Debugging log

    const appointmentData = {
      DoctorID: doctorID,
      AppointmentDate: formattedDate,
      Summary: this.appointment.Summary
    };

    this.http.post('https://localhost:7154/api/Patients/book-appointment', appointmentData, { headers }).subscribe(
      response => {
        alert('Appointment booked successfully!');
      },
      error => {
        alert('Failed to book appointment.');
        console.error('Error booking appointment', error);
      }
    );
  }
}
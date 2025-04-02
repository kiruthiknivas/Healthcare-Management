import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-patients',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './view-patients.component.html',
  styleUrls: ['./view-patients.component.css']
})
export class ViewPatientsComponent implements OnInit {
  patients: any[] = [];
  doctorId!: number;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    const storedDoctorId = localStorage.getItem('doctorid');
    if (storedDoctorId) {
      this.doctorId = parseInt(storedDoctorId, 10);
      this.fetchPatients();
    } else {
      console.error('Doctor ID not found in local storage.');
    }
  }

  fetchPatients(): void {
    const token = localStorage.getItem('token');
    if (!token) {
      console.error('JWT token not found in local storage.');
      return;
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    this.http.get<any[]>(`https://localhost:7154/api/Doctors/${this.doctorId}/patients`, { headers }).subscribe(
      data => {
        this.patients = data;
        console.log('Fetched Patients:', this.patients); // Log the fetched patients
      },
      error => {
        console.error('Error fetching patients', error);
      }
    );
  }
}
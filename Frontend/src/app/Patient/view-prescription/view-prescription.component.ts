import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-prescription',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './view-prescription.component.html',
  styleUrls: ['./view-prescription.component.css']
})
export class ViewPrescriptionComponent implements OnInit {
  prescriptions: any[] = [];
  patientId!: number;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    const storedPatientId = localStorage.getItem('patientid');
    if (storedPatientId) {
      this.patientId = parseInt(storedPatientId, 10);
      this.fetchPrescriptions();
    } else {
      console.error('Patient ID not found in local storage.');
    }
  }

  fetchPrescriptions(): void {
    const token = localStorage.getItem('token');
    if (!token) {
      console.error('JWT token not found in local storage.');
      return;
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    this.http.get<any[]>(`https://localhost:7154/api/Patients/prescriptions`, { headers }).subscribe(
      data => {
        this.prescriptions = data;
        console.log('Fetched Prescriptions:', this.prescriptions); // Log the fetched prescriptions
      },
      error => {
        console.error('Error fetching prescriptions', error);
      }
    );
  }
}
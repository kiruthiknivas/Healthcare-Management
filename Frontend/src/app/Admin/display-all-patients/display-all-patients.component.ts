import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-display-all-patients',
  standalone:true,
  imports : [CommonModule, FormsModule],
  templateUrl: './display-all-patients.component.html',
  styleUrls: ['./display-all-patients.component.css']
})
export class DisplayAllPatientsComponent implements OnInit {
  patients: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadPatients();
  }

  loadPatients() {
    const token = localStorage.getItem('token');
    this.http.get<any[]>('https://localhost:7154/api/Admin/display-all-patients',{
      headers: {
          Authorization: `Bearer ${token}`,
      }}).subscribe(data => {
      this.patients = data;
      console.log(this.patients[0].patientID);
      

    });
  }

  deletePatient(patientId: number) {
    const token = localStorage.getItem('token');
    this.http.delete(`https://localhost:7154/api/Admin/delete-patient/${patientId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      responseType: 'text' // Specify response type as text
    }).subscribe({
      next: (response) => {
        alert(response); // Show success message
        this.patients = this.patients.filter(patient => patient.id !== patientId);
      },
      error: (error) => {
        console.error('Error deleting patient:', error);
      }
    });
  }
}
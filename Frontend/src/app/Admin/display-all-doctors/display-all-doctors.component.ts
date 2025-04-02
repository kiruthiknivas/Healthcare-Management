import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-display-all-doctors',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './display-all-doctors.component.html',
  styleUrls: ['./display-all-doctors.component.css']
})
export class DisplayAllDoctorsComponent implements OnInit {
  doctors: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadDoctors();
  }

  loadDoctors() {
    const token = localStorage.getItem('token');
    this.http.get<any[]>('https://localhost:7154/api/Admin/display-all-doctors', {
      headers: {
        Authorization: `Bearer ${token}`,
      }
    }).subscribe(data => {
      this.doctors = data;
    });
  }

  deleteDoctor(doctorId: number) {
    const token = localStorage.getItem('token');
    this.http.delete(`https://localhost:7154/api/Admin/delete-doctor/${doctorId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      responseType: 'text' 
    }).subscribe({
      next: (response) => {
        alert(response); 
        this.doctors = this.doctors.filter(doctor => doctor.id !== doctorId);
      },
      error: (error) => {
        console.error('Error deleting doctor:', error);
      }
    });
  }
}
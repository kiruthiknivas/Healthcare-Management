import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-approve-reject-doctors',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './approve-reject-doctors.component.html',
  styleUrls: ['./approve-reject-doctors.component.css']
})
export class ApproveRejectDoctorsComponent implements OnInit {
  unapprovedDoctors: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadUnapprovedDoctors();
  }

  loadUnapprovedDoctors() {
    const token = localStorage.getItem('token');
    this.http.get<any[]>('https://localhost:7154/api/Admin/unapproved-doctors', {
      headers: {
        Authorization: `Bearer ${token}`,
      }
    }).subscribe(data => {
      this.unapprovedDoctors = data;
    });
  }

  approveDoctor(doctorId: number) {
    const token = localStorage.getItem('token');
    this.http.post(`https://localhost:7154/api/Admin/approve-reject-doctor/${doctorId}`, { isApproved: true }, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      responseType: 'text' // Specify response type as text
    }).subscribe({
      next: (response) => {
        alert(response); // Show success message
        this.loadUnapprovedDoctors(); // Refresh the list
      },
      error: (error) => {
        console.error('Error approving doctor:', error);
      }
    });
  }

  rejectDoctor(doctorId: number) {
    const token = localStorage.getItem('token');
    this.http.post(`https://localhost:7154/api/Admin/approve-reject-doctor/${doctorId}`, { isApproved: false }, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      responseType: 'text' // Specify response type as text
    }).subscribe({
      next: (response) => {
        alert(response); // Show success message
        this.loadUnapprovedDoctors(); // Refresh the list
      },
      error: (error) => {
        console.error('Error rejecting doctor:', error);
      }
    });
  }
}
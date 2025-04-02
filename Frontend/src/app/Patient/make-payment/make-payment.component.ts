import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-make-payment',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './make-payment.component.html',
  styleUrls: ['./make-payment.component.css']
})
export class MakePaymentComponent implements OnInit {
  appointmentID!: number;
  amountPaid!: number;
  paymentType: string = '';
  transactionId: string = '';
  patientId!: number;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    const storedPatientId = localStorage.getItem('patientid');
    if (storedPatientId) {
      this.patientId = parseInt(storedPatientId, 10);
    } else {
      console.error('Patient ID not found in local storage.');
    }
  }

  submitPayment(): void {
    const token = localStorage.getItem('token');
    if (!token) {
      console.error('JWT token not found in local storage.');
      return;
    }
  
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const paymentDto = {
      appointmentID: this.appointmentID,
      amountPaid: this.amountPaid
    };
  
    console.log('Payment DTO:', paymentDto); // Log the DTO for debugging
  
    this.http.post(`https://localhost:7154/api/Patients/make-payment`, paymentDto, { headers }).subscribe(
      response => {
        alert('Payment made successfully!');
        this.router.navigate(['/patient-dashboard']); // Navigate back to the patient dashboard
      },
      error => {
        alert('Failed to make payment.');
        console.error('Error making payment', error);
      }
    );
  }
}
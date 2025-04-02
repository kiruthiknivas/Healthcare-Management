import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginRequest } from './LoginRequest';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Import FormsModule
import { Token } from '@angular/compiler';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule], // Use FormsModule instead of NgModel
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
 obj:any={
  Email: "",
  Password: ""
 }

  constructor(private http: HttpClient, private router: Router) {}

  // onSubmit() {
  //   const loginRequest = new LoginRequest(this.email, this.password);
  //   this.http.post<any>('https://localhost:7154/api/Patients/login', loginRequest)
  //     .subscribe(response => {
  //       localStorage.setItem('token', response.token);
  //       this.router.navigate(['/landing']);
  //     }, error => {
  //       console.error('Login failed', error);
  //     });
  // }
  onsubmit() {
    this.http.post("https://localhost:7154/api/Patients/login", this.obj).subscribe(
      (res: any) => {
        console.log(res);
        localStorage.setItem("token", res.token);
        localStorage.setItem("patientName", res.patientname); 
        localStorage.setItem("patientid",res.patientid);
        console.log(res.patientid);
        // Store the patient's first name
        this.router.navigate(['/patient-dashboard']);
      },
      (error) => {
        alert('Invalid credentials');
      }
    );
  }
}
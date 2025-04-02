import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Import FormsModule

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule], // Use FormsModule
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class DoctorRegisterComponent {
  registerData: any = {
    drName: '',
    specialization: '',
    licenseNo: '',
    drFee: 0,
    email: '',
    password: '',
    contactNo: ''
  };

  constructor(private http: HttpClient, private router: Router) {}

  onSubmit() {
    this.http.post("https://localhost:7154/api/Doctors/register", this.registerData, { responseType: 'text' }).subscribe(
      (res: any) => {
        try {
          const parsedRes = JSON.parse(res);
          console.log(parsedRes);
          alert("Doctor Registration Request sent to Admin. Once Admin approves, you will be able to log in.");
          this.router.navigate(['Home']);
        } catch (e) {
          console.error("Error parsing response:", e);
          alert("Doctor Registration Request sent to Admin. Once Admin approves, you will be able to log in.");
          this.router.navigate(['Home']);
        }
      },
      (error) => {
        console.error("HTTP Error:", error);
        alert("An error occurred during registration. Please try again later.");
      }
    );
}
}
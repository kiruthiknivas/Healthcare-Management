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
  imports: [CommonModule,FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class DoctorLoginComponent {
  obj:any={
    Email: "",
    Password: ""
   }
  
    constructor(private http: HttpClient, private router: Router) {}
  
    onsubmit() {
      this.http.post("https://localhost:7154/api/Doctors/login", this.obj).subscribe(
        (res: any) => {
          console.log(res);
          localStorage.setItem("token", res.token);
          localStorage.setItem("doctorname", res.doctor);
          console.log(res.doctor);
          localStorage.setItem("doctorid",res.doctorid);
          console.log(res.doctorid);
           // Store the doctor's first name
          alert("Login Success");
          this.router.navigate(['doctor-dashboard']);
        },
        (error) => {
          alert('Invalid credentials');
        }
      );
    }
}

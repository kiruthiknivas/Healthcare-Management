import { HttpClient, HttpErrorResponse } from '@angular/common/http';
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
export class RegisterComponent {
  registerData:any = {
    firstName: '',
    lastName: '',
    email: '',
    gender: '',
    password: '',
    contactNo: ''
  };

  constructor(private http: HttpClient, private router: Router) {}

  onSubmit() {
    this.http.post("https://localhost:7154/api/Patients/register", this.registerData, { responseType: 'text' }).subscribe(
      (res: any) => {
        console.log(res);
        alert("Patient Registered Successfully");
        this.router.navigate(['Patient/Login']);
      },
      (error: HttpErrorResponse) => {
        if (error.status === 200) {
          alert("Patient Registered Successfully");
          this.router.navigate(['Patient/Login']);
        } else {
          console.error('Error:', error);
          alert("An error occurred during registration.");
        }
      }
    );
  }
}
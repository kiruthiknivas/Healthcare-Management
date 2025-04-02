import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { LoginComponent } from './Patient/login/login.component';
import { RegisterComponent } from './Patient/register/register.component';
import { PatientDashboardComponent } from './Patient/patient-dashboard/patient-dashboard.component';
import { DoctorLoginComponent } from './Doctor/login/login.component';
import { DoctorRegisterComponent } from './Doctor/register/register.component';
import { AppointmentComponent } from './Patient/appointment/appointment.component';
import { DoctorDashboardComponent } from './Doctor/doctor-dashboard/doctor-dashboard.component';
import { ViewAppointmentsComponent } from './Doctor/view-appointments/view-appointments.component';
import { WritePrescriptionComponent } from './Doctor/write-prescription-component/write-prescription-component.component';
import { ViewPatientsComponent } from './Doctor/view-patients/view-patients.component';
import { ViewPrescriptionComponent } from './Patient/view-prescription/view-prescription.component';
import { MakePaymentComponent } from './Patient/make-payment/make-payment.component';
import { AdminLoginComponent } from './Admin/login/login.component';
import { AdminDashboardComponent } from './Admin/admin-dashboard/admin-dashboard.component';
import { DisplayAllPatientsComponent } from './Admin/display-all-patients/display-all-patients.component';
import { DisplayAllDoctorsComponent } from './Admin/display-all-doctors/display-all-doctors.component';
import { ApproveRejectDoctorsComponent } from './Admin/approve-reject-doctors/approve-reject-doctors.component';
import { ContactUsComponent } from './contact-us/contact-us.component';

export const routes: Routes = [
    {
        path:'Home',
        component:HomeComponent
    },{
        path:'',
        component:HomeComponent
    },{
        path:'About',
        component:AboutComponent
    },
    {
        path:'Contact',
        component:ContactUsComponent
    },
    {
        path:'Admin/Login',
        component:AdminLoginComponent
    },
    {
        path:'admin-dashboard',
        component:AdminDashboardComponent
    },
    {
        path:'display-all-patients',
        component:DisplayAllPatientsComponent
    },
    {
        path:'display-all-doctors',
        component:DisplayAllDoctorsComponent
    },
    {
        path:'unapproved',
        component:ApproveRejectDoctorsComponent
    }
    ,{
        path:'Patient/Login',
        component:LoginComponent
    },
    {
        path:'Patient/register',
        component:RegisterComponent
    },
    {
        path:'patient-dashboard',
        component:PatientDashboardComponent
    },
    {
        path:'Patient/bookappointment',
        component:AppointmentComponent
    },
    {
        path:'Doctor/Login',
        component:DoctorLoginComponent
    },
    {
        path:'Doctor/Register',
        component:DoctorRegisterComponent
    },
   
    {
        path:'doctor-dashboard',
        component:DoctorDashboardComponent
    },
    {
        path:'view-appointments',
        component:ViewAppointmentsComponent
    },
    {
        path:'write-prescription',
        component:WritePrescriptionComponent
    },
    {
        path:'view-patients',
        component:ViewPatientsComponent
    },
    {
        path:'view-prescription',
        component:ViewPrescriptionComponent
    },
    {
        path:'make-payment',
        component:MakePaymentComponent
    }
];

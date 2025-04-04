# Healthcare-Management

## Overview

The Healthcare API is a comprehensive solution designed to manage healthcare-related operations such as doctor registration, patient management, appointment scheduling, and prescription handling. This API is built using ASP.NET Core and follows RESTful principles to ensure scalability and ease of use.

## Features

- **Doctor Registration and Login**: Doctors can register and log in to the system. Registration requests are sent to the admin for approval.
- **Patient Management**: Doctors can view their patients and manage their appointments.
- **Appointment Handling**: Doctors can approve or reject appointment requests.
- **Prescription Management**: Doctors can add prescriptions for their patients.
- **JWT Authentication**: Secure authentication using JSON Web Tokens (JWT).

## Technologies Used

- **ASP.NET Core 6.0**
- **Entity Framework Core**
- **JWT for Authentication**
- **SQL Server**

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/kiruthiknivas/Healthcare-Management.git
   ```
2. **Navigate to the project directory:**:
   ```bash
   cd Healthcare-Management
   ```
4. **Restore dependencies**:
   ```bash
   dotnet restore
   ```
5. **Update the database connection string in appsettings.json**
6. **Run the application:**
   ```bash
   dotnet run
   ```
7. **Access Swagger UI:**
   Navigate to https://localhost:7124/swagger/index.html to test the API endpoints.
    

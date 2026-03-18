<div align="center">

```
███████╗██████╗ ███████╗
██╔════╝██╔══██╗██╔════╝
█████╗  ██████╔╝███████╗
██╔══╝  ██╔══██╗╚════██║
██║     ██████╔╝███████║
╚═╝     ╚═════╝ ╚══════╝
Flight Booking System
```

# ✈️ Flight Booking System

### A production-grade REST API for seamless flight search, booking, and check-in

<br/>

![.NET](https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![NUnit](https://img.shields.io/badge/NUnit-22B455?style=for-the-badge&logo=nunit&logoColor=white)

<br/>

> Built with **ASP.NET Core 8** · **Entity Framework Core** · **JWT Auth** · **SMTP Notifications** · **NUnit + Moq**

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Database Design](#-database-design)
- [API Reference](#-api-reference)
- [Getting Started](#-getting-started)
- [Configuration](#-configuration)
- [Authentication](#-authentication)
- [Email Notifications](#-email-notifications)
- [Unit Tests](#-unit-tests)
- [HTTP Status Codes](#-http-status-codes)

---

## 🌐 Overview

The **Flight Booking System (FBS)** is a backend REST API that manages core airline operations — from user registration and JWT-authenticated login, through flight search, booking, and check-in with automatic seat assignment. All critical events trigger SMTP email notifications to the user.

The system follows a clean **layered architecture**: Controllers → Services → Repositories → Database, with full role-based authorization separating regular users from admins.

---

## ✨ Features

| # | Feature | Description |
|---|---------|-------------|
| 1 | 🔐 **User Auth** | Register and login with BCrypt-hashed passwords and JWT tokens |
| 2 | 🛡️ **Role-Based Access** | Separate `User` and `Admin` roles with protected endpoints |
| 3 | 🔍 **Flight Search** | Search available flights by source, destination, and travel date |
| 4 | 💰 **Fare Calculation** | View base fare + GST breakdown for any flight |
| 5 | 🎫 **Book Flight** | Create bookings with auto-generated booking reference |
| 6 | ✅ **Check-In** | Check in using booking reference — seat auto-assigned |
| 7 | 📧 **SMTP Emails** | Automated emails on registration, booking, and check-in |
| 8 | ✈️ **Admin Panel** | Admin can add, update, delete, and view all flights |

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────────┐
│                    CLIENT / SWAGGER UI                   │
└────────────────────────┬────────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────────┐
│              PRESENTATION LAYER — CONTROLLERS            │
│  AuthController  FlightsController  BookingController    │
│  AdminController  CheckinController  FareController      │
└────────────────────────┬────────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────────┐
│              MIDDLEWARE — GlobalExceptionMiddleware       │
│              Handles 400 · 401 · 403 · 404 · 500        │
└────────────────────────┬────────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────────┐
│               BUSINESS LAYER — SERVICES                  │
│  AuthService  FlightService  BookingService              │
│  CheckinService  FareService  EmailService               │
└────────────────────────┬────────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────────┐
│                DATA LAYER — REPOSITORIES                 │
│  AuthRepository  FlightRepository  BookingRepository     │
│  CheckinRepository  FareRepository  AdminRepository      │
└────────────────────────┬────────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────────┐
│          ApplicationDbContext (EF Core)                  │
│       Users · Flights · Bookings · CheckIns · Admins     │
│              SQL Server Express — FlightBookingDB        │
└─────────────────────────────────────────────────────────┘
```

**Integrations running alongside:**
```
JWT Bearer Token (HS256 · 2hr expiry · User/Admin roles)
SMTP via Gmail (Register · Booking confirmed · Check-in confirmed)
```

---

## 🛠️ Tech Stack

| Layer | Technology |
|-------|------------|
| **Runtime** | .NET 8 / ASP.NET Core |
| **Language** | C# |
| **ORM** | Entity Framework Core 8 |
| **Database** | SQL Server 2022 Express |
| **Auth** | JWT Bearer Tokens (HS256) |
| **Password Hashing** | BCrypt.Net |
| **Email** | System.Net.Mail (SMTP / Gmail) |
| **API Docs** | Swashbuckle / Swagger UI |
| **Testing** | NUnit + Moq |

---

## 📁 Project Structure

```
FlightBookingSystem/
│
├── Controllers/
│   ├── AuthController.cs          # Register, User Login, Admin Login
│   ├── FlightsController.cs       # Search flights (public)
│   ├── FareController.cs          # Get fare details
│   ├── BookingController.cs       # Book flight, search booking
│   ├── CheckinController.cs       # Check-in flow
│   └── AdminController.cs         # Admin CRUD for flights
│
├── Services/
│   ├── AuthService.cs             # BCrypt hash, JWT generation
│   ├── FlightService.cs           # Flight search + admin ops
│   ├── BookingService.cs          # Booking creation
│   ├── CheckinService.cs          # Seat assignment, status update
│   ├── FareService.cs             # Fare + GST calculation
│   ├── EmailService.cs            # SMTP email triggers
│   └── PaymentService.cs          # Dummy payment (always true)
│
├── Repositories/
│   ├── AuthRepository.cs
│   ├── FlightRepository.cs
│   ├── BookingRepository.cs
│   ├── CheckinRepository.cs
│   ├── FareRepository.cs
│   └── AdminRepository.cs
│
├── Models/
│   ├── User.cs
│   ├── Flight.cs
│   ├── Booking.cs
│   ├── Checkin.cs
│   └── Admin.cs
│
├── DTOs/                          # Request/Response data transfer objects
├── Interfaces/                    # IService and IRepository contracts
├── Middleware/
│   └── GlobalExceptionMiddleware.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Migrations/
├── Tests/                         # NUnit + Moq unit tests
├── appsettings.json               # Connection strings, JWT, SMTP config
└── Program.cs
```

---

## 🗄️ Database Design

### Schema Overview

```
┌──────────────┐       ┌─────────────────────┐       ┌──────────────────────┐
│    USERS     │       │      BOOKINGS        │       │       CHECKINS       │
├──────────────┤       ├─────────────────────┤       ├──────────────────────┤
│ user_id (PK) │──1──N─│ id (PK)             │──1──1─│ checkin_id (PK)      │
│ username     │       │ booking_reference   │       │ booking_reference    │
│ email        │       │ user_id (FK)        │       │ booking_ref (FK)     │
│ password     │       │ flight_number (FK)  │       │ seat_number          │
└──────────────┘       │ passenger_name      │       │ checkin_reference    │
                       │ gender              │       │ checkin_status       │
┌──────────────┐       │ booking_date        │       └──────────────────────┘
│   FLIGHTS    │       │ booking_status      │
├──────────────┤       │ base_fare           │       ┌──────────────────────┐
│ flight_id(PK)│──1──N─│ final_fare          │       │       ADMINS         │
│ flight_number│       └─────────────────────┘       ├──────────────────────┤
│ source       │                                     │ id (PK)              │
│ destination  │                                     │ username             │
│ departure_time│                                    │ password             │
│ arrival_time │                                     └──────────────────────┘
│ fare         │
│ available_seats│
└──────────────┘
```

### Relationships

- `Users → Bookings` — **One to Many** — One user can have multiple bookings
- `Flights → Bookings` — **One to Many** — One flight can have multiple bookings
- `Bookings → CheckIns` — **One to One** — One booking has exactly one check-in
- `Admins` — **Standalone** — No foreign key relationships

---

## 🔌 API Reference

All endpoints are available via Swagger UI at: **`http://localhost:5208/swagger`**

### Auth Endpoints

| Method | Endpoint | Body | Auth | Response |
|--------|----------|------|------|----------|
| `POST` | `/api/auth/user/register` | `{ username, email, password }` | Public | `201` Created |
| `POST` | `/api/auth/user/login` | `{ username, password }` | Public | `200` + JWT Token |
| `POST` | `/api/auth/admin/login` | `{ username, password }` | Public | `200` + JWT Token |

### Flight Endpoints

| Method | Endpoint | Params | Auth | Response |
|--------|----------|--------|------|----------|
| `GET` | `/api/flights/search` | `?source=&destination=&travel_date=` | Public | `200` List of flights |
| `GET` | `/api/fare/get?flightNumber=XX` | Query param | Public | `200` Fare details |

### Booking & Check-in

| Method | Endpoint | Body | Auth | Response |
|--------|----------|------|------|----------|
| `POST` | `/api/bookings/book` | `{ flight_number, passenger_name, gender }` | 🔒 JWT | `201` BookingRef |
| `GET` | `/api/bookings/search` | `?bookingReference=` | 🔒 JWT | `200` Booking details |
| `POST` | `/api/checkin` | `{ booking_reference }` | 🔒 JWT | `201` Seat + CheckinRef |

### Admin Endpoints

| Method | Endpoint | Body | Auth | Response |
|--------|----------|------|------|----------|
| `POST` | `/api/flights/add` | `{ flight_number, source, destination, travel_date, fare, available_seats }` | 🔒 Admin JWT | `201` Flight |
| `PUT` | `/api/flights/update/{flightNumber}` | Fields to update | 🔒 Admin JWT | `200` Updated flight |
| `DELETE` | `/api/flights/delete/{flightNumber}` | — | 🔒 Admin JWT | `204` No Content |
| `GET` | `/api/flights` | — | 🔒 Admin JWT | `200` All flights |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Express 2022](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (optional, for DB inspection)
- A Gmail account with an [App Password](https://support.google.com/accounts/answer/185833) for SMTP

### Installation

```bash
# 1. Clone the repository
git clone https://github.com/Arun200404/FlightBookingSystem.git
cd FlightBookingSystem

# 2. Restore dependencies
dotnet restore

# 3. Configure appsettings.json (see Configuration section below)

# 4. Apply EF Core migrations
dotnet ef migrations add InitialCreate
dotnet ef database update

# 5. Run the application
dotnet run

# 6. Open Swagger UI
# Navigate to: http://localhost:5208/swagger
```

> 💡 After running `dotnet ef database update`, open **SSMS** and connect to `localhost\SQLEXPRESS` — the `FlightBookingDB` database and all tables will be created automatically.

---

## ⚙️ Configuration

Update `appsettings.json` with your local settings:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=FlightBookingDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "SecretKey": "YourSecretKey_MinLength32Chars!",
    "Issuer": "FlightBookingSystem",
    "Audience": "FlightBookingClients",
    "ExpiryMinutes": 120
  },
  "SmtpSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "youremail@gmail.com",
    "Password": "your-app-password",
    "FromAddress": "youremail@gmail.com",
    "EnableSsl": true
  }
}
```

> ⚠️ **Important:** `appsettings.json` is excluded from version control via `.gitignore` to protect credentials.

---

## 🔐 Authentication

The system uses **JWT Bearer Tokens** for authentication.

```
Token payload includes:
  • UserId
  • Username
  • Role  →  "User" or "Admin"
  • Expiry  →  2 hours

Algorithm: HS256
```

### Using JWT in Swagger

1. Call `POST /api/auth/user/login` to get a token
2. Click **Authorize** (🔓) in Swagger UI
3. Enter: `Bearer <your-token>`
4. All protected endpoints are now accessible

### Role-Based Authorization

```csharp
[Authorize]                    // Any authenticated user
[Authorize(Roles = "Admin")]   // Admin only → returns 403 for regular users
```

---

## 📧 Email Notifications

Three automated SMTP emails are triggered through `EmailService`:

| Event | Subject | Content |
|-------|---------|---------|
| **User Registers** | `Welcome to Flight Booking System` | Welcome message with username |
| **Booking Confirmed** | `Booking Confirmed — BK101` | Booking ref, flight details, passenger name |
| **Check-In Complete** | `Check-in Confirmed — CK201` | Check-in ref, assigned seat number, flight info |

---

## 🧪 Unit Tests

Tests are written using **NUnit** and **Moq**, covering all controllers in isolation.

```bash
# Run all tests
dotnet test
```

### Test Coverage Summary

| Controller | Tests | Scenarios Covered |
|------------|-------|-------------------|
| `AdminController` | 8 | Add/Update/Delete flights, duplicate number, zero fare, same source-destination |
| `AuthController` | 6 | Register, duplicate username/email, short password, login valid/invalid |
| `BookingController` | 6 | Book flight, flight not found, no seats, invalid gender, search booking |
| `CheckinController` | 4 | Valid check-in, invalid ref, already checked in, seat number in response |
| `FareController` | 4 | Valid fare, GST = 5%, final fare calc, flight not found |
| `FlightsController` | 3 | Matching flights, no matches, multiple results |

> Tests use `void` test methods with `.Result` to call async service methods synchronously.

---

## 📊 HTTP Status Codes

| Code | Meaning | When Returned |
|------|---------|---------------|
| `200 OK` | Success | GET requests — search, fare, login |
| `201 Created` | Resource created | Register, booking, check-in, add flight |
| `204 No Content` | Deleted | Flight deleted by admin |
| `400 Bad Request` | Invalid input | Missing fields, duplicate data, zero fare |
| `401 Unauthorized` | Auth failed | JWT missing, expired, or invalid |
| `403 Forbidden` | Access denied | User attempts admin-only endpoint |
| `404 Not Found` | Not found | Flight, booking, or check-in doesn't exist |
| `409 Conflict` | Duplicate | Already checked in, duplicate flight number |
| `500 Internal Server Error` | Server fault | Unhandled exception via GlobalExceptionMiddleware |

---

## 📌 Validations

| Module | Field | Rule |
|--------|-------|------|
| Register | `email` | Valid email format required |
| Register | `password` | Cannot be empty |
| Flight Search | `destination` | Must differ from source |
| Flight Search | `travel_date` | Must be today or future |
| Book Flight | `gender` | Must be `Male`, `Female`, or `Other` |
| Check-In | `booking_reference` | Must belong to logged-in user, status must be `Confirmed` |
| Admin Add Flight | `fare` | Must be greater than 0 |
| Admin Add Flight | `available_seats` | Must be greater than 0 |

---

<div align="center">

---

Built by **Arun Kumar S** · [GitHub](https://github.com/Arun200404/FlightBookingSystem)

*Flight Booking System — ASP.NET Core 8 · SQL Server · JWT · SMTP · NUnit*

</div>

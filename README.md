# Student Management System (3-Tier Architecture)

A robust, 3-tier .NET application developed as a technical assessment. The system consists of a RESTful Web API, a SQL Server database, and a Windows Forms desktop client, strictly adhering to the Separation of Concerns principle.

## Tech Stack
* **API:** ASP.NET Core 8 Web API
* **Database:** MS SQL Server, Entity Framework Core (Code-First)
* **Desktop Client:** Windows Forms, .NET Generic Host
* **Security:** JWT Authentication, Cryptographic Password Hashing
* **Patterns:** Model-View-Presenter (MVP), Dependency Injection, Repository Pattern

## Setup Instructions
1. Clone the repository: `git clone https://github.com/VolodymyrSribnyi/EleksTestTask`
2. Open the solution in Visual Studio 2022.
3. Set **WebApi** as the startup project and run `Update-Database` in the Package Manager Console to apply EF Core migrations.
4. Set the solution to **Multiple Startup Projects** (Start both `WebApi` and `WinFormsClient`).
5. Use the following test credentials to log in:
   * **Login:** `testadmin`
   * **Password:** `Pa$$w0rd`

## Architecture & Technical Decisions
* **N-Tier Separation:** The WinForms client has no direct access to the database. All data operations are securely orchestrated through RESTful API endpoints using DTOs.
* **Asynchronous UI:** All HTTP network calls from the WinForms client are strictly asynchronous (`async/await`) using `IHttpClientFactory` and `DelegatingHandler` to prevent UI blocking and socket exhaustion.
* **MVP Pattern & DI:** The desktop client utilizes the Model-View-Presenter pattern with Passive View. Forms and Presenters are resolved via the built-in .NET Dependency Injection container (`Microsoft.Extensions.Hosting`).
* **Security Note:** Passwords are cryptographically hashed using `PasswordHasher<TUser>`. 
  > *Note for reviewers:* The JWT secret key and database connection strings are intentionally kept in `appsettings.json` to ensure the project runs out-of-the-box. In a production environment, these would be managed securely via Azure Key Vault or Environment Variables.

## Lessons Learned
During this project, I successfully integrated the modern `.NET Generic Host` into a legacy framework like Windows Forms, allowing for clean Dependency Injection, central state management (User Session), and a highly testable MVP architecture.

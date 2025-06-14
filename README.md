# Identity Service

A modular Identity Service built with .NET, following clean architecture principles.  
**This project is intended as a study of Vertical Slice Architecture, CQRS patterns, and best practices for password implementation.**  
The solution separates API, domain, and infrastructure concerns for better maintainability and scalability.

---

## Project Structure
 <pre>
├── Identity.Api/                # API entry point and configuration
│   ├── Extensions/
│   ├── Properties/
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── Identity.Api.csproj
├── Identity.Domain/             # Domain entities and core logic
│   ├── Entities/
│   ├── Interfaces/
│   └── Identity.Domain.csproj
├── Identity.Infrastructure/     # Data access and persistence
│   ├── Data/
│   ├── Migrations/
│   └── Identity.Infrastructure.csproj
├── Identity.sln                 # Solution file
└── .gitignore
 </pre>

---

## Getting Started

1. **Clone the repository:**
    ```sh
    git clone https://github.com/lucaslgm/identity-service.git
    cd identity-service
    ```

2. **Open the solution:**
    - Open `Identity.sln` with Visual Studio or your preferred IDE.

3. **Configure the application:**
    - Update `appsettings.json` and `appsettings.Development.json` as needed.

4. **Apply database migrations:**
    - Ensure your database is set up using the migrations in `Identity.Infrastructure/Migrations`.

5. **Run the API:**
    - Start the `Identity.Api` project.

---

## Author

- [lucaslgm](https://github.com/lucaslgm)

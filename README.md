### 📅 EventManager API
A comprehensive ASP.NET Core Web API designed to manage events, locations, and attendees. This project was developed as a final assignment to demonstrate advanced backend development skills, including complex entity relationships and business logic validation.

### 🏗 Architecture & Patterns
Clean Controller Structure: Business logic is decoupled from controllers and moved into the Service layer (Controller -> Service -> Storage).

Dependency Injection (DI): Leveraged for maintaining loosely coupled components and efficient database context management.

Database Management: Powered by SQL Server and Entity Framework Core with structured migrations.

### 🌟 Key Features & Business Logic
Location Management: Full CRUD functionality for event venues, including city and specific details.

Event Coordination: * One-to-Many Relationship: Each event is linked to exactly one location, while one location can host multiple events.

Time Validation: Built-in logic to prevent creating events where the end time is earlier than the start time.

Integrity Check: Strict validation ensures events cannot be created for non-existent locations.

Attendee System (Bonus Feature):

Many-to-Many Relationship: Implemented via a Registration join table to connect Events and Attendees.

Duplicate Prevention: Robust logic to ensure an attendee cannot register for the same event more than once.

Smart Filtering: Ability to fetch all events associated with a specific location or retrieve all events a specific person is attending.

### 🛠 Tech Stack
Framework: .NET 8 Web API

ORM: Entity Framework Core

Database: SQL Server

Validation: Custom input validation returning appropriate HTTP Status Codes (400, 404, 409).

Documentation: Fully interactive Swagger/OpenAPI UI for testing all endpoints.

### 🚀 Setup Instructions
Clone the repo: git clone <your-repository-url>

Database Setup: Configure your connection string in appsettings.json.

Apply Migrations: Run Update-Database in the Package Manager Console.

Launch: Run the application to explore the API via Swagger.

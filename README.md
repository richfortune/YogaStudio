# YogaStudio

YogaStudio is a modern management application for Yoga studios. It features a complete booking engine, student/instructor management, and scheduling.
This repository contains both the .NET 8 ASP.NET Core Web API (Backend) and the React Application (Frontend).

## Architecture

The Backend is built strictly adhering to Clean Architecture principles. E.g.
- Pure Domain entities with NO dependencies on Entity Framework.
- Repository pattern and DbContext configuration abstracted in the Infrastructure layer.
- PostgreSQL database integration.

## Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js (v18+)
- PostgreSQL Server

### Backend Setup
1. `cd src/Backend/YogaStudio.Api`
2. Configure your `appsettings.Development.json` with the PostgreSQL connection string.
3. Run Entity Framework commands to apply initial migrations:
   `dotnet ef database update`
4. Start the API locally:
   `dotnet run`

### Frontend Setup
*(Instructions will be added once React is initialized)*

## Contributing & Governance
Please refer to [docs/repository_governance.md](docs/repository_governance.md) for detailed guidelines on the Branching Strategy, Commit Conventions, and PR Workflow.

## License
MIT License.

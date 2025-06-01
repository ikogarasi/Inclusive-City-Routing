# Inclusive City Project

This project consists of three main components:

1. Frontend (React/TypeScript with Vite)
2. Backend (ASP.NET Core Web API)
3. OSRM Routing Server (Docker-based)

## Prerequisites

- [Node.js](https://nodejs.org/) (v14 or higher)
- [.NET SDK](https://dotnet.microsoft.com/download) (8.0 or higher)
- [Docker](https://www.docker.com/products/docker-desktop/) and Docker Compose

## Project Structure

- `frontend/` - React TypeScript frontend application built with Vite
- `InclusiveCity/` - .NET Core backend API and related projects
- `OSRM-API/` - Open Source Routing Machine API running in Docker

## Getting Started

### 1. Starting the Backend (.NET API)

1. Configure your database connection string in `InclusiveCity.API/appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Username=postgres;Password=yourpassword;Database=InclusiveCity"
   }
   ```

2. Configure Azure Blob Storage (if needed) in the same file:

   ```json
   "BlobConnectionString": "DefaultEndpointsProtocol=https;AccountName=youraccountname;AccountKey=youraccountkey;EndpointSuffix=core.windows.net",
   "BlobReviewsImagesContainerName": "reviews",
   "BlobStructureImagesContainerName": "structure"
   ```

3. Apply database migrations:

   ```powershell
   cd InclusiveCity
   dotnet ef database update --project InclusiveCity.Persistence --startup-project InclusiveCity.API
   ```

4. Open the solution in Visual Studio:

   ```powershell
   cd InclusiveCity
   start InclusiveCity.sln
   ```

5. Or build and run using the .NET CLI:
   ```powershell
   cd InclusiveCity
   dotnet build
   cd InclusiveCity.API
   dotnet run
   ```

The API will be available at `https://localhost:7133` or `http://localhost:5133` (the exact ports may vary based on your configuration).

### 2. Starting the Frontend

1. Install dependencies:

   ```powershell
   cd frontend
   npm install
   ```

2. Run the development server:
   ```powershell
   npm run dev
   ```

The frontend will be available at `http://localhost:5173` (default Vite port).

### 3. Starting the OSRM Routing Server

1. Start the Docker containers:

   ```powershell
   cd OSRM-API
   docker-compose up -d
   ```

2. The OSRM API will be available at `http://localhost:5000`.

## Development

### Frontend

The frontend is built with:

- React 18
- TypeScript
- Vite as the build tool
- Material UI for components
- React Router for navigation
- Redux Toolkit for state management
- Leaflet for maps

Common commands:

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run lint` - Run ESLint
- `npm run preview` - Preview production build

### Backend

The backend is built with:

- ASP.NET Core Web API
- Entity Framework Core
- Clean Architecture pattern with:
  - API layer
  - Application layer
  - Domain layer
  - Infrastructure layer
  - Persistence layer

### OSRM Routing Server

The OSRM server is containerized with Docker and includes:

- PostgreSQL database for storing route data
- OSRM engine configured for accessibility routing
- Custom routing profiles for inclusive city navigation

## API Documentation

Once the backend is running, you can access the Swagger documentation at:
`https://localhost:7133/swagger` or `http://localhost:5133/swagger`

## Additional Information

- The frontend and backend are designed to work together but can be developed independently.
- The OSRM server requires significant resources for initial processing of map data.
- Make sure all required ports (5000, 5133/7133, 5173, 5444) are available on your system.

## Database Migrations

To create a new migration after model changes:

```powershell
cd InclusiveCity
dotnet ef migrations add MigrationName --project InclusiveCity.Persistence --startup-project InclusiveCity.API
```

To remove the last migration (if it hasn't been applied):

```powershell
dotnet ef migrations remove --project InclusiveCity.Persistence --startup-project InclusiveCity.API
```

## Configuration

### Database Configuration

The application uses PostgreSQL by default. You can change the connection string in `InclusiveCity.API/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Username=postgres;Password=yourpassword;Database=InclusiveCity"
}
```

### Azure Blob Storage Configuration

For storing images, the application uses Azure Blob Storage. Configure it in `InclusiveCity.API/appsettings.json`:

```json
"BlobConnectionString": "DefaultEndpointsProtocol=https;AccountName=youraccountname;AccountKey=youraccountkey;EndpointSuffix=core.windows.net",
"BlobReviewsImagesContainerName": "reviews",
"BlobStructureImagesContainerName": "structure"
```

### OSRM API Configuration

The routing API URL can be configured in `InclusiveCity.API/appsettings.json`:

```json
"OsrmApi": {
  "GetComputedRouteUrl": "http://localhost:5000/route/v1/walking/<:lon1>,<:lat1>;<:lon2>,<:lat2>?steps=true&geometries=polyline6"
}
```

## Troubleshooting

- If you encounter issues with the OSRM server, check Docker logs:

  ```powershell
  docker logs osrm_routing
  ```

- For database connection issues:
  ```powershell
  docker logs osrm_postgres
  ```

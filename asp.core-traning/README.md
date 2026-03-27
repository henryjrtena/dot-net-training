# Todo List Tutorial Application

This workspace now contains the full sample application described across the tutorial series:

- `TodoList.Web`: ASP.NET Core MVC app with todos, validation, custom routes, admin area, and a view component
- `TodoList.Api`: ASP.NET Core Web API with Swagger, CORS, rate limiting, JWT login, and protected todo endpoints
- `TodoList.Client`: React + Vite frontend that calls the API, creates todos, logs in, and loads protected data

## Projects

- `TodoListApp.sln`
- `TodoList.Web/`
- `TodoList.Api/`
- `TodoList.Client/`

## Run the MVC app

```powershell
dotnet run --project TodoList.Web
```

Open:

- `https://localhost:7216`
- `http://localhost:5233`

## Run the API

If your machine has a private Telerik feed configured globally, use the public NuGet source explicitly for restore/build:

```powershell
dotnet restore TodoList.Api\TodoList.Api.csproj --source https://api.nuget.org/v3/index.json
dotnet run --project TodoList.Api
```

Open:

- `https://localhost:7275/swagger`
- `http://localhost:5171/swagger`

## Run the React client

```powershell
cd TodoList.Client
npm install
npm run dev
```

Open:

- `http://localhost:5173`

The default API base URL is already set in code to `https://localhost:7275/api`.

## Notes

- JWT keys are stored in `TodoList.Api/keys/`.
- Keep the student workspace rooted in `asp.core-traning/` so everything stays easy to manage in Git.

# Reference — dotnet-csharp scaffold templates

## Location — all interview scaffolds

Every project created by this skill goes under **`src/Interview/`** and is added to **`src/Interview/Interview.sln`**.

```
src/Interview/
├── Interview.sln
└── {TopicFolder}/
    ├── {TopicFolder}.csproj
    └── Program.cs (+ optional Models/, Services/, *.http)
```

**Run:** `dotnet run --project src/Interview/{TopicFolder}`  
**Build:** `dotnet build src/Interview/Interview.sln`

## Single-file educational console

Use for language fundamentals (arrays, types, exceptions). One `Program.cs` with `Main`, overview comments, and runnable samples.

**`{TopicFolder}.csproj`**

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net8.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    </PropertyGroup>

</Project>
```

**`Program.cs` skeleton**

```csharp
// Run: dotnet run --project src/Interview/{TopicFolder}

// Overview comments: what the topic is, when to use it, key rules.

// Runnable examples with line-by-line comments.
```

For top-level statements (preferred on `net8.0`), omit `namespace` and `Main` — see `src/Interview/AsyncAwait/Program.cs`.

## Interview practice scaffold

Use when the user wants hands-on exercises. Requirements:

- Numbered exercises with clear goals
- `TODO` markers where the user implements code
- One working reference example
- `Interview tip:` lines for common interview points
- Optional `*.http` file if HTTP is involved

Exercise template:

```csharp
// =============================================================================
// EXERCISE {N} — {short title}
// =============================================================================
// Goal: {one sentence}
//
// Steps:
//   1. ...
//   2. ...
//
// Test: {how to verify}
//
// Interview tip: ...

// TODO: Implement Exercise {N} here
```

## Web API practice scaffold

Use for REST, minimal APIs, controllers, middleware topics.

**Project creation**

```bash
dotnet new webapi -n {TopicFolder} -o src/Interview/{TopicFolder} --use-minimal-apis --no-openapi -f net8.0
dotnet sln src/Interview/Interview.sln add src/Interview/{TopicFolder}/{TopicFolder}.csproj
```

**Typical layout**

```
src/Interview/{TopicFolder}/
├── {TopicFolder}.csproj
├── Program.cs           # Exercises 1–N (Minimal APIs)
├── Controllers/         # Optional later exercise (controller style)
├── Models/              # Domain + request DTOs
├── Services/            # IRepository + in-memory implementation (pre-built)
├── Properties/launchSettings.json
├── appsettings.json
└── {TopicFolder}.http
```

**Pre-build for the user (agent implements)**

- `Models/` — records for domain and DTOs
- `Services/IBookRepository.cs` + in-memory impl (rename entity to fit topic)
- `Program.cs` — DI registration, pipeline, exercises, one working `/health` route
- `launchSettings.json` — set `launchUrl` to the health route
- `.http` file — requests per exercise with `@host = https://localhost:{port}`

**Leave for the user (TODO)**

- `MapGet` / `MapPost` / `MapPut` / `MapDelete` handlers
- Controller actions if Exercise N covers `[ApiController]`

**`Program.cs` Web API header**

```csharp
// Run:  dotnet run --project src/Interview/{TopicFolder}
// Test: use {TopicFolder}.http
//
// REST cheat sheet:
//   GET    /resources      → 200 OK
//   GET    /resources/{id} → 200 or 404
//   POST   /resources      → 201 Created
//   PUT    /resources/{id} → 200 or 404
//   DELETE /resources/{id} → 204 No Content or 404
```

**DI registration pattern**

```csharp
builder.Services.AddSingleton<IEntityRepository, EntityRepository>();
// For controller exercise (commented until needed):
// builder.Services.AddControllers();
// app.MapControllers();
```

## Multi-file educational

Use when the topic has natural subtopics (e.g. `for` / `while` / `foreach` each deserve a file). Still place under `src/Interview/{TopicFolder}/` unless the topic is a large legacy-style reference (see `src/StatementsExpressionsOperators/` for historical layout only).

```
src/Interview/{TopicFolder}/
├── {TopicFolder}.csproj
├── Program.cs          # calls into topic files or runs demos
└── {ConceptName}.cs    # one concept per file
```

- Each file: header comment + focused runnable examples.
- `Program.cs` invokes demos or documents entry points.

## Adding a project to Interview.sln

Prefer the CLI:

```bash
dotnet sln src/Interview/Interview.sln add src/Interview/{TopicFolder}/{TopicFolder}.csproj
```

Manual entry template (if needed):

```text
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "{TopicFolder}", "{TopicFolder}\{TopicFolder}.csproj", "{NEW-GUID-HERE}"
EndProject
```

Add matching `ProjectConfigurationPlatforms` entries for Debug|Any CPU and Release|Any CPU.

## HTTP test file template

```http
@host = https://localhost:{port from launchSettings.json}

### Reference — health (works out of the box)
GET {{host}}/{resource}/health

### Exercise 1
GET {{host}}/{resource}
```

## Topic → exercise ideas (quick reference)

| Topic area | Suggested exercises |
|------------|---------------------|
| Endpoints / REST | CRUD, route params, query strings, status codes, controllers |
| LINQ | Where/Select, grouping, aggregation, deferred execution |
| Dependency Injection | Register services, lifetimes, constructor injection |
| Async/await | async HTTP calls, Task.WhenAll, cancellation, InnerException |
| Entity Framework | DbContext, migrations, CRUD, includes |
| Middleware | custom middleware, ordering, short-circuiting |
| Validation | Data annotations, minimal API validation, ProblemDetails |
| Testing | xUnit facts, WebApplicationFactory, mocking repository |

Tailor exercises to the exact topic the user provides; do not implement all rows.

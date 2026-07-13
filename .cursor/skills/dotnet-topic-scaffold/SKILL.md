---
name: dotnet-topic-scaffold
description: >-
  Creates new C# practice scaffold projects in the dotnet-csharp repository for
  interview prep and topic learning. Use when the user asks to create a new
  topic project, practice scaffold, learning exercise, or interview prep module,
  and provides a topic name (e.g. "LINQ", "Create endpoints", "Dependency Injection").
---

# dotnet-csharp Topic Scaffold

Create a new practice project under `src/` when the user names a topic. The user fills in TODOs; you supply structure, comments, and interview tips.

## Before creating anything

1. **Read the topic** from the user's message. If missing, ask: *"What topic should this practice project cover?"*
2. **Check for collisions**: `Glob` `src/{TopicName}/` — do not overwrite an existing topic without explicit confirmation.
3. **Pick a scaffold type** (see table below). If unclear, default to **interview practice** when the user mentions interviews, exercises, or endpoints; otherwise **educational console**.

| Signal in topic | Scaffold type | Template |
|-----------------|---------------|----------|
| endpoints, API, REST, Web API, controllers | Web API practice | [reference.md — Web API](reference.md#web-api-practice-scaffold) |
| interview, practice, exercises, TODO | Console or Web practice (with TODO blocks) | [reference.md — Practice](reference.md#interview-practice-scaffold) |
| broad area with subtopics (e.g. statements, generics) | Multi-file educational | [reference.md — Multi-file](reference.md#multi-file-educational) |
| default / language concept | Single-file educational console | [reference.md — Console](reference.md#single-file-educational-console) |

4. **Read one nearby example** in `src/` that matches the chosen type (e.g. `src/CreateEndpoints/` for Web API, `src/Arrays/` for console).

## Naming and layout

```
src/{TopicFolder}/
├── {TopicFolder}.sln
└── {TopicFolder}Project/
    ├── {TopicFolder}Project.csproj
    ├── Program.cs
    └── (optional) Models/, Services/, Controllers/, *.http
```

| User says | TopicFolder | Project / namespace |
|-----------|-------------|---------------------|
| "create endpoints" | `CreateEndpoints` | `CreateEndpointsProject` |
| "LINQ" | `Linq` | `LinqProject` |
| "dependency injection" | `DependencyInjection` | `DependencyInjectionProject` |

Rules:
- PascalCase folder and solution names; no spaces.
- Project folder always `{TopicFolder}Project`; namespace matches.
- Generate a new GUID for each `.sln` project entry (`[System.Guid]::NewGuid()` or equivalent).

## Target framework

| Project kind | TFM |
|--------------|-----|
| New console / class library practice | `net8.0` |
| New Web API practice | `net8.0` (`Microsoft.NET.Sdk.Web`) |
| Adding to an existing topic folder | Match that topic's `.csproj` TFM |

Do not downgrade existing topics to `netcoreapp3.1` unless the user asks.

## Content requirements

Every scaffold must include:

1. **Header comment block** in `Program.cs` (or main file): topic title, how to run (`dotnet run --project src/...`), and what the user will learn.
2. **Educational comments** explaining concepts (concise, interview-oriented when practice type).
3. **At least one working reference** snippet or endpoint the user can run immediately.
4. **Progressive exercises** for practice scaffolds: numbered `EXERCISE N` sections with `TODO` and step-by-step hints.
5. **Interview tips** inline where relevant (status codes, DI lifetimes, when to use which API, etc.).

Do **not** add:
- `README.md` unless the user asks
- Solution answers (unless user asks for an answer key / solutions branch)
- `.idea/` or `bin/` / `obj/` artifacts

## Implementation workflow

Copy this checklist and track progress:

```
- [ ] 1. Resolve topic name and scaffold type
- [ ] 2. Create directory structure and .sln / .csproj
- [ ] 3. Add Program.cs (and supporting files if needed)
- [ ] 4. Add .http test file for Web API scaffolds
- [ ] 5. dotnet build — fix errors before finishing
- [ ] 6. Summarize for user: path, how to run, exercise list
```

### Step 1 — Scaffold with CLI when appropriate

```bash
# Console
dotnet new console -n {TopicFolder}Project -o src/{TopicFolder}/{TopicFolder}Project -f net8.0

# Web API (minimal APIs, no OpenAPI template noise)
dotnet new webapi -n {TopicFolder}Project -o src/{TopicFolder}/{TopicFolder}Project --use-minimal-apis --no-openapi -f net8.0
```

Then replace generated boilerplate with the repo's commented, exercise-driven style.

### Step 2 — Practice exercise pattern

Use this structure in `Program.cs`:

```csharp
// =============================================================================
// {TOPIC} — Interview practice scaffold
// =============================================================================
// Run: dotnet run --project src/{TopicFolder}/{TopicFolder}Project

// REFERENCE — working example (leave runnable)
// ...

// EXERCISE 1 — short goal
// Steps: 1. ... 2. ... 3. ...
// Interview tip: ...
// TODO: Implement Exercise 1 here
```

For Web API topics, pre-implement **repository/service layer** so the user only writes endpoints. Throw `NotImplementedException` only in controller actions if controllers are an optional later exercise.

### Step 3 — Verify

```bash
dotnet build src/{TopicFolder}/{TopicFolder}.sln
```

Build must succeed. The only failing behavior at runtime should be intentional `TODO` / `NotImplementedException` on exercises not yet implemented.

### Step 4 — Commit and push

Only when the user explicitly asks. Use Conventional Commits, e.g. `feat({scope}): add scaffold project for {topic} practice`.

## Quality bar

- Match tone and comment density of `src/Arrays/ArraysProject/Program.cs` (educational) or `src/CreateEndpoints/CreateEndpointsProject/Program.cs` (interview practice).
- Minimal scope: no unrelated refactors elsewhere in the repo.
- Prefer records, file-scoped namespaces, and implicit usings on `net8.0` new projects.
- Keep exercises focused on the stated topic — do not bundle unrelated concepts.

## Additional resources

- File templates and copy-paste blocks: [reference.md](reference.md)
- Worked examples in this repo: [examples.md](examples.md)

# Examples in dotnet-csharp

Use these as style references when creating new scaffolds. **New scaffolds always go under `src/Interview/`** and are added to `Interview.sln`.

## Interview practice — Web API

**Path:** `src/Interview/CreateEndpoints/`

**What to copy:**
- Exercise numbering and `TODO` blocks in `Program.cs`
- Pre-built `Services/` + `Models/` so user focuses on endpoints
- `CreateEndpoints.http` for testing
- `Interview tip:` comments on status codes, DI lifetimes, REST verbs
- Optional `Controllers/` for a later exercise (commented `AddControllers` / `MapControllers`)

**Run:**
```bash
dotnet run --project src/Interview/CreateEndpoints
```

## Interview practice — external HTTP client

**Path:** `src/Interview/ExternalRequestApi/`

**What to copy:**
- Pre-built typed `HttpClient` service in `Services/`
- Configuration via `IOptions<T>` and `appsettings.json`
- Minimal API exercises that proxy an external API
- `ExternalRequestApi.http` and optional Postman collection

**Run:**
```bash
dotnet run --project src/Interview/ExternalRequestApi
```

## Interview practice — console (async / exceptions)

**Path:** `src/Interview/AsyncAwait/`

**What to copy:**
- Top-level statements with `await` in the entry point
- Working REFERENCE section + numbered EXERCISE blocks with `TODO`
- `#pragma warning disable CS8321` for unused exercise stubs until the user uncomments them
- Interview tips on exception handling, `InnerException`, and `throw;` vs `throw ex;`

**Run:**
```bash
dotnet run --project src/Interview/AsyncAwait
```

## Educational console — single file (legacy layout, style only)

**Path:** `src/Arrays/ArraysProject/Program.cs`

**What to copy:**
- Top-of-file concept overview (multi-line comment block)
- Inline comments on every meaningful line
- Progressive examples in `Main` (declare → initialize → variations)

Do **not** create new projects at `src/{Topic}/{Topic}Project/` — use `src/Interview/{Topic}/` instead.

## Educational — multiple files per concept (legacy layout, style only)

**Path:** `src/StatementsExpressionsOperators/Statements/`

**What to copy:**
- One file per language feature (`IterationStatements_for.cs`, etc.)
- Subfolder grouping (`Iteration/`, `Selection/`, `Jump/`)

For new multi-file scaffolds, prefer `src/Interview/{TopicFolder}/` with concept files alongside `Program.cs`.

## Commit message example

After user requests commit:

```
feat(interview): add scaffold project for LINQ practice

Add exercises for Where/Select, grouping, and deferred execution
with commented examples and TODO blocks.
```

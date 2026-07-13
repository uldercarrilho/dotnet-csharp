# Examples in dotnet-csharp

Use these as style references when creating new scaffolds.

## Interview practice — Web API

**Path:** `src/CreateEndpoints/`

**What to copy:**
- Exercise numbering and `TODO` blocks in `Program.cs`
- Pre-built `Services/` + `Models/` so user focuses on endpoints
- `CreateEndpointsProject.http` for testing
- `Interview tip:` comments on status codes, DI lifetimes, REST verbs
- Optional `Controllers/` for a later exercise (commented `AddControllers` / `MapControllers`)

**Run:**
```bash
dotnet run --project src/CreateEndpoints/CreateEndpointsProject
```

## Educational console — single file

**Path:** `src/Arrays/ArraysProject/Program.cs`

**What to copy:**
- Top-of-file concept overview (multi-line comment block)
- Inline comments on every meaningful line
- Progressive examples in `Main` (declare → initialize → variations)
- Namespace + class `Program` pattern

## Educational — multiple files per concept

**Path:** `src/StatementsExpressionsOperators/Statements/`

**What to copy:**
- One file per language feature (`IterationStatements_for.cs`, etc.)
- Subfolder grouping (`Iteration/`, `Selection/`, `Jump/`)
- Separate `.csproj` per sub-area when scope is large

## Educational — generics split across files

**Path:** `src/Generics/GenericsProject/`

**What to copy:**
- `GenericMethods.cs`, `GenericInterfaces.cs` alongside `Program.cs`
- Overview comments tied to Microsoft docs concepts
- `Program.cs` orchestrates or documents entry points

## Commit message example

After user requests commit:

```
feat(linq): add scaffold project for LINQ interview practice

Add exercises for Where/Select, grouping, and deferred execution
with commented examples and TODO blocks.
```

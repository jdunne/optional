# Repository Guidelines

## Project Structure & Module Organization
- `optional.sln` is the solution entry point.
- `Optional/` contains the library code (`Optional.cs`) and targets `net9.0`.
- `Optional.Tests/` contains xUnit tests (`Optional_Tests.cs`) and references the library project.
- `tmp/` and `output/` are local working/output folders and are not part of core source.

## Build, Test, and Development Commands
- `dotnet restore optional.sln`: restore NuGet dependencies.
- `dotnet build optional.sln -c Release`: compile library and tests in Release mode.
- `dotnet test optional.sln -c Release`: run all xUnit tests.
- `dotnet test Optional.Tests/Optional.Tests.csproj --filter TestOptionalWithValue`: run a focused test during iteration.

Run commands from the repository root so project references resolve consistently.

## Coding Style & Naming Conventions
- Language is C# (`net9.0`), with 4-space indentation and braces on new lines (Allman style).
- Prefer clear, small APIs and guard clauses (`ArgumentNullException.ThrowIfNull(...)`) for public inputs.
- Use PascalCase for types/methods/properties (`Optional<T>`, `OrElseGet`), camelCase for locals/parameters, and `_camelCase` for private fields.
- Keep XML doc comments on public APIs concise and accurate.

## Testing Guidelines
- Framework: xUnit (`[Fact]`) in `Optional.Tests`.
- Add/adjust tests for every behavior change, especially `HasValue`, `Value`, `Map`, `Filter`, and exception paths.
- Name tests descriptively by scenario and expected behavior (for example, `Map_ReturnsEmpty_WhenTransformReturnsNull`).
- Ensure `dotnet test optional.sln` passes before opening a PR.

## Commit & Pull Request Guidelines
- Follow existing history style: short, imperative, sentence-case commit messages (for example, `Handle null results in Map`).
- Keep commits focused; avoid mixing refactors with behavior changes.
- PRs should include:
  - concise summary of what changed and why,
  - linked issue (if applicable),
  - test evidence (command run and result),
  - notes on any API or behavior changes.

## Security & Configuration Notes
- Do not commit secrets or machine-specific settings; `.gitignore` already excludes common Visual Studio artifacts.
- Keep target frameworks and test package versions aligned when upgrading dependencies.

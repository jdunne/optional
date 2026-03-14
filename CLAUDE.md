# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

A C# port of Java 8's `Optional<T>` — a container type that may or may not hold a non-null value. The library targets `net9.0` and uses xUnit for testing.

## Commands

Run all commands from the repository root.

```bash
dotnet restore optional.sln
dotnet build optional.sln -c Release
dotnet test optional.sln -c Release

# Run a single test by name
dotnet test Optional.Tests/Optional.Tests.csproj --filter TestOptionalWithValue
```

## Architecture

Two types in `Optional/Optional.cs` within the `Optional` namespace:

- **`Optional` (static class)** — factory methods: `From<T>` (non-null ref), `FromNullable<T>` (nullable ref), `FromValue<T>` (value type), `Empty<T>`.
- **`Optional<T>` (struct)** — the container itself. Implements `IEquatable<Optional<T>>`. Key methods: `HasValue`, `Value`, `OrElse`, `OrElseGet`, `OrElseThrow<E>`, `IfHasValue`, `Filter`, `Map`.

`Map` treats a null transform result as empty (returns `Optional.Empty<U>()`).

## Coding Conventions

- C# with 4-space indentation, Allman-style braces.
- PascalCase for types/methods/properties, camelCase for locals/parameters, `_camelCase` for private fields.
- Guard public inputs with `ArgumentNullException.ThrowIfNull(...)`.
- Keep XML doc comments on all public APIs.

## Testing Conventions

- xUnit `[Fact]` tests in `Optional.Tests/Optional_Tests.cs`.
- Name tests by scenario: `Map_ReturnsEmpty_WhenTransformReturnsNull`.
- Cover both the happy path and exception paths (null guards, `InvalidOperationException` on empty `Value`).

## Commit Style

Short, imperative, sentence-case messages (e.g. `Handle null results in Map`). Keep commits focused — don't mix refactors with behavior changes.

# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

- Build: `dotnet build`
- Run the API: `dotnet run --project src/AuctionHouse.API`
- Restore packages: `dotnet restore`

There is no test project in the solution yet. When one is added (e.g. `AuctionHouse.Domain.Tests`), the standard commands will apply: `dotnet test` for the whole suite, `dotnet test --filter "FullyQualifiedName~TestName"` for a single test.

## Architecture

This is a .NET 10 solution (`AuctionHouse.slnx`) structured as a Clean Architecture / layered design under `src/`:

- **AuctionHouse.Domain** — enterprise/business logic. Contains `Entities/`, `ValueObjects/`, `Enums/`, `Events/`, `Exceptions/`, `Repositories/` (interfaces), `Abstractions/`. This layer should have no dependency on the other projects.
- **AuctionHouse.Application** — use cases / application services that orchestrate the domain.
- **AuctionHouse.Infrastructure** — implementations of domain-defined interfaces (persistence, external services, etc.).
- **AuctionHouse.API** — ASP.NET Core Web API entry point (`Program.cs`), using minimal APIs (no controllers) with `AddOpenApi()`/`MapOpenApi()` for OpenAPI docs.

**Current state:** the solution is freshly scaffolded. The `.csproj` files do not yet declare `ProjectReference`s between layers, and `Application`/`Infrastructure`/`Domain` each still contain a placeholder `Class1.cs`. `Program.cs` still has the default `dotnet new webapi` weather-forecast sample endpoint. The `Domain/Enums/AuctionStatus.cs` file defines a `Status` enum (`Active`, `Inactive`) — expect the domain model (entities, value objects, repositories) to be built out incrementally from here, so don't assume folders under `Domain/` are populated just because they exist.

When wiring up new layers, remember the dependency direction is inward: `API → Application → Domain`, with `Infrastructure → Domain`/`Application` for implementing domain interfaces. `Domain` itself should not reference any other project in the solution.

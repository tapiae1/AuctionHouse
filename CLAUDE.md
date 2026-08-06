# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

- Build: `dotnet build`
- Run the API: `dotnet run --project src/AuctionHouse.API`
- Restore packages: `dotnet restore`
- Run tests: `dotnet test` for the whole suite, `dotnet test --filter "FullyQualifiedName~TestName"` for a single test.

## Architecture

This is a .NET 10 solution (`AuctionHouse.slnx`) structured as a Clean Architecture / layered design under `src/`, with tests under `tests/`:

- **AuctionHouse.Domain** — enterprise/business logic. No `ProjectReference`s (correctly has no dependency on the other projects).
  - `Entities/`: `Auction`, `Bid`, `User` — each uses a private constructor plus a static `Create(...)` factory method. `Auction.PlaceBid(amount)` holds the bidding invariants (must be `Active`, before `EndTime`, bid strictly higher than `CurrentBid`).
  - `Enums/AuctionStatus.cs`: `Scheduled`, `Active`, `Ended`, `Cancelled`.
  - `Exceptions/DomainException.cs`.
  - `Repositories/`: `IAuctionRepository`, `IBidRepository`, `IUserRepository` — interfaces only.
  - `ValueObjects/`, `Events/`, `Abstractions/` — folders exist but are still empty; don't assume they're populated.
- **AuctionHouse.Application** — references Domain. No use-case/service classes yet; this layer is still just wired up, not implemented.
- **AuctionHouse.Infrastructure** — references Domain. Implements `AuctionRepository`, `BidRepository`, `UserRepository` under `Repositories/`.
- **AuctionHouse.API** — references Application. `Program.cs` is minimal-API boilerplate (`AddOpenApi()`/`MapOpenApi()`, `UseHttpsRedirection()`) — the weather-forecast sample has been removed, but no real endpoints have been added yet.
- **tests/AuctionHouse.Infrastructure.Tests** — xUnit project referencing Infrastructure. Currently covers `AuctionRepository` (`AuctionRepositoryTests.cs`). No Domain or Application test projects yet.

When wiring up new layers, remember the dependency direction is inward: `API → Application → Domain`, with `Infrastructure → Domain`/`Application` for implementing domain interfaces. `Domain` itself should not reference any other project in the solution.

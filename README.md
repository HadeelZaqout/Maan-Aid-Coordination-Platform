# معاً | Ma'an — Aid Coordination Platform

A full-stack aid/donation coordination platform connecting **beneficiaries**, **donors**, and **field coordinators** through a transparent, auditable workflow — from request submission to verified delivery.

Built solo, end to end, with a clean Service Layer architecture and a full audit trail of every status change.

## Live Demo
*(add your deployed link here once live, or remove this section)*

## The Problem
Aid coordination often lacks transparency — beneficiaries don't know the status of their request, donors don't know where their donation goes, and coordinators lack a structured way to verify and track requests. Ma'an solves this with a clear, role-based workflow where every action is logged.

## How It Works
Every request moves through four tracked stages:

`Submitted` → `Verified` → `Matched` → `Delivered`

- **Beneficiaries** submit and track their own requests
- **Coordinators** verify requests and move them through the workflow
- **Donors** browse verified requests and contribute
- Every status change is recorded in a full **audit trail** (`StatusHistory`)

## Tech Stack
- ASP.NET Core (Razor Pages)
- Entity Framework Core (Code-First)
- ASP.NET Identity (role-based auth: Beneficiary / Donor / Coordinator / Admin)
- SQL Server
- Bootstrap + custom RTL Arabic UI

## Architecture Highlights
- **Service Layer + Interfaces** — business logic separated from Razor Pages via `IDonationRequestService`, `IDonaationService`, `IStatusHistoryService`, `IDistributionService`
- **ViewModel Pattern** — forms bind to dedicated ViewModels, not EF entities directly
- **Role-Based Authorization** — `[Authorize(Roles = "...")]` on every protected page
- **Audit Trail** — every status transition is logged with who changed it, when, and from/to which status
- **Ownership checks** — users can only view/edit their own records (protected against ID enumeration)

## Core Models
| Model | Purpose |
|---|---|
| `DonationRequest` | A beneficiary's aid request, with category, urgency, and status |
| `Donaation` | A donor's contribution toward a specific request |
| `StatusHistory` | Full audit log of every status change |
| `Distribution` | Final delivery confirmation by a coordinator |

## Running Locally
1. Clone the repo
2. Update the connection string in `appsettings.json`
3. Run `dotnet ef database update` to apply migrations
4. Run the project — roles are seeded automatically on first run

## Screenshots
*(add 2-3 screenshots here: landing page, request form, coordinator dashboard)*

---
Built by [Hadeel Zaqout](https://github.com/HadeelZaqout)

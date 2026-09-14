<div align="center">

# معاً | Ma'an
### Aid Coordination Platform

*Connecting beneficiaries, donors, and field coordinators through a transparent, auditable workflow.*

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-512BD4?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Identity](https://img.shields.io/badge/ASP.NET_Identity-5C2D91?style=for-the-badge)

</div>

---

## 📋 The Problem

Aid coordination often lacks transparency — beneficiaries don't know the status of their request, donors don't know where their donation goes, and coordinators lack a structured way to verify and track requests.

**Ma'an** solves this with a clear, role-based workflow where every action is tracked and logged — built solo, end to end.

## 🔄 How It Works

Every request moves through four tracked stages:

<div align="center">

`Submitted` → `Verified` → `Matched` → `Delivered`

</div>

- **Beneficiaries** submit and track their own requests
- **Coordinators** verify requests and move them through the workflow
- **Donors** browse verified requests and contribute
- Every status change is recorded in a full **audit trail** (`StatusHistory`)

## 🖼️ Screenshots

<table>
<tr>
<td width="50%">

**Landing Page**
Live stats pulled directly from the database — not hardcoded.

![Landing](./screenshots/Screenshot%202026-09-14%20232003.png)

</td>
<td width="50%">

**Submit a Request** — Beneficiary Dashboard
Category-colored accent, clean form validation.

![Request Form](./screenshots/Screenshot%202026-09-14%20232041.png)

</td>
</tr>
<tr>
<td width="50%">

**Browse & Donate** — Donor Dashboard
Only shows requests that passed coordinator verification.

![Donor Dashboard](./screenshots/Screenshot%202026-09-14%20232101.png)

</td>
<td width="50%">

**Manage All Requests** — Coordinator Dashboard
Status-aware actions: change status or confirm delivery.

![Coordinator Dashboard](./screenshots/Screenshot%202026-09-14%20232120.png)

</td>
</tr>
</table>

## 🏗️ Architecture Highlights

- **Service Layer + Interfaces** — business logic separated from Razor Pages via `IDonationRequestService`, `IDonaationService`, `IStatusHistoryService`, `IDistributionService`
- **ViewModel Pattern** — forms bind to dedicated ViewModels, not EF entities directly
- **Role-Based Authorization** — `[Authorize(Roles = "...")]` on every protected page
- **Audit Trail** — every status transition is logged with who changed it, when, and from/to which status
- **Ownership checks** — users can only view/edit their own records, protected against ID enumeration attacks

## 🧩 Core Models

| Model | Purpose |
|---|---|
| `DonationRequest` | A beneficiary's aid request, with category, urgency, and status |
| `Donaation` | A donor's contribution toward a specific request |
| `StatusHistory` | Full audit log of every status change |
| `Distribution` | Final delivery confirmation by a coordinator |

## ⚙️ Tech Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core (Razor Pages) |
| ORM | Entity Framework Core (Code-First) |
| Auth | ASP.NET Identity — 4 roles (Beneficiary, Donor, Coordinator, Admin) |
| Database | SQL Server |
| UI | Bootstrap + custom RTL Arabic theme |

## 🚀 Running Locally

```bash
git clone https://github.com/HadeelZaqout/Maan-Aid-Coordination-Platform.git
cd Maan-Aid-Coordination-Platform
```

1. Update the connection string in `appsettings.json`
2. Run `dotnet ef database update` to apply migrations
3. Run the project — roles are seeded automatically on first run

---

<div align="center">

Built solo by [Hadeel Zaqout](https://github.com/HadeelZaqout)

</div>

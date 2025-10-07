# KQ Municipality – School Registration System

A production-grade student registration platform used by educational institutions in Israel. The solution includes ASP.NET Core APIs, an ASP.NET MVC site, and a modern Next.js app with RTL support, all backed by SQL Server. Docker is supported for reproducible builds and deployments (Windows containers for Windows Server 2019; Linux containers for dev/CI).

---

## Tech Stack

- **Backend:** ASP.NET Core Web API, Entity Framework Core, JWT Authentication
- **Web (MVC):** ASP.NET MVC 5
- **Web (Modern):** Next.js (React) – `kq-reg-web-next`
- **Database:** Microsoft SQL Server
- **Integrations:** Microsoft Graph API
- **Containerization:** Docker (Windows containers on Server 2019; Linux containers for dev/CI)

---

## Overview

This system enables schools to register students and manage data securely and efficiently. It provides user management, integration with Microsoft services, and multi-language UI with full RTL support.

---

## Repository Structure (Main Apps)

- `kqapi` — ASP.NET Core Web API (Auth, business logic, EF Core)
- `kqwebmvc` — ASP.NET MVC 5 site
- `kq-reg-web-next` — Next.js (React) registration portal
- `kqsql` — SQL Server database (runs as a normal Windows service in production)

> Either frontend (MVC or Next.js) can be used; both talk to `kqapi`.

---

## Key Features

- Secure registration and login (JWT), role-based authorization
- Microsoft Graph API integration (Office 365 user sync, permissions)
- Dynamic registration forms with validation
- Admin dashboard for user management and export
- Responsive UI with RTL (Hebrew/Arabic)
- Dockerized services for reproducible builds and deployments

---

## Docker Support

### Production (Windows Server 2019)
- `kqapi`, `kqwebmvc`, `kq-reg-web-next` run as **Windows containers**
- `kqsql` stays as a **native SQL Server service** (recommended/supported)
- Images are **built outside** the server (dev laptop or CI) and **pushed to a registry** (e.g., ECR/Docker Hub)
- On the server: `docker pull` and `docker run` only

**Base images (examples):**
- .NET runtime: `mcr.microsoft.com/dotnet/aspnet:<version>-windowsservercore-ltsc2019`
- .NET SDK (build): `mcr.microsoft.com/dotnet/sdk:<version>-windowsservercore-ltsc2019`
- Node/Next.js on Windows: `mcr.microsoft.com/windows/servercore:ltsc2019` (with Node installed) or a Node-on-Windows image

### Dev / CI (Linux containers)
- Equivalent Dockerfiles can target Linux bases for faster local builds/CI
- CI builds → push to registry → deploy to production (Windows containers)

> Note: Windows Server 2019 runs **Windows containers only**. Linux images will not run there.

---

## Configuration

Typical environment variables:

```bash
# API (kqapi)
ASPNETCORE_ENVIRONMENT=Production
KQ_CONNECTION_STRING="Server=localhost;Database=KQ;User ID=kqapp;Password=***;TrustServerCertificate=True;"

# Next.js (kq-reg-web-next)
NEXT_PUBLIC_API_BASE_URL=https://xxx.example.com
```
<h3>📸 Screenshots:</h3>

<img src="https://raw.githubusercontent.com/RamiIssa1974/KQMunicipality/master/ScreenShots/LoginPage.jpg" width="620" height="320" alt="Login Page">
<img src="https://raw.githubusercontent.com/RamiIssa1974/KQMunicipality/master/ScreenShots/Registe1.jpg" width="620" height="320" alt="Register Page">
<img src="https://raw.githubusercontent.com/RamiIssa1974/KQMunicipality/master/ScreenShots/RejectPage.jpg" width="620" height="320" alt="Reject Page">
<img src="https://raw.githubusercontent.com/RamiIssa1974/KQMunicipality/master/ScreenShots/FinishPage.jpg" width="620" height="320" alt="Finish Page">

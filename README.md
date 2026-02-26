# KoiShow

KoiShow is a management and scoring system for Koi fish contests, designed to help organizers, judges, and participants manage contests, register Koi fish, submit entries, and track results. The project provides both backend services and data models for contest operations, Koi registration, judging, and result calculation.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [API Overview](#api-overview)
- [Screenshots](#screenshots)

## Features

- Manage Koi contests with flexible rules and scoring.
- Register users and Koi fish for participation.
- Handle contest registration forms and payments.
- Judge/scoring system for evaluating contestants.
- Track and display contest results.

## Getting Started

This project is primarily a backend service developed in .NET, using a layered architecture with repositories, services, and an API.

### Prerequisites

- [.NET 6 SDK or later](https://dotnet.microsoft.com/en-us/download)
- SQL Server (or other compatible database)
- Visual Studio or VS Code (recommended)
- Git

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/namyus/koi-show-management-system-deploy
   cd FA24_SE1716_PRN231_G2_KoiShow
   ```

2. **Set up the database**
   - Configure your connection string in `appsettings.json` or via environment variables.
   - Run database migrations if available.

3. **Build and run the API**
   ```bash
   cd KoiShow.APIService
   dotnet build
   dotnet run
   ```

4. **Swagger documentation**
   - Once running, navigate to `http://localhost:<port>/swagger` to explore the API endpoints.

## Usage

- Use the provided REST API to register users, add Koi fish, create contests, and handle judging.

### API Overview

The API provides endpoints to manage:
- Accounts (users, admins, judges)
- Koi fish and their varieties
- Contest creation, registration, and scoring
- Registration forms and payments
- Contest results and points

## Screenshots

<<<<<<< Updated upstream
## Screenshots
=======
<div style="text-align: center;">
    <img src="https://ibb.co/vxMVh65N" style="width: 70%;"/>
    <p><em>Screenshot 1</em></p>
</div>
>>>>>>> Stashed changes

<div align="center">
  <img src="https://i.ibb.co/kgP5JsnL/Screenshot-2026-01-31-211054.png" width="70%" />
  <p><em>Screenshot 1</em></p>
</div>
<br/>

<<<<<<< Updated upstream
---
=======
<div style="text-align: center;">
    <img src="https://ibb.co/pBbm3K9N"  style="width: 70%;"/>
    <p><em>Screenshot 2</em></p>
</div>
>>>>>>> Stashed changes

## Docker Setup

<<<<<<< Updated upstream
KoiShow supports running the API and SQL Server using Docker.

### Prerequisites
=======
<hr/>
<br/>
>>>>>>> Stashed changes

- Docker Desktop
- Docker Compose

### Run with Docker

1. Make sure **Docker Desktop is running**.

2. From the root directory of the project, run:

   ```bash
   docker compose up --build
   ```
3. After startup:

- API Base URL: http://localhost:8080

- Swagger UI: http://localhost:8080/swagger

- SQL Server: localhost:1434

## Stop Containers:

   ```bash
   docker compose down
   ```

## Reset Database (Remove Volume):

   ```bash
   docker compose down -v
   ```
### Notes

1. The API uses environment variables for the connection string inside Docker.

2. Inside containers, use sqlserver as the database host (not localhost).

3. Entity Framework migrations run automatically at startup (if configured in Program.cs).


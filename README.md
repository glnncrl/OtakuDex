# ⬡ OtakuDex

> A full stack anime collection tracker built with ASP.NET Core MVC, Entity Framework Core, and SQL Server.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC%20.NET%2010-7B61FF?style=flat-square)
![SQL Server](https://img.shields.io/badge/SQL%20Server-OtakuDexDB-0FFFC1?style=flat-square)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-A08BFF?style=flat-square)
![Chart.js](https://img.shields.io/badge/Chart.js-Analytics-FF2D78?style=flat-square)

---

## About

OtakuDex is a full stack web application for managing and tracking personal anime collections. It features CRUD operations, community reviews, custom collections, genre tagging, a live analytics dashboard with CSV export, an AI-powered recommendation engine, and a fully responsive interface with dark/light theming.

---

## Features

| Feature | Description |
|---|---|
| **Anime Tracker** | Full CRUD with 5 watch statuses, 1–10 ratings, and 9 data fields per entry |
| **Reviews** | Write and manage community reviews per anime with individual ratings |
| **Collections** | Build custom curated anime lists with cover poster grid display |
| **Genre Tags** | Tag anime with genres and browse your entire tracker by genre |
| **Analytics Dashboard** | Live Chart.js charts — watch status, rating distribution, timeline, top genres — with one-click CSV export of all data |
| **For You** | Weighted recommendation engine powered by your ratings, genres, and studios |
| **Dark / Light Theme** | Toggle between dark and light mode from desktop or mobile nav, with your preference saved across visits |
| **Responsive Design** | Collapsible mobile navigation menu for a smooth experience on any screen size |

---

## Tech Stack

| Category | Technology |
|---|---|
| Language | C# (.NET 10) |
| Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core (Code-First) |
| Database | Microsoft SQL Server |
| Frontend | Razor Views, Bootstrap 5, Custom CSS |
| Charts | Chart.js (CDN) |
| Data Export | Client-side CSV generation (JavaScript Blob API) |
| Theming | CSS custom properties + localStorage persistence |
| Fonts | Space Grotesk, Inter (Google Fonts) |
| Version Control | Git & GitHub |
| IDE | Visual Studio 2022 |

---

## Database Schema

| Table | Description |
|---|---|
| `Animes` | Main anime collection — all user-tracked entries |
| `Reviews` | User reviews linked to each anime |
| `Collections` | Named custom anime lists |
| `CollectionItems` | Junction table linking anime to collections |
| `Genres` | Genre tag definitions |
| `AnimeGenres` | Junction table linking anime to genres (many-to-many) |
| `AnimeDatabase` | Seeded catalog of 30 anime used by the recommendation engine |

---

## Getting Started

### Prerequisites

- Visual Studio 2022
- .NET 10 SDK
- Microsoft SQL Server (any edition)
- SQL Server Management Studio (SSMS)

### Setup

1. **Clone the repository**

```bash
git clone https://github.com/glnncrl/OtakuDex.git
cd OtakuDex
```

2. **Configure the connection string**

Open `appsettings.json` and update the connection string to match your SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=OtakuDexDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

3. **Apply migrations**

Open **Package Manager Console** in Visual Studio and run:

```powershell
Update-Database
```

This will create the `OtakuDexDB` database with all tables and seed the `AnimeDatabase` table with 30 anime entries for the recommendation engine.

4. **Run the application**

Press **F5** in Visual Studio or run:

```bash
dotnet run
```

The app will be available at `https://localhost:7140`.

---

## Pages

| Route | Description |
|---|---|
| `/` | Home page with hero, featured anime, features, and quotes |
| `/Anime` | Tracker — full anime list with search, filter, sort |
| `/Anime/Create` | Add a new anime to the tracker |
| `/Anime/Details/{id}` | Anime details with genre tags and reviews |
| `/Collection` | My Lists — curated anime collections |
| `/Genre` | Genre Tags — browse tracker by genre |
| `/Dashboard` | Analytics Dashboard with live Chart.js charts and CSV export |
| `/Recommendation` | For You — personalized anime recommendations |
| `/Home/Privacy` | About page with team info and project details |

---

## Team

| Name | GitHub |
|---|---|
| Dayawon, Lance Joseph M. | [@lancejosephmdayawon](https://github.com/lancejosephmdayawon) |
| Felonia, Hannah Sophia N. | [@sophiahannah](https://github.com/sophiahannah) |
| Pabayo, Glenn Carlo U. | [@glnncrl](https://github.com/glnncrl) |
| Tanawan, Joshua C. | [@Toshihua](https://github.com/Toshihua) |

---

> *"The only thing we're allowed to do is believe that we won't regret the choice we made."*
> — Levi Ackerman

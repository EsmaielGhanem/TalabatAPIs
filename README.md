# 🛒 Talabat Clone - E-Commerce Backend System

A scalable and production-ready e-commerce backend system inspired by Talabat, built using modern .NET technologies and best practices.

---

## 🚀 Features

* 🔐 Authentication & Authorization using JWT
* 💳 Secure Payment Integration (Stripe)
* 🧠 Clean Architecture (Onion Architecture)
* 🔄 Repository & Unit of Work Patterns
* ⚡ Redis Caching for performance optimization
* 🔍 Dynamic Querying using Specification Pattern
* 🔁 AutoMapper for DTO ↔ Entity mapping
* 📦 Order Management System
* 🛍️ Shopping Cart with caching support
* 🌱 Data Seeding for initial setup

---

## 🏗️ Architecture

This project follows **Onion Architecture**, ensuring:

* Separation of concerns
* High maintainability
* Testability
* Scalability

### Layers:

* **Core** → Entities, Interfaces, Business Logic
* **Application** → Services, DTOs, Specifications
* **Infrastructure** → Database, Repositories, External Services
* **API** → Controllers, Endpoints

---

## 🛠️ Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Redis
* JWT Authentication
* Stripe Payment Gateway
* AutoMapper

---

## 📌 Design Patterns

* Repository Pattern
* Unit of Work Pattern
* Specification Pattern

---

## ⚙️ Getting Started

### Prerequisites

* .NET SDK
* SQL Server
* Redis

### Installation

```bash
git clone https://github.com/your-username/your-repo.git
cd your-repo
```

### Update Configuration

Edit `appsettings.json`:

* Database Connection String
* JWT Settings
* Stripe Keys
* Redis Configuration

### Run the Project

```bash
dotnet restore
dotnet run
```

---

## 📡 API Features

* User Registration & Login
* Product Listing & Filtering
* Cart Management
* Order Creation
* Payment Processing

---

## 📈 Future Improvements

* Add Frontend (React / Angular)
* Dockerize the application
* CI/CD Pipeline
* Admin Dashboard
* Real-time order tracking

---

## 👨‍💻 Author

Developed by [Esmaiel Ghanem]

---

## ⭐ Notes

This project demonstrates strong backend engineering skills including system design, clean architecture, and real-world integrations.

Feel free to fork, contribute, or use it as a reference!

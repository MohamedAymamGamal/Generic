# E-Commerce Platform (Ecom)

A full-stack **E-Commerce Platform** built with **ASP.NET Core Web API** and **Angular**, following **Clean Architecture** principles. The application provides a complete online shopping experience with secure authentication, product management, shopping cart, online payments, caching, and containerized deployment using Docker.

---

## 🚀 Features

### Authentication & Authorization
- JWT Authentication
- Role-based authorization (Admin & Customer)
- User registration and login
- Secure API endpoints

### Product Management
- Product catalog
- Categories & Brands
- Product search, filtering, sorting
- Pagination

### Shopping
- Shopping Cart
- Wishlist
- Order Management
- Secure Checkout

### Payments
- Stripe Payment Integration
- Payment Intent creation
- Secure payment processing
- Order confirmation

### Performance
- Redis distributed caching
- Optimized database queries
- Fast API response times

---

# 🏗️ Architecture

The project follows **Clean Architecture** with a layered architecture to keep business logic independent from infrastructure.

```
src
├── API
├── Application
├── Domain
└── Infrastructure
```

---

# 📐 Design Patterns

The application uses several enterprise design patterns:

- ✅ Clean Architecture
- ✅ Repository Pattern
- ✅ **Unit of Work Pattern**
- ✅ Service Layer Pattern
- ✅ Specification Pattern
- ✅ Dependency Injection
- ✅ CQRS (where applicable)
- ✅ Global Exception Handling

### Unit of Work

The **Unit of Work Pattern** coordinates multiple repositories under a single database transaction. Instead of each repository saving changes independently, all operations are committed together through a single `SaveChangesAsync()` call.

**Benefits**
- Maintains data consistency
- Supports transactional operations
- Reduces database calls
- Simplifies repository management
- Makes business logic easier to maintain and test

Example flow:

```
Create Order
     │
     ▼
Update Product Stock
     │
     ▼
Create Payment
     │
     ▼
Commit Transaction (Unit of Work)
```

---

# 🛠️ Tech Stack

## Backend
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Redis
- ASP.NET Core Identity
- JWT Authentication
- Stripe API

## Frontend
- Angular
- TypeScript
- RxJS
- Bootstrap

## DevOps
- Docker
- Docker Compose

---

# 🐳 Docker

The application is fully containerized using **Docker Compose**.

### Services

- ASP.NET Core API
- Angular Client
- SQL Server
- Redis

### Benefits

- One-command project setup
- Consistent development environment
- Easy onboarding
- Isolated services
- Production-like local environment

Run the application:

```bash
docker compose up --build
```

Stop containers:

```bash
docker compose down
```

---

# 📦 Modules

- Authentication
- Products
- Categories
- Brands
- Shopping Cart
- Orders
- Payments
- Admin Dashboard

---

# ⚡ API Features

- RESTful API
- CRUD Operations
- Pagination
- Filtering
- Sorting
- Search
- Image Upload
- Global Exception Handling

---

# 💳 Payment Workflow

1. Customer adds items to the cart.
2. Checkout creates a Stripe Payment Intent.
3. Payment is completed securely.
4. Order is created.
5. Product inventory is updated.
6. Changes are committed through the **Unit of Work** transaction.

---

# 🚀 Getting Started

Clone the repository:

```bash
git clone https://github.com/yourusername/ecommerce-platform.git
```

Start all services:

```bash
docker compose up --build
```

---

# 🔧 Future Improvements

- Email notifications
- Product reviews & ratings
- Coupons & discounts
- Elasticsearch integration
- CI/CD pipeline
- Kubernetes deployment
- Cloud deployment (Azure/AWS)

---

## 📄 License

This project is intended for educational and portfolio purposes.

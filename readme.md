# 🚀 Store API

> A modern, lightweight, and robust ASP.NET Core Web API built with Entity Framework Core and SQLite. Designed for clean architecture, fast performance, and seamless developer experience.

## 📖 Table of Contents

* [About The Project](#-about-the-project)

* [Built With](#-built-with)

* [Getting Started](#-getting-started)

  * [Prerequisites](#prerequisites)

  * [Installation & Setup](#installation--setup)

* [API Endpoints Reference](#-api-endpoints-reference)

* [Database Management](#-database-management)

* [Contributing](#-contributing)

* [License](#-license)

## 🌟 About The Project

**Store API** is a RESTful backend service designed to manage product inventory efficiently. It utilizes the latest **.NET 10** features alongside **Entity Framework Core (Code First)** and a lightweight **SQLite** database, making it exceptionally fast, portable, and ideal for both microservices and standalone web applications.

### ✨ Key Features

* **Clean Controller Architecture:** Well-structured REST API endpoints following standard conventions.

* **EF Core ORM:** Smooth database interactions with automated migrations and SQLite support.

* **Input Validation & Exception Handling:** Robust error responses and safe model binding.

* **Hot Reloading:** Instant feedback loop during development using `dotnet watch`.

* **API Testing Ready:** Includes `.http` request templates compatible with VS Code REST Client and Scalar UI.

## 🛠 Built With

* [**ASP.NET Core 10**](https://dotnet.microsoft.com/) - High-performance modern web framework.

* [**Entity Framework Core**](https://learn.microsoft.com/en-us/ef/core/) - Modern object-database mapping for .NET.

* [**SQLite**](https://www.sqlite.org/index.html) **/ [PostgreSQL](https://www.postgresql.org/)** - Fast, reliable, self-contained SQL database engine.

* [**VS Code**](https://code.visualstudio.com/) - Powerful lightweight editor with C# Dev Kit.

## 🚀 Getting Started

Follow these steps to set up and run the project locally on your Linux (Parrot OS / Debian / Ubuntu) or Windows machine.

### Prerequisites

Make sure you have the following installed:

* [.NET SDK 10.0](https://dotnet.microsoft.com/download) (or higher)

* [Git](https://git-scm.com/)

* [VS Code](https://code.visualstudio.com/) with **C# Dev Kit** & **REST Client** extensions.

### Installation & Setup

1. **Clone the repository:**

   ```
   git clone https://github.com/your-username/StoreApi.git
   cd StoreApi
   
   ```

2. **Restore dependencies:**

   ```
   dotnet restore
   
   ```

3. **Run EF Core migrations to create the SQLite database:**

   ```
   dotnet ef database update
   
   ```

4. **Run the application with Hot Reload:**

   ```
   dotnet watch run
   
   ```

   *The API will start at `http://localhost:5123` or `https://localhost:7123`.*

## 📡 API Endpoints Reference

Base URL: `http://localhost:5123/api`

| Method | Endpoint | Description | Request Body / Params | 
| ----- | ----- | ----- | ----- | 
| **GET** | `/products` | Retrieve all products | None | 
| **GET** | `/products/{id}` | Retrieve a specific product by ID | Path Parameter (`id`) | 
| **POST** | `/products` | Create a new product | JSON Object | 
| **PUT** | `/products/{id}` | Update an existing product | Path ID & JSON Object | 
| **DELETE** | `/products/{id}` | Delete a product by ID | Path Parameter (`id`) | 

### Example JSON Payload (POST / PUT):

```
{
  "name": "Mechanical Keyboard",
  "category": "Accessories",
  "price": 4500.00,
  "stock": 15
}

```

## 🗄 Database Management

This project uses **SQLite**. The database file (`store.db`) is automatically generated in your root directory upon running the initial migration.

* To view/edit the database directly in VS Code, install the **SQLite Viewer** extension and click on `store.db`.

* To create a new migration after modifying models in `Models/`:

  ```
  dotnet ef migrations add MigrationName
  dotnet ef database update
  
  ```

## 🤝 Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project

2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)

3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)

4. Push to the Branch (`git push origin feature/AmazingFeature`)

5. Open a Pull Request

## 📝 License

Distributed under the **MIT License**. See `LICENSE` file for more information.
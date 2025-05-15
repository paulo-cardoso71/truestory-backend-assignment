# Truestory API Assignment

## 🧾 Overview

This is a RESTful Web API built with C# and .NET 8. It connects to the public mock API at [https://restful-api.dev](https://restful-api.dev/) and extends its functionality by adding:

- 🔍 Product listing with name filtering and pagination
- ➕ Adding new products (POST)
- ❌ Deleting products by ID (DELETE)
- ✅ Validation and error handling

---

## ⚙️ Technologies

- .NET 8 Web API
- C#
- HttpClient
- System.Text.Json
- Swagger (Swashbuckle)

---

## 🚀 How to Run

1. Make sure you have the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed.

2. Clone the repository:

```bash
git clone https://github.com/paulo-cardoso71/truestory-backend-assignment.git
cd TruestoryApi
dotnet run
```

3. Open your browser and go to:

http://localhost:<your-port>/swagger

## 📌 API Endpoints

1. GET /api/Products:
Retrieve products from the mock API with optional name filter and pagination.

Query Parameters:

name (optional): substring to filter by name

page (default: 1)

pageSize (default: 5)

Example: /api/Products?name=mac&page=1&pageSize=3

2. POST /api/Products
Request body:
{
  "name": "Notebook Gamer",
  "data": "RTX 3060, 16GB RAM, SSD"
}

3. DELETE /api/Products/{id}
Delete a product by ID.

## 🧪 Validation and Error Handling

name is required for POST requests

Returns status code 400 for invalid models

Handles API errors with descriptive messages (500, 404, etc.)


## 📁 Project Structure

TruestoryApi/
├── Controllers/
│   └── ProductsController.cs
├── Models/
│   └── Product.cs
├── Program.cs
└── README.md

## ✍️ Author
Paulo Eder Medeiros Cardoso





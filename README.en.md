# FakePOS API `ASP.NET Core 8`
FakePOS is a web API developed in ASP.NET, which simulates the operations of a point of sale. This project is designed to simplify and optimize inventory management, sales and purchasing, offering key functionalities that allow users to track product movements in detail.

[README.md en español](./README.md)

Swagger Docs [here.](https://fakeposapi.azurewebsites.net/swagger/index.html)

## Features
- **Sales Management**: Performs sales records with all the necessary details, including product and quantity management.
- **Purchasing Management**: Adds stock to products accurately, ensuring that inventory is always up to date.
- **Inventory Kardex**: Keeps a complete history of all stock movements, providing transparency and control.
- **Advanced Security**: Implements authentication with JWT to protect endpoints and ensure that only authorized users can access them.
- **Interactive Documentation**: Integration with Swagger for easy and efficient API visualization and testing.

## Technical Requirements
- **Framework**: .NET 8 or higher
- **DB**: SQL Server
- **IDE**: Visual Studio or Visual Studio Code

## Installation and configuration
to install the project in a local environment

- Clone the repository from GitHub by copying the following command:

  ```bash
  git clone https://github.com/jsduenas28/FakePOS-Api.git
  ```

- Configure the connection to the SQL Server database in the `appsettings.json` file. Add the server in `ConnectionString`:

  ```csharp
  "ConnectionStrings": {
    "StoreConnection": "Server=yourserver;Database=fakePOSDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
  ```

- Run the InitDB migration to create the database structure, this is done in the NuGet administration console using the following command:

  ```bash
  Update Database
  ```

- Run the project and access the Swagger Docs at `/swagger`.

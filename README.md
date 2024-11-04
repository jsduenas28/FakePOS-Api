# FakePOS API `ASP.NET Core 8`
FakePOS es una API web desarrollada en ASP.NET, que simula las operaciones de un punto de venta. Este proyecto está diseñado para simplificar y optimizar la gestión de inventario, ventas y compras, ofreciendo funcionalidades clave que permiten a los usuarios realizar un seguimiento detallado de los movimientos de productos.

[README.md in english](./README.en.md)

Swagger Docs [aquí.](https://fakeposapi.azurewebsites.net/swagger/index.html)

## Caracteristicas
- **Gestión de Ventas**: Realiza registros de ventas con todos los detalles necesarios, incluyendo el manejo de productos y cantidades.
- **Gestión de Compras**: Agrega stock a los productos de manera precisa, asegurando que el inventario esté siempre actualizado.
- **Kardex de Inventario**: Guarda un historial completo de todos los movimientos de stock, proporcionando transparencia y control.
- **Seguridad Avanzada**: Implementa autenticación con JWT para proteger los endpoints y asegurar que solo usuarios autorizados puedan acceder a ellos.
- **Documentación Interactiva**: Integración con Swagger para una visualización y prueba de la API de manera sencilla y eficiente.

## Requsitos tecnicos
- **Framework**: .NET 8 o superior
- **Base de datos**: SQL Server
- **Entorno de desarrollor**: Visual Studio o Visual Studio Code

## Instalación y configuración
para instalar el proyecto en un entorno local

- Clona el repositorio desde GitHub copiando el siguiente comando:
 
  ```bash
  git clone https://github.com/jsduenas28/FakePOS-Api.git
  ```
- Configura la conexión a la base de datos SQL Server en el archivo `appsettings.json`. Agrega el servidor en `ConnectionString`:
 
  ```csharp
  "ConnectionStrings": {
    "StoreConnection": "Server=tuservidor;Database=fakePOSDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
  ```

- Ejecuta la migración InitDB para crear la estructura de la base de datos, esto lo haces en la consola de administración de NuGet usando el siguiente comando:

  ```bash
  Update Database
  ```

- Ejecute el proyecto y acceda a la documentación de Swagger en `/swagger`

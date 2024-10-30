# FakePOS API `ASP.NET Core 8`
Fake POS (Point of Sale) API simula la funcionabilidad de un punto de venta real.

[README.md in english](./README.en.md)

Swagger Docs [aquí.](https://fakeposapi.azurewebsites.net/swagger/index.html)

Esta API incluye:

 - Operaciones CRUD en la mayoria de endpoints.
 - Login y Register de usuario.
 - Auth con JWT.
 - Manejo de Stock de productos.
 - Compra, Venta y anulación de ventas.
 - Validación de Stock del producto al realizar una Venta.
 - Registro de movimientos de inventario por producto en Kardex.

# JWT en Swagger
Muchos de los endpoints requiren de authorization usando un token de JWT, los endpoints `Auth/register` y `Auth/login` no estan protegidos.

Al hacer login se retornara el token para acceder al resto de endpoints, copia y pega el token en el input con el siguiente formato: `Bearer <token>`

# Movimientos de inventario

## Venta:
    POST: api/Venta
Este endpoint te permite realizar una nueva venta. La cantidad vendida de cada producto se restara de su Stock.

Si la cantidad a vender es mayor que el Stock (No hay suficiente producto en stock para realizar la venta), se retornara un status code 400

**Body:**
```json
{
  "factura": "string",
  "fecha": "yyyy-MM-DD",
  "metodoPago": "string",
  "detalleVenta": [
    {
      "idProducto": 0,
      "cantidad": 0
    }
  ]
}
```

## Anular venta:
    PUT: api/Venta/anular/{idVenta}
Este endpoint te permite anular una venta, esto cambia su estado `IsContable` de true a false.
Si una venta es anulada, el Stock del producto sera regresado.

## Compra:

    POST: api/Compra
Este endpoint te permite realizar una compra de producto, esto con el fin de agregar nuevo Stock a los productos.

**Body:**
```json
{
  "factura": "string",
  "fecha": "2024-10-29",
  "metodoPago": "string",
  "detalleCompra": [
    {
      "idProducto": 0,
      "cantidad": 0
    }
  ]
}
```

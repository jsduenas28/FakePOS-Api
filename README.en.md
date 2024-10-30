# FakePOS API `ASP.NET Core 8`
The Fake POS (Point of Sale) API simulates the functionality of a real point of sale.

Swagger Docs [here.](https://fakeposapi.azurewebsites.net/swagger/index.html)

This API includes:

 - CRUD operations on each endpoint.
 - User Login and Register.
 - Auth with JWT.
 - Product Stock Management.
 - Buy, Sell and Sales Cancellation Functions.
 - Product Stock Validation when making a Sale.
 - Registration of inventory movements by product in Kardex.

# JWT in Swagger
Many endpoints require authorization using a JWT token, the `Auth/register` and `Auth/login` endpoints are not protected.

When logging in, the token is returned to access the rest of the endpoints, copy and paste the token into the input with the following format: `Bearer <token>`

# Inventory movements

## Sale:
    POST: api/Venta
This endpoint allows you to make a new sale. The quantity sold of each product will be subtracted from its Stock.

If the quantity to be sold is greater than the Stock (There is not enough product in stock to make the sale), a status code 400 will be returned.

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

## Cancel sale:
    PUT: api/Venta/anular/{idVenta}
This endpoint allows you to void a sale, this changes the value of `IsContable` from true to false.
If a sale is canceled, the invoiced Stock will be returned to the product..

## Buy:

    POST: api/Compra
This terminal allows you to purchase a product, in order to add new Stock to the products..

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

using fakePOSApi.DTOs;
using fakePOSApi.Models;
using fakePOSApi.Repository;
using fakePOSApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// EntityFramework
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

// Repository
builder.Services.AddScoped<IAuthRepository<Usuario>, AuthRepository>();
builder.Services.AddKeyedScoped<IRepository<Categoria>, CategoriaRepository>("categoriaRepository");
builder.Services.AddScoped<IProductoRepository<Producto>, ProductoRepository>();
builder.Services.AddKeyedScoped<IFacturaRepository<VentaDto, Venta>, VentaRepository>("ventaRepository");
builder.Services.AddKeyedScoped<IDetalleRepository<DetalleVenta>, DetalleVentaRepository>("detalleVentaRepository");
builder.Services.AddKeyedScoped<IFacturaRepository<CompraDto, Compra>, CompraRepository>("compraRepository");
builder.Services.AddKeyedScoped<IDetalleRepository<DetalleCompra>, DetalleCompraRepository>("detalleCompraRepository");
builder.Services.AddScoped<IKardexRepository<Kardex>, KardexRepository>();
builder.Services.AddScoped<IUsuarioRepository<Usuario>, UsuarioRepository>();

// Service
builder.Services.AddScoped<IAuthService<UserRegisterDto, UserLoginDto, Usuario>, AuthService>();
builder.Services.AddKeyedScoped<IService<CategoriaDto, CategoriaInsertDto, CategoriaUpdateDto>, CategoriaService>("categoriaService");
builder.Services.AddKeyedScoped<IService<ProductoDto, ProductoInsertDto, ProductoUpdateDto>, ProductoService>("productoService");
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddKeyedScoped<IService<CompraDto, CompraInsertDto, CompraUpdateDto>, CompraService>("compraService");
builder.Services.AddScoped<IKardexService<KardexListDto>, KardexService>();
builder.Services.AddScoped<IUsuarioService<UsuarioDto, UsuarioUpdateDto, UsuarioChangePasswordDto>, UsuarioService>();


// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Vendedor", policy => policy.RequireRole("Vendedor"));
    options.AddPolicy("ActiveUser", policy => policy.RequireClaim("IsActive", "True"));
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Configuración para que Swagger acepte JWT
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "FakePOS - API",
        Version = "v1",
        Description = "Punto de Venta Falso (Fake Point Of Sale) desarrollado en ASP.NET Core 8.",
        Contact = new OpenApiContact
        {
            Name = "Josué David Sánchez Dueñas",
            Email = "josueduenas2805@gmail.com"
        }
    });

    // Configurar el esquema de seguridad para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduzca el token de la siguiente forma: Bearer {token}"
    });

    // Configurar que los endpoints protegidos requieran el token JWT
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FakePOS - API v1");
    c.RoutePrefix = "swagger";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FakePOS - API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

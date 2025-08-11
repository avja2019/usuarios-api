using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Data;
using UsuariosAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar DbContext con SQL Server
builder.Services.AddDbContext<UsuariosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Configuración de CORS dinámica
var angularDevUrl = "http://localhost:4200";
var angularProdUrl = builder.Configuration["AngularProdUrl"] ?? "https://miappangular.netlify.app";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins(angularDevUrl, angularProdUrl)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient(); // Para IHttpClientFactory
builder.Services.AddScoped<ReCaptchaService>();
builder.Services.AddScoped<UsuarioRepository>();

var app = builder.Build();

// 🔹 PROBAR CONEXIÓN A BASE DE DATOS AL INICIAR
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UsuariosDbContext>();
    try
    {
        db.Database.OpenConnection();
        Console.WriteLine("✅ Conexión a la base de datos OK");
        db.Database.CloseConnection();
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Error al conectar a la base de datos: " + ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();

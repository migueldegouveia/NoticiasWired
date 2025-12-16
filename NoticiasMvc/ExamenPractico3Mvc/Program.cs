using Microsoft.EntityFrameworkCore;
using ExamenPractico3Mvc.Data;
using ExamenPractico3Mvc.Repositories;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Obtiene la cadena de conexión desde appsettings.json.
/// Es importante no exponer esta cadena en el código fuente.
/// </summary>
string? cadena = builder.Configuration.GetConnectionString("hospitalazurexamarin");

/// <summary>
/// Registro del repositorio en el contenedor de dependencias.
/// Se crea una nueva instancia cada vez que se solicita.
/// </summary>
builder.Services.AddTransient<RepositoryNoticias>();

/// <summary>
/// Configuración del contexto de Entity Framework Core utilizando SQL Server.
/// </summary>
builder.Services.AddDbContext<NoticiasContext>(options =>
    options.UseSqlServer(cadena));

/// <summary>
/// Habilita el soporte para controladores y vistas (MVC).
/// </summary>
builder.Services.AddControllersWithViews();

var app = builder.Build();

/// <summary>
/// Configuración del pipeline HTTP.
/// Incluye manejo de errores, HTTPS, archivos estáticos y routing.
/// </summary>
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

/// <summary>
/// Ruta por defecto del proyecto.
/// Apunta al controlador Noticias y su acción Index.
/// </summary>
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Noticias}/{action=Index}/{id?}");

app.Run();

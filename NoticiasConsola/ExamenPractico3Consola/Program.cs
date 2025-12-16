using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExamenPractico3Consola.Repositories;
using ExamenPractico3Consola.Data;
using Microsoft.Extensions.Logging;

class Program
{
    static async Task Main(string[] args)
    {
        // Construir configuración leyendo appsettings.json
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        // Obtener cadena de conexión
        string cadena = config.GetConnectionString("NoticiasDb");

        var services = new ServiceCollection();

        // Logging
        services.AddLogging(config =>
        {
            config.AddConsole();
            config.AddDebug();
        });

        // DbContext
        services.AddDbContext<NoticiasContext>(options =>
            options.UseSqlServer(cadena));

        // Repositorio
        services.AddTransient<RepositoryNoticias>();

        var provider = services.BuildServiceProvider();

        var repo = provider.GetRequiredService<RepositoryNoticias>();

        await repo.PopulateNoticiasAsync();
    }
}
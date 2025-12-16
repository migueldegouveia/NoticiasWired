using ExamenPractico3Consola.Data;
using ExamenPractico3Consola.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace ExamenPractico3Consola.Repositories
{
    /// <summary>
    /// Repositorio encargado de obtener noticias desde un feed RSS externo
    /// y almacenarlas en la base de datos si no existen previamente.
    /// </summary>
    class RepositoryNoticias
    {
        private readonly NoticiasContext _context;
        private readonly ILogger<RepositoryNoticias> _logger;

        public RepositoryNoticias(
            NoticiasContext context,
            ILogger<RepositoryNoticias> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Descarga el RSS del link, transforma los elementos XML en objetos Noticia
        /// y guarda únicamente las noticias nuevas en la base de datos.
        /// </summary>
        public async Task PopulateNoticiasAsync()
        {
            try
            {
                // URL del feed RSS que se va a consumir
                var url = "https://www.techrepublic.com/rssfeeds/articles/";

                using var client = new HttpClient();

                _logger.LogInformation("Iniciando carga RSS");

                // Descargar el contenido XML del feed
                var xml = await client.GetStringAsync(url);

                // Parsear el XML en un documento navegable
                var doc = XDocument.Parse(xml);

                // Obtener el nombre de la fuente desde el título del canal RSS
                // Ejemplo: <channel><title>TechRepublic</title></channel>
                var fuente = doc.Root?
                    .Element("channel")?
                    .Element("title")?
                    .Value ?? "Desconocido";

                // Convertir cada <item> del RSS en un objeto Noticia
                var noticias = doc.Descendants("item")
                    .Select(x => new Noticia
                    {
                        Titulo = x.Element("title")?.Value ?? "Sin título",
                        Descripcion = x.Element("description")?.Value ?? "",
                        Link = x.Element("link")?.Value ?? "",

                        // Intentar convertir la fecha del RSS; si falla, usar la fecha actual
                        Fecha = DateTime.TryParse(
                            x.Element("pubDate")?.Value, out var f) ? f : DateTime.UtcNow,

                        Fuente = fuente
                    })
                    .OrderByDescending(n => n.Fecha)
                    .Take(20) // Limitar a las 20 noticias más recientes
                    .ToList();

                int insertadas = 0;

                // Insertar solo las noticias que no existan previamente
                foreach (var noticia in noticias)
                {
                    // Se considera duplicada si el enlace ya existe en la base de datos
                    bool existe = await _context.Noticias
                        .AnyAsync(n => n.Link == noticia.Link);

                    if (!existe)
                    {
                        _context.Noticias.Add(noticia);
                        insertadas++;
                    }
                }

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                _logger.LogInformation(
                    "Carga finalizada. Noticias nuevas insertadas: {Total}", insertadas);
            }
            catch (Exception ex)
            {
                // Registrar cualquier error ocurrido durante el proceso
                _logger.LogError(ex, "Error al actualizar noticias desde el RSS");
            }
        }
    }
}

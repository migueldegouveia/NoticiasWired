using ExamenPractico3Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenPractico3Mvc.Data
{
    /// <summary>
    /// Contexto de Entity Framework Core que representa la conexión con la base de datos
    /// y expone las entidades del dominio como DbSet.
    /// </summary>
    public class NoticiasContext : DbContext
    {
        /// <summary>
        /// Constructor que recibe las opciones de configuración del contexto,
        /// normalmente proporcionadas por el contenedor de dependencias.
        /// </summary>
        public NoticiasContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Tabla de noticias mapeada desde el modelo Noticia.
        /// </summary>
        public DbSet<Noticia> Noticias { get; set; }
    }
}

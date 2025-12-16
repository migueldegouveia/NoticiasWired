using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamenPractico3Mvc.Models
{
    /// <summary>
    /// Representa una noticia almacenada en la base de datos.
    /// Incluye información básica como título, descripción, fecha y fuente.
    /// </summary>
    [Table("noticias_MiguelDias")]
    public class Noticia
    {
        /// <summary>
        /// Identificador único de la noticia.
        /// </summary>
        [Key]
        [Column("IDNOTICIAS")]
        public int IdNoticia { get; set; }

        /// <summary>
        /// Título de la noticia.
        /// </summary>
        [Column("TITULO")]
        public string Titulo { get; set; }

        /// <summary>
        /// Enlace externo a la noticia original.
        /// Si es null o "Sin enlace", se considera una noticia creada manualmente.
        /// </summary>
        [Column("LINK")]
        public string? Link { get; set; } = "Sin enlace";

        /// <summary>
        /// Contenido o descripción de la noticia.
        /// Puede contener HTML si proviene de un RSS.
        /// </summary>
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        /// <summary>
        /// Fecha de publicación de la noticia.
        /// </summary>
        [Column("FECHA")]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Fuente o medio del cual proviene la noticia.
        /// </summary>
        [Column("FUENTE")]
        public string Fuente { get; set; }

        /// <summary>
        /// Indica si la noticia fue creada manualmente por el usuario.
        /// Se determina por la ausencia de un enlace válido.
        /// </summary>
        [NotMapped]
        public bool EsNoticiaPropia =>
            string.IsNullOrEmpty(Link) || Link == "Sin enlace";
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenPractico3Consola.Models
{
    [Table("noticias_MiguelDias")]
    class Noticia
    {
        [Key]
        [Column("IDNOTICIAS")]
        public int IdNoticia { get; set; }

        [Required]
        [Column("TITULO")]
        public string Titulo { get; set; }

        [Required]
        [Column("LINK")]
        public string Link { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("FECHA")]
        public DateTime Fecha { get; set; }

        [Column("FUENTE")]
        public string Fuente { get; set; }
    }
}

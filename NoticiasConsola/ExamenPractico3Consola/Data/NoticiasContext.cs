using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenPractico3Consola.Models;

namespace ExamenPractico3Consola.Data
{
    class NoticiasContext: DbContext
    {
        public NoticiasContext(DbContextOptions<NoticiasContext> options) : base(options)
        {
        }

        public DbSet<Noticia> Noticias { get; set; }
    }
}

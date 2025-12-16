using ExamenPractico3Mvc.Data;
using ExamenPractico3Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenPractico3Mvc.Repositories
{
    /// <summary>
    /// Repositorio encargado de gestionar el acceso a datos de las noticias.
    /// Contiene operaciones de consulta, filtrado, paginación y CRUD.
    /// </summary>
    public class RepositoryNoticias
    {
        private readonly NoticiasContext _context;

        public RepositoryNoticias(NoticiasContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de fuentes disponibles en la base de datos,
        /// sin duplicados y ordenadas alfabéticamente.
        /// </summary>
        public async Task<List<string>> GetFuentesAsync()
        {
            return await _context.Noticias
                .Select(n => n.Fuente)
                .Distinct()
                .OrderBy(f => f)
                .ToListAsync();
        }

        /// <summary>
        /// Devuelve todas las noticias ordenadas por fecha descendente.
        /// </summary>
        public async Task<List<Noticia>> GetNoticiasAsync() =>
            await _context.Noticias
                .OrderByDescending(n => n.Fecha)
                .ToListAsync();

        /// <summary>
        /// Cuenta cuántas noticias cumplen los filtros aplicados:
        /// - Filtrado por fuente
        /// - Filtrado por noticias creadas manualmente (sin enlace)
        /// </summary>
        public async Task<int> CountAsync(string? fuente, bool misNoticias)
        {
            var query = _context.Noticias.AsQueryable();

            // Filtrar por fuente si se ha seleccionado una
            if (!string.IsNullOrEmpty(fuente))
                query = query.Where(n => n.Fuente == fuente);

            // Filtrar solo noticias creadas manualmente
            if (misNoticias)
                query = query.Where(n => n.Link == null || n.Link == "Sin enlace");

            return await query.CountAsync();
        }

        /// <summary>
        /// Obtiene una página de noticias aplicando:
        /// - Paginación
        /// - Filtrado por fuente
        /// - Filtrado por noticias propias
        /// </summary>
        public async Task<List<Noticia>> GetPagedAsync(int page, int pageSize, string? fuente, bool misNoticias)
        {
            var query = _context.Noticias.AsQueryable();

            if (!string.IsNullOrEmpty(fuente))
                query = query.Where(n => n.Fuente == fuente);

            if (misNoticias)
                query = query.Where(n => n.Link == null || n.Link == "Sin enlace");

            return await query
                .OrderByDescending(n => n.Fecha)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene una noticia por su identificador.
        /// Devuelve null si no existe.
        /// </summary>
        public async Task<Noticia?> GetNoticiaAsync(int id) =>
            await _context.Noticias
                .FirstOrDefaultAsync(n => n.IdNoticia == id);

        /// <summary>
        /// Inserta una nueva noticia en la base de datos.
        /// </summary>
        public async Task AddNoticiaAsync(Noticia noticia)
        {
            _context.Noticias.Add(noticia);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza una noticia existente.
        /// </summary>
        public async Task UpdateNoticiaAsync(Noticia noticia)
        {
            _context.Noticias.Update(noticia);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina una noticia por su ID si existe en la base de datos.
        /// </summary>
        public async Task DeleteNoticiaAsync(int id)
        {
            var noticia = await _context.Noticias
                .FirstOrDefaultAsync(n => n.IdNoticia == id);

            if (noticia != null)
            {
                _context.Noticias.Remove(noticia);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ExamenPractico3Mvc.Models;
using ExamenPractico3Mvc.Repositories;

namespace ExamenPractico3Mvc.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar el flujo completo de noticias:
    /// listado, filtrado, creación, edición, visualización y eliminación.
    /// </summary>
    public class NoticiasController : Controller
    {
        private readonly RepositoryNoticias _repo;

        public NoticiasController(RepositoryNoticias repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Muestra el listado de noticias con paginación, filtrado por fuente
        /// y opción de mostrar únicamente las noticias creadas manualmente.
        /// </summary>
        public async Task<IActionResult> Index(int page = 1, string? fuente = null, bool misNoticias = false)
        {
            int pageSize = 5;

            // Datos auxiliares para filtros y estado de la vista
            ViewBag.Fuentes = await _repo.GetFuentesAsync();
            ViewBag.FuenteSeleccionada = fuente;
            ViewBag.MisNoticias = misNoticias;

            // Obtener total de registros y página actual
            var total = await _repo.CountAsync(fuente, misNoticias);
            var noticias = await _repo.GetPagedAsync(page, pageSize, fuente, misNoticias);

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);

            return View(noticias);
        }

        /// <summary>
        /// Muestra los detalles completos de una noticia específica.
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            var noticia = await _repo.GetNoticiaAsync(id);

            if (noticia == null)
                return NotFound();

            return View(noticia);
        }

        /// <summary>
        /// Devuelve la vista para crear una nueva noticia manualmente.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Procesa la creación de una nueva noticia.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                await _repo.AddNoticiaAsync(noticia);
                TempData["SuccessMessage"] = "Noticia creada correctamente";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Error al crear la noticia";
            return View(noticia);
        }

        /// <summary>
        /// Carga la noticia a editar y muestra el formulario de edición.
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            var noticia = await _repo.GetNoticiaAsync(id);

            if (noticia == null)
            {
                return NotFound();
            }

            return View(noticia);
        }

        /// <summary>
        /// Procesa la modificación de una noticia existente.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                await _repo.UpdateNoticiaAsync(noticia);
                TempData["SuccessMessage"] = "Noticia modificada correctamente";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Error al editar la noticia";
            return View(noticia);
        }

        /// <summary>
        /// Elimina una noticia de la base de datos tras confirmación del usuario.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var noticia = await _repo.GetNoticiaAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }

            await _repo.DeleteNoticiaAsync(id);
            TempData["SuccessMessage"] = "Noticia eliminada correctamente";

            return RedirectToAction("Index");
        }
    }
}

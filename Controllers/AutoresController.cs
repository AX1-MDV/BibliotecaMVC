using BibliotecaMVC.Data;
using Microsoft.EntityFrameworkCore;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers
{
    public class AutoresController : Controller
    {
        private readonly BibliotecaContext _context;
        public AutoresController(BibliotecaContext context)
        {
            _context = context;
        }
        public  async Task<IActionResult> Index()
        {
            var autores = await _context.Autores.ToListAsync();
            return View(autores);
        }
        public async Task<IActionResult> Detalles(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null) return NotFound();
            return View(autor);
        }
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return View(autor);
            }

            try
            {
                _context.Autores.Add(autor);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Autor guardado correctamente en la base de datos.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // loggear el error
                ModelState.AddModelError(string.Empty, "Error al guardar el autor. Intente de nuevo.");
                return View(autor);
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null) return NotFound();
            return View(autor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Autor autor)
        {
            if (id != autor.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Autores.Update(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var autor = await _context.Autores.FindAsync(id.Value);
            if (autor == null) return NotFound();

            return View(autor);
        }

        // AutoresDelete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null) return NotFound();

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
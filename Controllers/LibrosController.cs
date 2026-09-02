using BibliotecaMVC.Interfaces;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaMVC.Controllers
{
    public class LibrosController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILibrosService _librosService;

        public LibrosController(IWebHostEnvironment env, ILibrosService librosService)
        {
            _env = env;
            _librosService = librosService;
        }

        public IActionResult Index()
        {
            return View(_librosService.GetAll());
        }

        public IActionResult Detalles(int id)
        {
            var libro = _librosService.GetById(id);
            if (libro == null) return NotFound();
            return View(libro);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Libro libro, IFormFile? imageFile)
        {
            if (!ModelState.IsValid) return View(libro);

            await _librosService.CreateAsync(libro, imageFile);
            return RedirectToAction("Index");
        }

        // GET: Libros/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var libro = _librosService.GetById(id.Value);
            if (libro == null) return NotFound();
            return View(libro);
        }

        // POST: Libros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Libro libro, IFormFile? imageFile)
        {
            if (id != libro.Id) return BadRequest();
            if (!ModelState.IsValid) return View(libro);

            var updated = await _librosService.UpdateAsync(libro, imageFile);
            if (!updated) return NotFound();

            return RedirectToAction("Index");
        }

        // GET: Libros/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var libro = _librosService.GetById(id.Value);
            if (libro == null) return NotFound();
            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleted = _librosService.Delete(id);
            if (!deleted) return NotFound();
            return RedirectToAction("Index");
        }
    }
}
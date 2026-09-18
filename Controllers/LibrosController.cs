using BibliotecaMVC.Data;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaMVC.Controllers
{
    public class LibrosController : Controller
    {
        private readonly BibliotecaContext _context;
        private readonly IWebHostEnvironment _env;

        public LibrosController(BibliotecaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var libros = await _context.Libros.ToListAsync();
            return View(libros);
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
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

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                await using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);
                libro.ImagePath = fileName;
            }

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Libro creado correctamente en la base de datos.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var libro = await _context.Libros.FindAsync(id.Value);
            if (libro == null) return NotFound();
            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Libro libro, IFormFile? imageFile)
        {
            if (id != libro.Id) return BadRequest();
            if (!ModelState.IsValid) return View(libro);

            var existente = await _context.Libros.FindAsync(id);
            if (existente == null) return NotFound();

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(existente.ImagePath))
                {
                    var oldPath = Path.Combine(uploads, existente.ImagePath);
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                existente.ImagePath = fileName;
            }

            existente.Titulo = libro.Titulo;
            existente.Autor = libro.Autor;
            existente.Categoria = libro.Categoria;
            existente.Precio = libro.Precio;
            existente.Disponible = libro.Disponible;
            existente.AnioPublicacion = libro.AnioPublicacion;

            _context.Libros.Update(existente);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Libro actualizado correctamente.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var libro = await _context.Libros.FindAsync(id.Value);
            if (libro == null) return NotFound();
            return View(libro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return NotFound();

            if (!string.IsNullOrEmpty(libro.ImagePath))
            {
                var uploads = Path.Combine(_env.WebRootPath, "images");
                var imgPath = Path.Combine(uploads, libro.ImagePath);
                if (System.IO.File.Exists(imgPath)) System.IO.File.Delete(imgPath);
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Libro eliminado correctamente.";
            return RedirectToAction("Index");
        }
    }
}
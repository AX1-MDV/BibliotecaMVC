using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BibliotecaMVC.Controllers
{
    public class LibrosController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private static List<Libro> _libros = new List<Libro>
        {
            new Libro { Id = 1, Titulo = "Clean Code", Autor = "Robert Martin", Categoria = "Programación", Precio = 19.99m, Disponible = true, AnioPublicacion = 2009, ImagePath = "clean-code.jpg" },
            new Libro { Id = 2, Titulo = "Don Quijote de la Mancha", Autor = "Miguel de Cervantes", Categoria = "Novela", Precio = 14.99m, Disponible = true, AnioPublicacion = 1605, ImagePath = "don-quijote.jpg" },
            new Libro { Id = 3, Titulo = "Cien años de soledad", Autor = "Gabriel García Márquez", Categoria = "Novela", Precio = 17.99m, Disponible = false, AnioPublicacion = 1967, ImagePath = "cien-anos-soledad.jpg" },
            new Libro { Id = 4, Titulo = "El Principito", Autor = "Antoine de Saint-Exupéry", Categoria = "Infantil", Precio = 9.99m, Disponible = true, AnioPublicacion = 1943, ImagePath = "el-principito.jpg" },
            new Libro { Id = 5, Titulo = "1984", Autor = "George Orwell", Categoria = "Ciencia Ficción", Precio = 12.99m, Disponible = true, AnioPublicacion = 1949, ImagePath = "1984.jpg" }
        };
        public LibrosController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_libros);
        }

        public IActionResult Detalles(int id)
        {
            var libro = _libros.FirstOrDefault(l => l.Id == id);
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

            libro.Id = _libros.Any() ? _libros.Max(l => l.Id) + 1 : 1;
            _libros.Add(libro);
            return RedirectToAction("Index");
        }

        // GET: Libros/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var libro = _libros.FirstOrDefault(l => l.Id == id.Value);
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

            var existente = _libros.FirstOrDefault(l => l.Id == id);
            if (existente == null) return NotFound();

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                await using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                // Opción: eliminar fichero anterior si existe
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

            return RedirectToAction("Index");
        }

        // GET: Libros/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var libro = _libros.FirstOrDefault(l => l.Id == id.Value);
            if (libro == null) return NotFound();
            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var libro = _libros.FirstOrDefault(l => l.Id == id);
            if (libro == null) return NotFound();
            _libros.Remove(libro);
            return RedirectToAction("Index");
        }
    }
}
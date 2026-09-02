using BibliotecaMVC.Interfaces;
using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BibliotecaMVC.Services
{
    public class LibrosService : ILibrosService
    {
        private readonly List<Libro> _libros;
        private readonly IWebHostEnvironment _env;

        public LibrosService(IRepositorioLibros repositorio, IWebHostEnvironment env)
        {
            _libros = repositorio.GetAll().ToList();
            _env = env;
        }

        public IEnumerable<Libro> GetAll() => _libros;

        public Libro? GetById(int id) => _libros.FirstOrDefault(l => l.Id == id);

        public async Task<Libro> CreateAsync(Libro libro, IFormFile? imageFile)
        {
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
            return libro;
        }

        public async Task<bool> UpdateAsync(Libro libro, IFormFile? imageFile)
        {
            var existente = _libros.FirstOrDefault(l => l.Id == libro.Id);
            if (existente == null) return false;

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                await using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                if (!string.IsNullOrEmpty(existente.ImagePath))
                {
                    var oldPath = Path.Combine(uploads, existente.ImagePath);
                    if (File.Exists(oldPath)) File.Delete(oldPath);
                }

                existente.ImagePath = fileName;
            }

            existente.Titulo = libro.Titulo;
            existente.Autor = libro.Autor;
            existente.Categoria = libro.Categoria;
            existente.Precio = libro.Precio;
            existente.Disponible = libro.Disponible;
            existente.AnioPublicacion = libro.AnioPublicacion;

            return true;
        }

        public bool Delete(int id)
        {
            var libro = GetById(id);
            if (libro == null) return false;

            if (!string.IsNullOrEmpty(libro.ImagePath))
            {
                var uploads = Path.Combine(_env.WebRootPath, "images");
                var imgPath = Path.Combine(uploads, libro.ImagePath);
                if (File.Exists(imgPath)) File.Delete(imgPath);
            }

            _libros.Remove(libro);
            return true;
        }
    }
}
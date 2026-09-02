using BibliotecaMVC.Models;
using Microsoft.AspNetCore.Http;

namespace BibliotecaMVC.Interfaces
{
    public interface ILibrosService
    {
        IEnumerable<Libro> GetAll();
        Libro? GetById(int id);
        Task<Libro> CreateAsync(Libro libro, IFormFile? imageFile);
        Task<bool> UpdateAsync(Libro libro, IFormFile? imageFile);
        bool Delete(int id);
    }
}
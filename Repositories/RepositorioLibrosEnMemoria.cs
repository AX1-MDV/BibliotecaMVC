using BibliotecaMVC.Interfaces;
using BibliotecaMVC.Models;
namespace BibliotecaMVC.Repositories
{
    public class RepositorioLibrosEnMemoria : IRepositorioLibros
    {
        public IEnumerable<Libro> GetAll()
        {
            return new List<Libro>
            {
                new Libro { Id = 1, Titulo = "Clean Code", Autor = "Robert Martin", Categoria = "Programación", Precio = 19.99m, Disponible = true, AnioPublicacion = 2009, ImagePath = "clean-code.jpg" },
                new Libro { Id = 2, Titulo = "Don Quijote de la Mancha", Autor = "Miguel de Cervantes", Categoria = "Novela", Precio = 14.99m, Disponible = true, AnioPublicacion = 1605, ImagePath = "don-quijote.jpg" },
                new Libro { Id = 3, Titulo = "Cien años de soledad", Autor = "Gabriel García Márquez", Categoria = "Novela", Precio = 17.99m, Disponible = false, AnioPublicacion = 1967, ImagePath = "cien-anos-soledad.jpg" },
                new Libro { Id = 4, Titulo = "El Principito", Autor = "Antoine de Saint-Exupéry", Categoria = "Infantil", Precio = 9.99m, Disponible = true, AnioPublicacion = 1943, ImagePath = "el-principito.jpg" },
                new Libro { Id = 5, Titulo = "1984", Autor = "George Orwell", Categoria = "Ciencia Ficción", Precio = 12.99m, Disponible = true, AnioPublicacion = 1949, ImagePath = "1984.jpg" }
            };
        }
    }
}
using BibliotecaMVC.Models;
namespace BibliotecaMVC.Interfaces
{
    public interface IRepositorioLibros
    {
        IEnumerable<Libro> GetAll();
    }
}

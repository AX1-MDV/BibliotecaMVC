using BibliotecaMVC.Models;
namespace BibliotecaMVC.Interfaces
{
    public interface IRepositorioAutores
    {
        IEnumerable<Autor> GetAll();
    }
}

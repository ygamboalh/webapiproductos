using ProductosSolution.Models;
namespace ProductosSolution.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<Usuario> Registrar(Usuario usuario, string password);
        Task<Usuario> Login(string correo, string password);
        Task<bool> ExisteUsuario(string correo);
    }
}

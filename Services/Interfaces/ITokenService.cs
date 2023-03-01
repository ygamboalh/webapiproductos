using ProductosSolution.Models;

namespace ProductosSolution.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Usuario usuario);
      
    }
}

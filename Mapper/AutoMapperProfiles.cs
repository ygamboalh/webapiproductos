using AutoMapper;
using ProductosSolution.Models;
using ProductosSolution.Dtos;
namespace ProductosSolution.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Para Post o create
            CreateMap<ProductoCreateDto, Producto>();

            //Para Put o update
            CreateMap<ProductoUpdateDto, Producto>();

            //Para Get o List
            CreateMap<Producto, ProductoToList>();

            //Usuarios
            CreateMap<UsuarioRegisterDto, Usuario>();
            CreateMap<UsuarioLoginDto, Usuario>();
            CreateMap<Usuario, UsuariosListDto>();
        }

    }
}

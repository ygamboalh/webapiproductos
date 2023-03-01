using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductosSolution.Data.Interfaces;
using ProductosSolution.Dtos;
using ProductosSolution.Models;
using ProductosSolution.Services.Interfaces;

namespace ProductosSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenService _token;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo, ITokenService token, IMapper mapper)
        {
            _repo = repo;
            _token = token;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UsuarioRegisterDto usuarioDto)
        {
            usuarioDto.Correo = usuarioDto.Correo.ToLower();
            if (await _repo.ExisteUsuario(usuarioDto.Correo))
                return BadRequest("Usuario con ese correo ya existe");
            var usuarioNuevo = _mapper.Map<Usuario>(usuarioDto);
            var usuarioCreado = await _repo.Registrar(usuarioNuevo, usuarioDto.Password);
            var usuarioCreadoDto = _mapper.Map<UsuariosListDto>(usuarioCreado);
            return Ok(usuarioCreadoDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var usuarioFromRepo = await _repo.Login(usuarioLoginDto.Correo, usuarioLoginDto.Password);
            if (usuarioFromRepo == null)
                return Unauthorized();
            var usuario = _mapper.Map<UsuariosListDto>(usuarioFromRepo);

            var token = _token.CreateToken(usuarioFromRepo);
            return Ok(new 
            {
                token = token,
                usuario = usuario
            });
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosSolution.Data.Interfaces;
using ProductosSolution.Models;
using System;
using Microsoft.EntityFrameworkCore;
using ProductosSolution.Dtos;
using AutoMapper;

namespace ProductosSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IApiRepository _repo;
        private readonly IMapper _mapper;

        public ProductosController(IApiRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productos = await _repo.GetProductosAsync();
            return Ok(productos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            var producto = await _repo.GetProductoByIdAsync(id);
            
            if (producto == null)
                return NotFound("Producto no encontrado");
            
            var productoDto = _mapper.Map<Producto>(producto);

            return Ok(productoDto);
        }
        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> Get(string nombre)
        {
            var producto = await _repo.GetProductoByNombreAsync(nombre);
            if (producto == null)
                return NotFound("Producto no encontrado");
            return Ok(producto);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductoCreateDto productoDto)
        { 
            var productoToCreate = _mapper.Map<Producto>(productoDto);

            _repo.Add(productoToCreate);

            if (await _repo.SaveAll())
                return Ok(productoToCreate);

            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put (int id, ProductoUpdateDto productoDto)
        {
            if (id != productoDto.Id)
                return BadRequest("Los ids no coinciden");

            var productoToUpdate = await _repo.GetProductoByIdAsync(productoDto.Id);

            if (productoToUpdate == null)
                return BadRequest();
            _mapper.Map(productoDto, productoToUpdate);

            if (!await _repo.SaveAll())
                return NoContent();
            return Ok(productoToUpdate);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            var producto = await _repo.GetProductoByIdAsync(id);
            if (producto == null)
                return NotFound("Producto no encontrado");
            _repo.Delete(producto);
            if (!await _repo.SaveAll())
                return BadRequest("No se pudo eliminar el producto");
            return Ok("Producto borrado");
            
        }
    }
}

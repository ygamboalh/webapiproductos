using ProductosSolution.Data.Interfaces;
using ProductosSolution.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace ProductosSolution.Data
{
    public class ApiRepository : IApiRepository
    {
        private readonly DataContext _context;

        public ApiRepository(DataContext context)
        {
            _context = context;
        }
        void IApiRepository.Add<T>(T entity)
        {
            _context.Add(entity);
        }

        void IApiRepository.Delete<T>(T entity)
        {
            _context.Remove(entity);
        }

        async Task<bool> IApiRepository.SaveAll()
        {
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<IEnumerable<Usuario>> IApiRepository.GetUsuariosAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios;
        }

        async Task<Usuario> IApiRepository.GetUsuarioByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync( u => u.Id == id);
            return usuario;
        }

        async Task<Usuario> IApiRepository.GetUsuarioByNombreAsync(string nombre)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync( u => u.Nombre == nombre );
            return usuario;
        }

        async Task<IEnumerable<Producto>> IApiRepository.GetProductosAsync()
        { 
            var productos = await _context.Productos.ToListAsync();
            return productos;
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(u => u.Id == id);
            return producto;   
        }

        async Task<Producto> IApiRepository.GetProductoByNombreAsync(string nombre)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync( u=>u.Nombre == nombre);
            return producto;
        }
    }
}

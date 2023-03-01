namespace ProductosSolution.Dtos
{
    public class UsuarioRegisterDto
    {
      
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }
        public string Password { get; set; }

        public UsuarioRegisterDto()
        {
            FechaDeAlta = DateTime.Now;
            Activo = true;
        }
    }
}

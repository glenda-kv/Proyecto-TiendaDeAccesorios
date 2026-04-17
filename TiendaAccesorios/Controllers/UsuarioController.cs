using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public UsuarioController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Usuario>>> GetUsuarios()
        {
            var usuarios = await _contexto.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(Guid id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario([FromBody] Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(Guid id, [FromBody] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
                return BadRequest();

            var existing = await _contexto.Usuarios.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.NombreCompleto = usuario.NombreCompleto;
            existing.Correo = usuario.Correo;
            existing.Contrasenia = usuario.Contrasenia;
            existing.Telefono = usuario.Telefono;
            existing.Rol = usuario.Rol;
            existing.Estado = usuario.Estado;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _contexto.Usuarios.Remove(usuario);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}

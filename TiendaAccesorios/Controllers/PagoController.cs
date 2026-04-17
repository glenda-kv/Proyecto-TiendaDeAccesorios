using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public PagoController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Pago>>> GetPagos()
        {
            var pagos = await _contexto.Pagos.ToListAsync();
            return Ok(pagos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(Guid id)
        {
            var pago = await _contexto.Pagos.FindAsync(id);

            if (pago == null)
                return NotFound();

            return Ok(pago);
        }

        [HttpPost]
        public async Task<ActionResult<Pago>> CreatePago([FromBody] Pago pago)
        {
            _contexto.Pagos.Add(pago);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPago), new { id = pago.IdPago }, pago);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePago(Guid id, [FromBody] Pago pago)
        {
            if (id != pago.IdPago)
                return BadRequest();

            var existing = await _contexto.Pagos.FindAsync(id);
            if (existing == null)
                return NotFound();

            
            existing.MetodoPago = pago.MetodoPago;
            existing.MontoPagado = pago.MontoPagado;
            existing.SaldoPendiente = pago.SaldoPendiente;
            existing.Interes = pago.Interes;
            existing.FechaPago = pago.FechaPago;
            existing.FechaVencimiento = pago.FechaVencimiento;
            existing.EstadoPago = pago.EstadoPago;

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(Guid id)
        {
            var pago = await _contexto.Pagos.FindAsync(id);
            if (pago == null)
                return NotFound();

            _contexto.Pagos.Remove(pago);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}



using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaAccesorios.Data;
using TiendaAccesorios.DTO.Producto;
using TiendaAccesorios.DTO.Producto.ActualizarPrecioProducto;
using TiendaAccesorios.DTO.Producto.ActualizarProducto;
using TiendaAccesorios.DTO.Producto.AdministrarProductos;
using TiendaAccesorios.DTO.Producto.AgregarProducto;
using TiendaAccesorios.DTO.Producto.BuscarProductos;
using TiendaAccesorios.DTO.Producto.CambiarEstadoProducto;
using TiendaAccesorios.DTO.Producto.IngresoStockProducto;
using TiendaAccesorios.DTO.Producto.ListarProductos;
using TiendaAccesorios.DTO.Producto.ObtenerProducto;
using TiendaAccesorios.DTO.Producto.StockBajoProductos;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Controllers
{
    public class ProductoController : BaseApiController
    {
        private readonly AppDbContext _contexto;
        private readonly IMapper _mapper;

        public ProductoController(AppDbContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ListarProductosOutput>>> GetProductos()
        {
            var productos = await _contexto.Productos
                .Where(p => p.EstaActivo)
                .OrderBy(p => p.NombreProducto)
                .ProjectTo<ListarProductosOutput>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(productos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ObtenerProductoOutput>> GetProducto(Guid id)
        {
            var producto = await _contexto.Productos
                .Where(p => p.IdProducto == id)
                .ProjectTo<ObtenerProductoOutput>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            return Ok(producto);
        }
        
        [HttpPost]
        public async Task<ActionResult<AgregarProductoOutput>> CreateProducto([FromBody]AgregarProductoInput entrada)
        {
            var categoria = await _contexto.Categorias
                .FirstOrDefaultAsync(c => c.NombreCategoria.ToLower() == entrada.Categoria.ToLower());

            if (categoria is null)
                return NotFound(new { mensaje = "La categoría ingresada no existe." });

            var producto = _mapper.Map<Producto>(entrada);
            producto.IdCategoria = categoria.IdCategoria;
            producto.Categoria = categoria;

            _contexto.Productos.Add(producto);
            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<AgregarProductoOutput>(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.IdProducto }, salida);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ActualizarProductoOutput>> UpdateProducto(Guid id, [FromBody] ActualizarProductoInput entrada)
        {
            var producto = await _contexto.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            var categoria = await _contexto.Categorias
                .FirstOrDefaultAsync(c => c.NombreCategoria.ToLower() == entrada.Categoria.ToLower());

            if (categoria is null)
                return NotFound(new { mensaje = "La categoría ingresada no existe." });

            _mapper.Map(entrada, producto);
            producto.IdCategoria = categoria.IdCategoria;
            producto.Categoria = categoria;

            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<ActualizarProductoOutput>(producto);
            return Ok(salida);
        }

        [HttpPatch("{id:guid}/precio")]
        public async Task<ActionResult<ActualizarPrecioProductoOutput>> ActualizarPrecio(Guid id, [FromBody] ActualizarPrecioProductoInput entrada)
        {
            var producto = await _contexto.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            producto.Precio = entrada.Precio;
            producto.FechaActualizacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<ActualizarPrecioProductoOutput>(producto);
            return Ok(salida);
        }

        [HttpPatch("{id:guid}/estado")]
        public async Task<ActionResult<CambiarEstadoProductoOutput>> CambiarEstado(Guid id, [FromBody] CambiarEstadoProductoInput entrada)
        {
            var producto = await _contexto.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            producto.EstaActivo = entrada.EstaActivo;
            producto.FechaActualizacion = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<CambiarEstadoProductoOutput>(producto);
            return Ok(salida);
        }

        [HttpPatch("{id:guid}/stock")]
        public async Task<ActionResult<IngresoStockProductoOutput>> AgregarStock(Guid id, [FromBody] IngresoStockProductoInput entrada)
        {
            var producto = await _contexto.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto is null)
                return NotFound(new { mensaje = "Producto no encontrado." });

            producto.Stock += entrada.Unidades;
            producto.FechaUltimoIngresoStock = DateTime.UtcNow;

            await _contexto.SaveChangesAsync();

            var salida = _mapper.Map<IngresoStockProductoOutput>(producto);
            return Ok(salida);
        }

        [HttpGet("buscar")]
        [ActionName("BuscarProductos")]
        public async Task<ActionResult<ICollection<BuscarProductosOutput>>> BuscarProductos([FromQuery] string buscarProducto)
        {
            var productos = await _contexto.Productos
                .Where(p => p.EstaActivo &&
                    (p.NombreProducto.Contains(buscarProducto) ||
                        p.Marca.Contains(buscarProducto)))
                .OrderBy(p => p.NombreProducto)
                .ProjectTo<BuscarProductosOutput>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (!productos.Any())
                return NotFound(new { mensaje = "No se encontraron productos con esa búsqueda." });

            return Ok(productos);
        }

        [HttpGet("categoria")]
        [ActionName("ListarProductosPorCategoria")]
        public async Task<ActionResult<ICollection<BuscarProductosOutput>>> ListarProductosPorCategoria([FromQuery] string nombre)
        {
            var categoriaExiste = await _contexto.Categorias
                .AnyAsync(c => c.NombreCategoria.ToLower() == nombre.ToLower());

            if (!categoriaExiste)
                return NotFound(new { mensaje = "La categoría ingresada no existe." });

            var productos = await _contexto.Productos
                .Where(p => p.EstaActivo &&
                    p.Categoria!.NombreCategoria.ToLower() == nombre.ToLower())
                .OrderBy(p => p.NombreProducto)
                .ProjectTo<BuscarProductosOutput>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (!productos.Any())
                return NotFound(new { mensaje = "No hay productos en esta categoría." });

            return Ok(productos);
        }

        [HttpGet("stock-bajo")]
        [ActionName("ListarProductosStockBajo")]
        public async Task<ActionResult<ICollection<StockBajoProductosOutput>>> ListarProductosStockBajo([FromQuery] int bajo = 5)
        {
            var productos = await _contexto.Productos
                .Where(p => p.EstaActivo && p.Stock <= bajo)
                .OrderBy(p => p.Stock)
                .ProjectTo<StockBajoProductosOutput>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (!productos.Any())
                return NotFound(new { mensaje = "No hay productos con stock bajo." });

            return Ok(productos);
        }

        [HttpGet("administrar")]
        [ActionName("AdministrarProductos")]
        public async Task<ActionResult<ICollection<AdministrarProductosOutput>>> AdministrarProductos()
        {
            var productos = await _contexto.Productos
                .OrderBy(p => p.NombreProducto)
                .ProjectTo<AdministrarProductosOutput>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (!productos.Any())
                return NotFound(new { mensaje = "No hay productos registrados." });

            return Ok(productos);
        }

        


        

    }
}

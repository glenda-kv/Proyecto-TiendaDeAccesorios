using System;
using AutoMapper;
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
using TiendaAccesorios.DTO.Venta.ActualizarObservacionVenta;
using TiendaAccesorios.DTO.Venta.CambiarEstadoVenta;
using TiendaAccesorios.DTO.Venta.GenerarVenta;
using TiendaAccesorios.DTO.Venta.ListarVentas;
using TiendaAccesorios.DTO.Venta.ObtenerVenta;
using TiendaAccesorios.Entidades;

namespace TiendaAccesorios.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // PRODUCTO

        CreateMap<Producto, ListarProductosOutput>()
            .ForMember(dest => dest.Categoria,
                       opt => opt.MapFrom(src => src.Categoria!.NombreCategoria));

        CreateMap<Producto, ObtenerProductoOutput>()
            .ForMember(dest => dest.Categoria,
                       opt => opt.MapFrom(src => src.Categoria!.NombreCategoria));
        
        CreateMap<Producto, BuscarProductosOutput>()
            .ForMember(dest => dest.Categoria,
                        opt => opt.MapFrom(src => src.Categoria!.NombreCategoria));
        
        CreateMap<AgregarProductoInput, Producto>()
            .ForMember(dest => dest.IdProducto,
                       opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.EstaActivo,
                       opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.FechaRegistro,
                       opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.IdCategoria, opt => opt.Ignore())
            .ForMember(dest => dest.Categoria, opt => opt.Ignore());

        CreateMap<Producto, AgregarProductoOutput>()
            .ForMember(dest => dest.Categoria,
                       opt => opt.MapFrom(src => src.Categoria!.NombreCategoria));
        
        CreateMap<ActualizarProductoInput, Producto>()
            .ForMember(dest => dest.FechaActualizacion,
                       opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.IdProducto, opt => opt.Ignore())
            .ForMember(dest => dest.EstaActivo, opt => opt.Ignore())
            .ForMember(dest => dest.Stock, opt => opt.Ignore())
            .ForMember(dest => dest.Precio, opt => opt.Ignore())
            .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore())
            .ForMember(dest => dest.FechaUltimoIngresoStock, opt => opt.Ignore())
            .ForMember(dest => dest.IdCategoria, opt => opt.Ignore())
            .ForMember(dest => dest.Categoria, opt => opt.Ignore());

        CreateMap<Producto, ActualizarProductoOutput>()
            .ForMember(dest => dest.Categoria,
                       opt => opt.MapFrom(src => src.Categoria!.NombreCategoria));
        
        CreateMap<Producto, ActualizarPrecioProductoOutput>();

        CreateMap<Producto, CambiarEstadoProductoOutput>();

        CreateMap<Producto, IngresoStockProductoOutput>();

        CreateMap<Producto, StockBajoProductosOutput>()
            .ForMember(dest => dest.Categoria,
                    opt => opt.MapFrom(src => src.Categoria!.NombreCategoria));

        CreateMap<Producto, AdministrarProductosOutput>()
            .ForMember(dest => dest.Categoria,
                    opt => opt.MapFrom(src => src.Categoria!.NombreCategoria));

        
        // VENTA
        CreateMap<Venta, ListarVentasOutput>()
            .ForMember(dest => dest.Cliente,
                    opt => opt.MapFrom(src => src.Cliente!.NombreCompleto));

        CreateMap<DetalleVenta, DetalleVentaOutput>()
            .ForMember(dest => dest.NombreProducto,
                    opt => opt.MapFrom(src => src.Producto!.NombreProducto));

        CreateMap<Venta, ObtenerVentaOutput>()
            .ForMember(dest => dest.Cliente,
                    opt => opt.MapFrom(src => src.Cliente!.NombreCompleto))
            .ForMember(dest => dest.Telefono,
                    opt => opt.MapFrom(src => src.Cliente!.Telefono))
            .ForMember(dest => dest.Productos,
                    opt => opt.MapFrom(src => src.DetallesVenta));

        CreateMap<DetalleVenta, GenerarDetalleVentaOutput>()
            .ForMember(dest => dest.NombreProducto,
                       opt => opt.MapFrom(src => src.Producto!.NombreProducto))
            .ForMember(dest => dest.Marca,
                       opt => opt.MapFrom(src => src.Producto!.Marca))
            .ForMember(dest => dest.Color,
                       opt => opt.MapFrom(src => src.Producto!.Color));

        CreateMap<Venta, GenerarVentaOutput>()
            .ForMember(dest => dest.Cliente,
                       opt => opt.MapFrom(src => src.Cliente!.NombreCompleto))
            .ForMember(dest => dest.Productos,
                       opt => opt.MapFrom(src => src.DetallesVenta));
        
        CreateMap<Venta, CambiarEstadoVentaOutput>()
            .ForMember(dest => dest.Cliente,
                    opt => opt.MapFrom(src => src.Cliente!.NombreCompleto));

        CreateMap<Venta, ActualizarObservacionVentaOutput>()
            .ForMember(dest => dest.Cliente,
                    opt => opt.MapFrom(src => src.Cliente!.NombreCompleto));

    }
}

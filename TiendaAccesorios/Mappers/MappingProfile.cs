using System;
using AutoMapper;
using TiendaAccesorios.DTO.Producto;
using TiendaAccesorios.DTO.Producto.ActualizarPrecioProducto;
using TiendaAccesorios.DTO.Producto.ActualizarProducto;
using TiendaAccesorios.DTO.Producto.AgregarProducto;
using TiendaAccesorios.DTO.Producto.CambiarEstadoProducto;
using TiendaAccesorios.DTO.Producto.IngresoStockProducto;
using TiendaAccesorios.DTO.Producto.ListarProductos;
using TiendaAccesorios.DTO.Producto.ObtenerProducto;
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
        
        // PATCH - Precio
        CreateMap<Producto, ActualizarPrecioProductoOutput>();

        // PATCH - Estado
        CreateMap<Producto, CambiarEstadoProductoOutput>();

        // PATCH - Stock
        CreateMap<Producto, IngresoStockProductoOutput>();
    }
}

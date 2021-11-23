﻿using AutoMapper;
using Restaurante.Clientes.Domain.Produtos.Modelos;
using Restaurante.Clientes.Integracoes.Config;
using Restaurante.Integrations;

namespace Restaurante.Clientes.Infra.Comum.Mapper
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PhotoResponseDTO, Domain.Produtos.Modelos.Photo>()
                .ForMember(x => x.Path, opt => opt.MapFrom(s => s.PhotoPath));
            CreateMap<ProductCategoryResponseDTO, Domain.Produtos.Modelos.ProductCategory>();
            CreateMap<ProductResponseDTO, Produto>();
        }
    }
}
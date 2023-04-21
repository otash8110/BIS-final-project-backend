using AutoMapper;
using FinalProject.Application.Products.Queries.GetOneProduct;
using FinalProject.Application.Products.Queries.GetProducts;
using FinalProject.Application.Products.Queries.GetUnregisteredProducts;
using FinalProject.Application.Search.Queries.GetSearchProducts;
using FinalProject.Core.Entities;

namespace FinalProject.Application.Common.Mapping
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, UnregisteredProductsDTO>();
            CreateMap<Product, OneProductDTO>();
            CreateMap<Product, SearchProductDTO>();
        }
    }
}

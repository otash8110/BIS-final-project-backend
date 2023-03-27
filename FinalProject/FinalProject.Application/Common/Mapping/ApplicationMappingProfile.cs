using AutoMapper;
using FinalProject.Application.Products.Queries.GetProducts;
using FinalProject.Core.Entities;

namespace FinalProject.Application.Common.Mapping
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Product, ProductDTO>();
        }
    }
}

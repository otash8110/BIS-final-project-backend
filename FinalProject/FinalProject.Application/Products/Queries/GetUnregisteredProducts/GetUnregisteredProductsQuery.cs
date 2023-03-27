using AutoMapper;
using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Entities;
using MediatR;

namespace FinalProject.Application.Products.Queries.GetUnregisteredProducts
{
    public record GetUnregisteredProductsQuery(): IRequest<IList<UnregisteredProductsDTO>>;

    public class GetUnregisteredProductsQueryHandler : IRequestHandler<GetUnregisteredProductsQuery, IList<UnregisteredProductsDTO>>
    {
        private readonly IMapper mapper;
        private readonly IBaseRepository<Product> productRepository;

        public GetUnregisteredProductsQueryHandler(IMapper mapper, IBaseRepository<Product> productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<IList<UnregisteredProductsDTO>> Handle(GetUnregisteredProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await productRepository.GetAllAsync();
            var filteredElements = result.Where(p => p.IsApproved == false).ToList();

            return mapper.Map<IList<UnregisteredProductsDTO>>(filteredElements);
        }
    }
}

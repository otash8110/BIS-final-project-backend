using AutoMapper;
using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Entities;
using MediatR;

namespace FinalProject.Application.Products.Queries.GetProducts
{
    public record GetProductsQuery(string userId) : IRequest<List<ProductDTO>>;

    public class GetProductQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDTO>>
    {
        private readonly IBaseRepository<Product> productRepository;
        private readonly IMapper mapper;

        public GetProductQueryHandler(IBaseRepository<Product> productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<List<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await productRepository.GetByFilter(p => p.UserId == request.userId);

            return mapper.Map<List<ProductDTO>>(result);
        }
    }
}

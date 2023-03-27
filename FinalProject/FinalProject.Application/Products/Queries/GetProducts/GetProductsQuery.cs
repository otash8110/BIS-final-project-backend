using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Entities;
using MediatR;

namespace FinalProject.Application.Products.Queries.GetProducts
{
    public record GetProductsQuery(string userId) : IRequest<ProductDTO>;

    public class GetProductQueryHandler : IRequestHandler<GetProductsQuery, ProductDTO>
    {
        private readonly IBaseRepository<Product> productRepository;

        public GetProductQueryHandler(IBaseRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ProductDTO> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await productRepository.
        }
    }
}

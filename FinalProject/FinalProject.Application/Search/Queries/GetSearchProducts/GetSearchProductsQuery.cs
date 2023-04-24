using AutoMapper;
using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Entities;
using MediatR;

namespace FinalProject.Application.Search.Queries.GetSearchProducts
{
    public record GetSearchProductsQuery(string productName): IRequest<List<SearchProductDTO>>;

    public class GetSearchProductsQueryHandler : IRequestHandler<GetSearchProductsQuery, List<SearchProductDTO>>
    {
        private readonly IBaseRepository<Product> productRepository;
        private readonly IMapper mapper;

        public GetSearchProductsQueryHandler(IBaseRepository<Product> productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<List<SearchProductDTO>> Handle(GetSearchProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetByFilterAsync(product => product.Name.Contains(request.productName));

            return mapper.Map<List<SearchProductDTO>>(products);
        }
    }
}

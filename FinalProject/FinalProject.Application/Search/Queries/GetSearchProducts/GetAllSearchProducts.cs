using AutoMapper;
using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Entities;
using MediatR;

namespace FinalProject.Application.Search.Queries.GetSearchProducts
{
    public record GetAllSearchProducts: IRequest<List<SearchProductDTO>>;

    public class GetAllSearchProductsHandler : IRequestHandler<GetAllSearchProducts, List<SearchProductDTO>>
    {
        private readonly IBaseRepository<Product> productRepository;
        private readonly IMapper mapper;

        public GetAllSearchProductsHandler(IBaseRepository<Product> productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<List<SearchProductDTO>> Handle(GetAllSearchProducts request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetByFilterAsync(p => p.IsApproved);

            return mapper.Map<List<SearchProductDTO>>(products);
        }
    }
}

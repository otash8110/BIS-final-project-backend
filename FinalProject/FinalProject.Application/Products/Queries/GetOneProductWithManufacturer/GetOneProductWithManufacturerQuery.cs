using AutoMapper;
using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Entities;
using MediatR;

namespace FinalProject.Application.Products.Queries.GetOneProductWithManufacturer
{
    public record GetOneProductWithManufacturerQuery(int id) : IRequest<OneProductWithManufacturerDTO>;

    public class GetOneProductWithManufacturerQueryHandler : IRequestHandler<GetOneProductWithManufacturerQuery, OneProductWithManufacturerDTO>
    {
        private readonly IBaseRepository<Product> productRepository;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper mapper;

        public GetOneProductWithManufacturerQueryHandler(IBaseRepository<Product> productRepository,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.currentUserService = currentUserService;
            this.mapper = mapper;
        }

        public async Task<OneProductWithManufacturerDTO> Handle(GetOneProductWithManufacturerQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByFilterAsync(p => p.Id == request.id);

            return mapper.Map<OneProductWithManufacturerDTO>(product.FirstOrDefault());
        }
    }
}

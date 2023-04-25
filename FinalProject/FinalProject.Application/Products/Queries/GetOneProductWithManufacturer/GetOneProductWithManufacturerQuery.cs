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
        private readonly IBaseRepository<Offer> offerRepository;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper mapper;

        public GetOneProductWithManufacturerQueryHandler(IBaseRepository<Product> productRepository,
            IBaseRepository<Offer> offerRepository,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.offerRepository = offerRepository;
            this.currentUserService = currentUserService;
            this.mapper = mapper;
        }

        public async Task<OneProductWithManufacturerDTO> Handle(GetOneProductWithManufacturerQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByFilterAsync(p => p.Id == request.id);
            var mappedProduct = mapper.Map<OneProductWithManufacturerDTO>(product.FirstOrDefault());

            var offers = await offerRepository.GetByFilterAsync(o => o.ProductId == mappedProduct.Id);
            foreach (var offer in offers)
            {
                if (offer != null)
                {
                    if (offer.DistributorEmail == currentUserService.UserEmail)
                    {
                        mappedProduct.isOfferMade = true;
                        break;
                    }
                }
            }

            return mappedProduct;
        }
    }
}

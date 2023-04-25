using AutoMapper;
using FinalProject.Application.Common.Interfaces;
using FinalProject.Application.Offers.Queries.Results;
using FinalProject.Core.Entities;
using MediatR;

namespace FinalProject.Application.Offers.Queries.GetManufacturerOffers
{
    public record GetManufacturerOffersQuery : IRequest<List<OfferResultDTO>>;

    public class GetManufacturerOffersQueryHandler : IRequestHandler<GetManufacturerOffersQuery, List<OfferResultDTO>>
    {
        private readonly IBaseRepository<Offer> offerRepository;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper mapper;

        public GetManufacturerOffersQueryHandler(IBaseRepository<Offer> offerRepository,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            this.offerRepository = offerRepository;
            this.currentUserService = currentUserService;
            this.mapper = mapper;
        }

        public async Task<List<OfferResultDTO>> Handle(GetManufacturerOffersQuery request, CancellationToken cancellationToken)
        {
            var manufacturerOffers = await offerRepository.GetByFilterAsync(o => o.ManufacturerEmail == currentUserService.UserEmail);

            return mapper.Map<List<OfferResultDTO>>(manufacturerOffers);
        }
    }
}

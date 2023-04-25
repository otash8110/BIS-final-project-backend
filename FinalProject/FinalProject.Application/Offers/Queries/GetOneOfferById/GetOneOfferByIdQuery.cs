using AutoMapper;
using FinalProject.Application.Common.Interfaces;
using FinalProject.Application.Offers.Queries.Results;
using FinalProject.Core.Entities;
using FinalProject.Core.Enums;
using MediatR;

namespace FinalProject.Application.Offers.Queries.GetOneOfferById
{
    public record GetOneOfferByIdQuery(int id) : IRequest<OfferResultDTO>;

    public class GetOneOfferByIdQueryHandler : IRequestHandler<GetOneOfferByIdQuery, OfferResultDTO>
    {
        private readonly IBaseRepository<Offer> offerRepository;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;

        public GetOneOfferByIdQueryHandler(IBaseRepository<Offer> offerRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            this.offerRepository = offerRepository;
            this.mapper = mapper;
            this.currentUserService = currentUserService;
        }

        public async Task<OfferResultDTO> Handle(GetOneOfferByIdQuery request, CancellationToken cancellationToken)
        {
            var offer = (await offerRepository.GetByFilterAsync(o => o.Id == request.id)).FirstOrDefault();
            var currentUserRole = currentUserService.Role;

            switch (currentUserRole)
            {
                case Roles.Manufacturer:
                    {
                        if (currentUserService.UserEmail != offer.ManufacturerEmail)
                        {
                            throw new UnauthorizedAccessException("You're not allowed to view this source");
                        }
                        break;
                    }
                case Roles.Distributor:
                    {
                        if (currentUserService.UserId != offer.DistributorEmail)
                        {
                            throw new UnauthorizedAccessException("You're not allowed to view this source");
                        }
                        break;
                    }
            }

            return mapper.Map<OfferResultDTO>(offer);
        }
    }
}

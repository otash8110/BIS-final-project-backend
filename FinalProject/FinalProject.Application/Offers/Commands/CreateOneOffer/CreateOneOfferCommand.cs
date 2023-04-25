using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Entities;
using MediatR;

namespace FinalProject.Application.Offers.Commands.CreateOneOffer
{
    public class CreateOneOfferCommand : IRequest<int>
    {
        public string Message { get; set; }
        public string ManufacturerEmail { get; set; }
        public int ProductId { get; set; }
    }

    public class CreateOneOfferCommandHandler : IRequestHandler<CreateOneOfferCommand, int>
    {
        private readonly IBaseRepository<Offer> offerRepository;
        private readonly ICurrentUserService currentUserService;

        public CreateOneOfferCommandHandler(IBaseRepository<Offer> offerRepository, ICurrentUserService currentUserService)
        {
            this.offerRepository = offerRepository;
            this.currentUserService = currentUserService;
        }
        public async Task<int> Handle(CreateOneOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = new Offer()
            {
                ManufacturerEmail = request.ManufacturerEmail,
                Message = request.Message,
                ProductId = request.ProductId,
                DistributorEmail = currentUserService.UserEmail
            };

            return await offerRepository.AddAsync(offer);
        }
    }
}

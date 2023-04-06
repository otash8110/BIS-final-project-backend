using AutoMapper;
using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Entities;
using MediatR;

namespace FinalProject.Application.Products.Queries.GetOneProduct
{
    public record GetOneProductQuery(int id): IRequest<OneProductDTO>;

    public class GetOneProductQueryHandler : IRequestHandler<GetOneProductQuery, OneProductDTO>
    {
        private readonly IBaseRepository<Product> productRepository;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper mapper;

        public GetOneProductQueryHandler(IBaseRepository<Product> productRepository,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.currentUserService = currentUserService;
            this.mapper = mapper;
        }

        public async Task<OneProductDTO> Handle(GetOneProductQuery request, CancellationToken cancellationToken)
        {
            var userProducts = await productRepository.GetByFilterAsync(p => p.UserId == currentUserService.UserId);
            var userProduct = userProducts.Where(p => p.Id == request.id).FirstOrDefault();
            if (userProduct == null) throw new UnauthorizedAccessException();

            return mapper.Map<OneProductDTO>(userProduct);
        }
    }
}

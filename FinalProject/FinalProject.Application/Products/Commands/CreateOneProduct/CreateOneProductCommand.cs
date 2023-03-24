using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Entities;
using FinalProject.Core.Enums;
using MediatR;

namespace FinalProject.Application.Products.Commands.CreateOneProduct
{
    public class CreateOneProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
        public string UserId { get; set; }
    }

    public class CreateOneProductCommandHandler : IRequestHandler<CreateOneProductCommand, int>
    {
        private readonly IBaseRepository<Product> productRepository;

        public CreateOneProductCommandHandler(IBaseRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<int> Handle(CreateOneProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Rating = request.Rating,
                UserId = request.UserId
            };

            return await productRepository.Add(product);
        }
    }
}

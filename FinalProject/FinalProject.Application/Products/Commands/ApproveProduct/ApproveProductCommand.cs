
using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Entities;
using MediatR;

namespace FinalProject.Application.Products.Commands.ApproveProduct
{
    public record ApproveProductCommand(int productId): IRequest<bool>;

    public class ApproveProductCommandHandler : IRequestHandler<ApproveProductCommand, bool>
    {
        private readonly IBaseRepository<Product> productRepository;

        public ApproveProductCommandHandler(IBaseRepository<Product> productRepository) {
            this.productRepository = productRepository;
        }

        public async Task<bool> Handle(ApproveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetOneAsync(request.productId);
            product.IsApproved = true;

            return await productRepository.UpdateAsync(product);
        }
    }
}

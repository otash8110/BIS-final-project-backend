using MediatR;

namespace FinalProject.Application.Products.Commands.CreateOneProduct
{
    public class CreateOneProductCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rate { get; set; }
    }

    public class CreateOneProductCommandHandler : IRequestHandler<CreateOneProductCommand>
    {
        public Task<Unit> Handle(CreateOneProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

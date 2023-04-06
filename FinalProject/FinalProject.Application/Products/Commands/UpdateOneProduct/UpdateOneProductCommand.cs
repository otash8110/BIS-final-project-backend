﻿using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Entities;
using FinalProject.Core.Enums;
using MediatR;
using System;
namespace FinalProject.Application.Products.Commands.UpdateOneProduct
{
    public class UpdateOneProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
    }

    public class UpdateOneProductHandler : IRequestHandler<UpdateOneProductCommand, bool>
    {
        private readonly IBaseRepository<Product> productRepository;

        public UpdateOneProductHandler(IBaseRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateOneProductCommand request, CancellationToken cancellationToken)
        {
            var product = (await productRepository.GetByFilterAsync(p => p.Id == request.ProductId)).First();
            if (product == null) throw new ArgumentOutOfRangeException("Product not found or was deleted");

            var updatedProduct = new Product
            {
                Id = product.Id,
                Name = request.Name,
                Description = request.Description,
                Rating = request.Rating,
                UserId = product.UserId
            };

            return await productRepository.UpdateAsync(updatedProduct);
        }
    }
}

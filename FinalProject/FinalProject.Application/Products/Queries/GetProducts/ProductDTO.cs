﻿using FinalProject.Core.Enums;

namespace FinalProject.Application.Products.Queries.GetProducts
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
    }
}

﻿using FinalProject.Core.Enums;

namespace FinalProject.Application.Products.Queries.GetUnregisteredProducts
{
    public class GetUnregisteredProductsDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
        public string UserId { get; set; }
        public bool IsApproved { get; set; }
    }
}

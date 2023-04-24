using FinalProject.Core.Enums;

namespace FinalProject.Application.Products.Queries.GetOneProductWithManufacturer
{
    public class OneProductWithManufacturerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; } 
    }
}

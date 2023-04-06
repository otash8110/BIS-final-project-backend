
using FinalProject.Core.Enums;

namespace FinalProject.Application.Products.Queries.GetOneProduct
{
    public class OneProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }

    }
}

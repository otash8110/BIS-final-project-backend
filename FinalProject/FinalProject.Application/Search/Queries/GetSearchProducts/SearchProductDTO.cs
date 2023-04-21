using FinalProject.Core.Enums;

namespace FinalProject.Application.Search.Queries.GetSearchProducts
{
    public class SearchProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
        public string UserId { get; set; }
    }
}

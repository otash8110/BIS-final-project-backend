namespace FinalProject.Application.Offers.Queries.Results
{
    public class OfferResultDTO
    {
        public string Message { get; set; }
        public string DistributorEmail { get; set; }
        public string ManufacturerEmail { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}

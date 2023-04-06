using FinalProject.Core.Common;

namespace FinalProject.Core.Entities
{
    public class Offer: AuditableBaseEntity
    {
        public string DistributorId { get; set; }
        public string ManufacturerId { get; set; }
    }
}

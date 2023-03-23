using FinalProject.Core.Common;

namespace FinalProject.Core.Entities
{
    public class Product : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }

        public string UserId { get; set; }
    }
}

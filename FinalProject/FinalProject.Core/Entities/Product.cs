using FinalProject.Core.Common;
using FinalProject.Core.Enums;

namespace FinalProject.Core.Entities
{
    public class Product : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }

        public string UserId { get; set; }
    }
}

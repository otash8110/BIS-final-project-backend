namespace FinalProject.Core.Common
{
    public abstract class AuditableBaseEntity: BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }


        public DateTime? LastModified { get; set; }

    }
}

namespace FinalProject.Core.Common
{
    public abstract class AuditableBaseEntity: BaseEntity
    {
        public DateTime Created { get; set; }


        public DateTime? LastModified { get; set; }

    }
}

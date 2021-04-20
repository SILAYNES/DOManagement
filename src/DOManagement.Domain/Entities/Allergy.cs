using DOManagement.Domain.Common;

namespace DOManagement.Domain.Entities
{
    public class Allergy : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
      
    }
}

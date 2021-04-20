using System;

namespace DOManagement.Domain.Common
{
    public class AuditableEntity
    {
        public bool Enabled { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
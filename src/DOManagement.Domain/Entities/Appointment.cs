using System;
using DOManagement.Domain.Common;

namespace DOManagement.Domain.Entities
{
    public class Appointment : AuditableEntity
    {
        public long Id { get; set; }
        public DateTime DateAt { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public Specialist Specialist { get; set; }
        public Patient Patient { get; set; }     
    }
}

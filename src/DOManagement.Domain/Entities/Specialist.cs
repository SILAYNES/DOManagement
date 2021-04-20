using System.Collections.Generic;
using DOManagement.Domain.Common;

namespace DOManagement.Domain.Entities
{
    public class Specialist : AuditableEntity
    {
        public long Id { get; set; }   
        public string Names { get; set; }
        public string Surnames { get; set; }
        public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
    }
}

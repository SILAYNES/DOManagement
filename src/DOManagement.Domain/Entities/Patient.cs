using System;
using System.Collections.Generic;
using DOManagement.Domain.Common;

namespace DOManagement.Domain.Entities
{
    public class Patient : AuditableEntity
    {
        public long Id { get; set; }   
        public string Names { get; set; }
        public string Surnames { get; set; }
        public long Age { get; set; }
        public DateTime Birthday { get; set; }
        public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
        public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
    }
}

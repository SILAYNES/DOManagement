using System;
using System.Collections.Generic;

namespace DOManagement.Application.Common.Models
{
    public class PatientModel
    {
        public long Id { get; set; }   
        public string Names { get; set; }
        public string Surnames { get; set; }
        public long Age { get; set; }
        public DateTime Birthday { get; set; }
        public IEnumerable<AppointmentModel> Appointments { get; private set; } = new List<AppointmentModel>();
        public IEnumerable<AllergyModel> Allergies { get; set; } = new List<AllergyModel>();
    }
}
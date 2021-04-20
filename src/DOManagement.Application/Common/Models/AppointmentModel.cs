using System;

namespace DOManagement.Application.Common.Models
{
    public class AppointmentModel
    {
        public long Id { get; set; }
        public DateTime DateAt { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public SpecialistModel Specialist { get; set; }
        public PatientModel Patient { get; set; }     
    }
}
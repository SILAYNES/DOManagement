using System;
using System.Collections.Generic;

namespace DOManagement.Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
    
    public abstract class DomainEvent
    {
        private readonly DateTimeOffset _dateOccurred;

        protected DomainEvent()
        {
            _dateOccurred = DateTimeOffset.UtcNow;
        }

        public bool IsPublished { get; set; }

        public DateTimeOffset DateOccurred => _dateOccurred;
    }
}
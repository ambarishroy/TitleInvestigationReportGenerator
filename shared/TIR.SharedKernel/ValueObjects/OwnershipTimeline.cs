using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIR.SharedKernel.ValueObjects
{
    public sealed class OwnershipTimeline
    {
        private readonly List<OwnershipEvent> _events = new();

        public IReadOnlyList<OwnershipEvent> Events => _events.OrderBy(e => e.EventDate ?? DateTime.MinValue).ToList();

        public void AddEvent(OwnershipEvent ownershipEvent) => _events.Add(ownershipEvent);

        public bool HasGaps()
        {
            // If any EventDate is null or FromOwner / ToOwner UNKNOWN → gap exists
            return _events.Any(e => e.EventDate == null || e.FromOwner == "UNKNOWN" || e.ToOwner == "UNKNOWN");
        }
    }
}

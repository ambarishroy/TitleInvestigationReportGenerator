using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIR.SharedKernel.ValueObjects
{
    public sealed class OwnershipEvent
    {
        public DateTime? EventDate { get; private set; }
        public string FromOwner { get; private set; } = "UNKNOWN";
        public string ToOwner { get; private set; } = "UNKNOWN";
        public string InstrumentType { get; private set; } = "UNKNOWN";
        public EvidenceReference Evidence { get; private set; }

        public OwnershipEvent(DateTime? eventDate, string fromOwner, string toOwner, string instrumentType, EvidenceReference evidence)
        {
            EventDate = eventDate;
            FromOwner = string.IsNullOrWhiteSpace(fromOwner) ? "UNKNOWN" : fromOwner;
            ToOwner = string.IsNullOrWhiteSpace(toOwner) ? "UNKNOWN" : toOwner;
            InstrumentType = string.IsNullOrWhiteSpace(instrumentType) ? "UNKNOWN" : instrumentType;
            Evidence = evidence;
        }
    }
}

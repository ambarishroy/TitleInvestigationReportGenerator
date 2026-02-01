using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIR.SharedKernel.Enums;

namespace TIR.SharedKernel.ValueObjects
{
    public sealed class LegalFact
    {
        public FactType Type { get; private set; }
        public EvidenceReference Evidence { get; private set; }
        public string Value { get; private set; } = "UNKNOWN";
        public LegalFact(FactType type, EvidenceReference evidence, string value)
        {
            Type = type;
            Evidence = evidence;
            Value = string.IsNullOrWhiteSpace(value) ? "UNKNOWN" : value;
        }
    }
}

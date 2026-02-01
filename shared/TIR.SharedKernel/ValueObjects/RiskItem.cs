using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIR.SharedKernel.ValueObjects
{
    public sealed class RiskItem
    {
        public string Description { get; private set; } = string.Empty;
        public EvidenceReference Evidence { get; private set; }

        public RiskItem(string description, EvidenceReference evidence)
        {
            Description = description;
            Evidence = evidence;
        }
    }
}

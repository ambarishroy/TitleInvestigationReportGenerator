using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIR.SharedKernel.Events
{
    public sealed record OcrCompletedEvent
    (
        Guid TenantId,
        Guid ProjectId,
        Guid DocumentId,
        bool IsFullyReadable,
        double AverageConfidence,
        IReadOnlyList<int> FailedPages,
        DateTime ProcessedAtUtc,
        string CorrelationId

    );
}

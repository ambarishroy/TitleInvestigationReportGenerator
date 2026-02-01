using System;
using System.Linq;
using System.Security.AccessControl;
using TIR.SharedKernel.ValueObjects;
using TIR.SharedKernel.Enums;
namespace TIR.TitleAnalysisService.Infrastructure;

public sealed class TimelineBuilder
{
    public OwnershipTimeline BuildTimeline(Guid documentId, List<LegalFact> facts)
    {
        var timeline = new OwnershipTimeline();

        // Example deterministic rule: any OwnershipTransfer fact creates an event
        var transfers = facts.Where(f => f.Type == FactType.OwnershipTransfer);
        foreach (var f in transfers)
        {
            // In real production, parse From/To/Date from structured fact
            var ownershipEvent = new OwnershipEvent(
                eventDate: DateTime.TryParse(f.Value, out var dt) ? dt : null,
                fromOwner: "UNKNOWN", // Extracted from fact if available
                toOwner: "UNKNOWN",   // Extracted from fact if available
                instrumentType: "Sale Deed",
                evidence: f.Evidence
            );
            timeline.AddEvent(ownershipEvent);
        }

        return timeline;
    }
}
